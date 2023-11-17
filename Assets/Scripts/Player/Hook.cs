using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player
{
    public class Hook : MonoBehaviour
    {
        [Header("Hook")]
        [SerializeField] private LayerMask whatIsGrapple;
        [SerializeField] private Transform hookTip;
        [SerializeField] private Transform hookCamera;
        [SerializeField] private Transform player;
        [SerializeField] private float maxDistance;
        
        [Header("Joint")]
        [SerializeField] private float maxDistanceFactor;
        [SerializeField] private float minDistanceFactor;
        [SerializeField] private float springForce;
        [SerializeField] private float damperValue;
        [SerializeField] private float massScaleValue;

        [Header("Line Renderer")] 
        [SerializeField] private int lineRendererPositions;

        [Header("Prediction")] 
        [SerializeField] private float predictionSphereCastRadius;
        
        [Header("Aim")]
        [SerializeField] private Image crosshairImage; 
        [SerializeField] private Color canHookColor; 
        [SerializeField] private Color cannotHookColor;
        
        
        private RaycastHit _predictionHit;
        private SpringJoint _joint;
        private LineRenderer _lineRenderer;
        private Vector3 _hookPoint;

        public PlayerController playerController;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            if (playerController == null) return;
            playerController.HookActionPerformed += HookActionPerformed;
            playerController.HookActionCanceled += HookActionCanceled;
        }

        private void LateUpdate()
        {
            DrawRope();
        }

        private void Update()
        {
            ValidateHookPoints();
        }

        private void HookActionPerformed(InputAction.CallbackContext context)
        {
            if (_predictionHit.point == Vector3.zero) return;
            
                _hookPoint = _predictionHit.point;
                _joint = player.gameObject.AddComponent<SpringJoint>();
                _joint.autoConfigureConnectedAnchor = false;
                _joint.connectedAnchor = _hookPoint;

                float distanceFromPoint = Vector3.Distance(player.position, _hookPoint);

                _joint.maxDistance = distanceFromPoint * maxDistanceFactor;
                _joint.minDistance = distanceFromPoint * minDistanceFactor;
                _joint.spring = springForce;
                _joint.damper = damperValue;
                _joint.massScale = massScaleValue;

                _lineRenderer.positionCount = lineRendererPositions;
        }

        private void DrawRope()
        {
            if (!_joint) return;
            
            _lineRenderer.SetPosition(0,hookTip.position);
            _lineRenderer.SetPosition(1,_hookPoint);
        }

        private void HookActionCanceled(InputAction.CallbackContext context)
        {
            _lineRenderer.positionCount = 0;
            Destroy(_joint);
        }
        private void OnDestroy()
        {
            if (playerController == null) return;
            playerController.HookActionPerformed -= HookActionPerformed;
            playerController.HookActionCanceled -= HookActionCanceled;
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
    }
}
