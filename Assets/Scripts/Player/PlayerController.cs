using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform orientation;
        [SerializeField] private float playerHeight;        
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private float groundDrag;
        
        [Header("Jump")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpCooldown;
        [SerializeField] private float airMultiplier;
        
        [Header("Fall")]
        [SerializeField] private float fallMultiplier;
        [SerializeField] private float gravity;
        [SerializeField] private float maxFallSpeed;
        
        private PlayerStateMachine _playerStateMachine;
        private bool _isReadyToJump;
        private float _horizontalInput;
        private float _verticalInput;
        private Vector3 _moveDirection;
        private InputMap _playerInput;
        private Rigidbody _rigidbody;
        private bool _isGrounded;

        private void Awake()
        {
            _playerInput = new InputMap();
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.freezeRotation = true;
            _playerStateMachine = GetComponent<PlayerStateMachine>();
        }

        private void Start()
        {
             OnEnable();
            _isReadyToJump = true;
        }

        public void ActivateController()
        {
            _playerInput.Player.Move.performed += OnMovePerformed;
            _playerInput.Player.Move.canceled += OnMoveCanceled;
            _playerInput.Player.Jump.started += OnJumpStarted;
        }
        public void DeactivateController()
        {
            _playerInput.Player.Move.performed -= OnMovePerformed;
            _playerInput.Player.Move.canceled -= OnMoveCanceled;
            _playerInput.Player.Jump.started -= OnJumpStarted;
        }

        public void OnEnable()
        {
            _playerInput.Enable();
            _playerInput.Player.Move.performed += OnMovePerformed;
            _playerInput.Player.Move.canceled += OnMoveCanceled;
            _playerInput.Player.Jump.started += OnJumpStarted;

        }

        public void OnDisable()
        {
            _playerInput.Disable();
            _playerInput.Player.Move.performed -= OnMovePerformed;
            _playerInput.Player.Move.canceled -= OnMoveCanceled;
            _playerInput.Player.Jump.started -= OnJumpStarted;

        }

        private void FixedUpdate()
        {
            MyInput();
            GroundCheck();
            SpeedControl();
        }

        private void MyInput()
        {
            _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;

            if (_isGrounded)
            {
                _rigidbody.AddForce(_moveDirection.normalized * moveSpeed, ForceMode.Force);
            }
            else if (!_isGrounded)
            {
                _rigidbody.AddForce(_moveDirection.normalized * (moveSpeed * airMultiplier), ForceMode.Force);
            }
        }
        private void OnJumpStarted(InputAction.CallbackContext context)
        {
            if (!_isReadyToJump || !_isGrounded) return;
            _isReadyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _horizontalInput = context.ReadValue<Vector2>().x;
            _verticalInput = context.ReadValue<Vector2>().y;
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            _horizontalInput = 0f;
            _verticalInput = 0f;
        }

        private void GroundCheck()
        {
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
            if (_isGrounded)
            {
                _rigidbody.drag = groundDrag;
            }
            else
            {
                PlayerFall();
                _rigidbody.drag = 0;
            }
        }

        private void SpeedControl()
        {
            Vector3 velocity = _rigidbody.velocity;
            Vector3 flatVel = new Vector3(velocity.x, 0f, velocity.z);

            if (!(flatVel.magnitude > moveSpeed)) return;
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _rigidbody.velocity = new Vector3(limitedVel.x, velocity.y, limitedVel.z);
        }

        private void Jump()
        {
            Vector3 velocity = _rigidbody.velocity;
            velocity = new Vector3(velocity.x, 0f, velocity.z);
            _rigidbody.velocity = velocity;
            
            //si voy a rotar el objeto me conviene usar transform.up que toma en cuenta el Y local
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            _isReadyToJump = true;
        }

        private void PlayerFall()
        {
            if (_isGrounded) return;
            
            float gravityForce = gravity * fallMultiplier;
            _rigidbody.AddForce(Vector3.down * gravityForce, ForceMode.Acceleration);
            
            if (_rigidbody.velocity.y < -maxFallSpeed)
            {
                var velocity = _rigidbody.velocity;
                velocity = new Vector3(velocity.x, -maxFallSpeed, velocity.z);
                _rigidbody.velocity = velocity;
            }
        }
    }
}