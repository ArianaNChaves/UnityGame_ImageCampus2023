using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveRestriction;
    [SerializeField] private float fallMultiplier;
    
    private const float Gravity = -9.81f;
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    private Vector2 _currentDirection;
    private bool _isGrounded = true;

    private void Awake()
    {
        InitializePlayerInput();
    }
    private void InitializePlayerInput()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = new PlayerInput();
        _playerInput.Player.Move.performed += ctx => _currentDirection = ctx.ReadValue<Vector2>();
        _playerInput.Player.Move.canceled += ctx => _currentDirection = Vector2.zero;
        _playerInput.Player.Jump.started += ctx => StartJump();
        _playerInput.Enable();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        HandleMovement();
        HandleJump();
    }
    private void HandleMovement()
    {
        float moveSpeed = _isGrounded ? speed : speed * moveRestriction;
        Vector3 movement = new Vector3(_currentDirection.x, 0, _currentDirection.y).normalized * moveSpeed;
        _rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, movement.z);
    }
    private void HandleJump()
    {
        if (!_isGrounded && _rigidbody.velocity.y < 10f)
        {
            _rigidbody.velocity += Vector3.up * (Gravity * fallMultiplier * Time.fixedDeltaTime);
        }
    }
    private void StartJump()
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
    }
    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
}