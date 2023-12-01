using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player
{
    public class AirPlayerController : MonoBehaviour
    {
        [Header("Hook")]
        [SerializeField] private LayerMask whatIsGrapple;
        [SerializeField] private Transform hookTip;
        [SerializeField] private Transform hookCamera;
        [SerializeField] private float maxDistance;

        
        [Header("Joint")]
        [SerializeField] private float maxDistanceFactor;
        [SerializeField] private float minDistanceFactor;
        [SerializeField] private float springForce;
        [SerializeField] private float damperValue;
        [SerializeField] private float massScaleValue;
        [SerializeField] private float extendCableSpeed;
        [SerializeField] private float shortenCableSpeed;

        [Header("Line Renderer")] 
        [SerializeField] private int lineRendererPositions;
        [SerializeField] private LineRenderer lineRenderer;

        [Header("Prediction")] 
        [SerializeField] private float predictionSphereCastRadius;
        
        [Header("Aim")]
        [SerializeField] private Image crosshairImage; 
        [SerializeField] private Color canHookColor; 
        [SerializeField] private Color cannotHookColor;

        [Header("Air Movement")] 
        [SerializeField] private float horizontalForce;
        [SerializeField] private Transform orientation;

        private PlayerStateMachine _playerStateMachine;
        private RaycastHit _predictionHit;
        private SpringJoint _joint;
        private Vector3 _hookPoint;
        private InputMap _playerInput;
        private Rigidbody _rigidbody;
        
        
        private void Awake()
        {
            _playerInput = new InputMap();
            _rigidbody = GetComponent<Rigidbody>();
            _playerStateMachine = GetComponent<PlayerStateMachine>();
            OnEnable();
        }

        private void Start()
        {
           
        }

        private void LateUpdate()
        {
            DrawRope();
        }

        private void Update()
        {
            ValidateHookPoints();
            if (_playerInput.Player.ShortenHook.inProgress)
            {
                Debug.Log("Acrotndo");
                ShortenHook();
            }
            if (_playerInput.Player.ExtendHook.inProgress)
            {
                Debug.Log("Extendiendo");
                ExtendHook();
            }
        }

        public void OnEnable()
        {
            _playerInput.Enable();
            _playerInput.Player.Hook.performed += OnHookPerformed;
            _playerInput.Player.Hook.canceled  += OnHookCanceled;
            _playerInput.Player.HookLateralMovement.performed += OnHookLateralMovementPerformed;
            _playerInput.Player.HookForwardMovement.performed += OnHookForwardMovementPerformed;
            _playerInput.Player.ExtendHook.performed += OnExtendHookPerformed;
            _playerInput.Player.ShortenHook.performed += OnShortenHookPerformed;
        }

        private void OnHookPerformed(InputAction.CallbackContext context)
        {
            _playerStateMachine.SetCurrentState(PlayerStateMachine.MovementState.Swinging);
            if (_predictionHit.point == Vector3.zero) return;
            
                _hookPoint = _predictionHit.point;
                _joint = gameObject.AddComponent<SpringJoint>();
                _joint.autoConfigureConnectedAnchor = false;
                _joint.connectedAnchor = _hookPoint;

                var position = transform.position;
                float distanceFromPoint = Vector3.Distance(position, _hookPoint);

                _joint.maxDistance = distanceFromPoint * maxDistanceFactor;
                _joint.minDistance = distanceFromPoint * minDistanceFactor;
                _joint.spring = springForce;
                _joint.damper = damperValue;
                _joint.massScale = massScaleValue;

                lineRenderer.positionCount = lineRendererPositions;
                
        }

        private void OnHookLateralMovementPerformed(InputAction.CallbackContext context)
        {
            float horizontalInput = context.ReadValue<float>();
            _rigidbody.AddForce(orientation.right * horizontalForce * horizontalInput, ForceMode.Impulse);
        }
        private void OnHookForwardMovementPerformed(InputAction.CallbackContext context)
        {
            float forwardInput = context.ReadValue<float>();
            _rigidbody.AddForce(orientation.forward * horizontalForce * forwardInput, ForceMode.Impulse);
        }
        private void OnExtendHookPerformed(InputAction.CallbackContext context)
        {
            ExtendHook();
        }
        private void OnShortenHookPerformed(InputAction.CallbackContext context)
        {
            ShortenHook();
        }

        private void ShortenHook()
        {
            if (!_joint) return;
            float shortenDistanceFromPoint = Vector3.Distance(transform.position, _hookPoint) - shortenCableSpeed;
            _joint.maxDistance = shortenDistanceFromPoint * maxDistanceFactor;
            _joint.minDistance = shortenDistanceFromPoint * minDistanceFactor; 
        }

        private void ExtendHook()
        {
            if (!_joint) return;
            float extendDistanceFromPoint = Vector3.Distance(transform.position, _hookPoint)  + extendCableSpeed;
            _joint.maxDistance = extendDistanceFromPoint * maxDistanceFactor;
            _joint.minDistance = extendDistanceFromPoint * minDistanceFactor;
        }
        private void DrawRope()
        {
            if (!_joint) return;
            
            lineRenderer.SetPosition(0,hookTip.position);
            lineRenderer.SetPosition(1,_hookPoint);
        }

        private void OnHookCanceled(InputAction.CallbackContext context)
        {
            _playerStateMachine.SetCurrentState(PlayerStateMachine.MovementState.Grounded);
            lineRenderer.positionCount = 0;
            Destroy(_joint);
        }
        public void OnDisable()
        {
            _playerInput.Disable();
            _playerInput.Player.Hook.performed -= OnHookPerformed;
            _playerInput.Player.Hook.canceled  -= OnHookCanceled;
            _playerInput.Player.HookLateralMovement.performed -= OnHookLateralMovementPerformed;
            _playerInput.Player.HookForwardMovement.performed -= OnHookForwardMovementPerformed;
            _playerInput.Player.ExtendHook.performed -= OnExtendHookPerformed;
            _playerInput.Player.ShortenHook.performed -= OnShortenHookPerformed;
        }

        private void ValidateHookPoints()
        {
            var position = hookCamera.position;
            var forward = hookCamera.forward;

            Physics.SphereCast(position, predictionSphereCastRadius, forward, out var sphereCastHit,
                maxDistance, whatIsGrapple);

            Physics.Raycast(position, forward, out var raycastHit, maxDistance, whatIsGrapple);

            Vector3 realHitPoint;
            if (raycastHit.point != Vector3.zero)
            {
                realHitPoint = raycastHit.point;
            }
            else if (sphereCastHit.point != Vector3.zero)
            {
                realHitPoint = sphereCastHit.point;
            }
            else
            {
                realHitPoint = Vector3.zero;
            }

            crosshairImage.color = realHitPoint != Vector3.zero ? canHookColor : cannotHookColor;

            _predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
        }
        public void ActivateController()
        {
            _playerInput.Player.HookLateralMovement.performed += OnHookLateralMovementPerformed;
            _playerInput.Player.HookForwardMovement.performed += OnHookForwardMovementPerformed;
            _playerInput.Player.ExtendHook.performed += OnExtendHookPerformed;
            _playerInput.Player.ShortenHook.performed += OnShortenHookPerformed;
        }
        public void DeactivateController()
        {
            _playerInput.Player.HookForwardMovement.performed -= OnHookForwardMovementPerformed;
            _playerInput.Player.HookLateralMovement.performed -= OnHookLateralMovementPerformed;
            _playerInput.Player.ExtendHook.performed -= OnExtendHookPerformed;
            _playerInput.Player.ShortenHook.performed -= OnShortenHookPerformed;
        }
        
    }
}
