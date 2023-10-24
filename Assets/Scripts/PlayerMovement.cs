using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    private Vector2 _currentDirection;
    private bool _isJumping;
    private const float Gravity = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = new PlayerInput();
        _playerInput.Player.Move.performed += ctx => _currentDirection = ctx.ReadValue<Vector2>();
        _playerInput.Player.Move.canceled += ctx => _currentDirection = Vector2.zero;
        _playerInput.Player.Jump.started+= ctx => StartJump();
        _playerInput.Player.Jump.canceled+= ctx => EndJump();
        _playerInput.Enable();
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(_currentDirection.x, 0, _currentDirection.y) * (speed * Time.fixedDeltaTime);
        _rigidbody.AddForce(movement, ForceMode.VelocityChange);
    }
    private void EndJump()
    {
        if (_isJumping)
        {
            
        }
        _isJumping = false;
        Debug.Log("Termino el salto" + _isJumping);
    }

    private void StartJump()
    {
        if (!_isJumping)
        {
            
        }
        _rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        _isJumping = true;
        Debug.Log("Empezo el salto" + _isJumping);

    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}