using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform orientation;
        [SerializeField] private float sensX;
        [SerializeField] private float sensY;
        private PlayerInput _cameraInput;
        private float _xRotation;
        private float _yRotation;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            CameraRotation();
        }

        private void CameraRotation()
        {
            float mouseX = Mouse.current.delta.x.ReadValue() * Time.deltaTime * sensX;
            float mouseY = Mouse.current.delta.y.ReadValue() * Time.deltaTime * sensY;

            _xRotation -= mouseY;
            _yRotation += mouseX;

            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            
            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
        }
    }
}
