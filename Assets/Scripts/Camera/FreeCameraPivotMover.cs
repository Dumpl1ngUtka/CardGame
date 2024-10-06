using UnityEngine;

namespace Battleground
{
    public class FreeCameraPivotMover : MonoBehaviour
    {
        [SerializeField] private float _cameraSensitivity = 2f;
        [SerializeField] private float _freeCamSpeed = 0.4f;
        private Vector3 _mousePositionDelta;
        private PlayerInput _inputActions;
        private Vector3 _velocity;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _inputActions = new PlayerInput();
        }

        private void InputHorizontalMove()
        {
            var value = _inputActions.Battle.CameraMove.ReadValue<Vector2>();
            _velocity += (Camera.main.transform.forward * value.y + Camera.main.transform.right * value.x) * _freeCamSpeed;
        }        
        
        private void InputVerticalMove()
        {
            var value = _inputActions.Battle.CameraVecticalMove.ReadValue<float>();
            _velocity += _freeCamSpeed * value * Vector3.up;
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        private void Update()
        {
            _velocity = Vector3.zero;
            if (_inputActions.Battle.CameraMove.IsPressed() )
                InputHorizontalMove();
            if (_inputActions.Battle.CameraVecticalMove.IsPressed())
                InputVerticalMove();
            RotateCamera();
            SetPivotPosition();
            _rigidbody.velocity = _velocity * 100;
        }

        private void RotateCamera()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _mousePositionDelta = Input.mousePosition;
            }
            if (Input.GetMouseButton(1))
            {
                //var delta = (_mousePositionDelta - Input.mousePosition) * (_cameraSensitivity / 2 * Mathf.Deg2Rad);
                //_cameraRotation += new Vector2(-delta.x, delta.y);
                //_cameraRotation = new Vector2(_cameraRotation.x, Mathf.Clamp(_cameraRotation.y, Mathf.Deg2Rad * -20f, Mathf.Deg2Rad * 35f));
                ////transform.rotation =  new Quaternion(Mathf.Sin(_cameraRotation.y), 0f, 0f, Mathf.Cos(_cameraRotation.y)) * new Quaternion(0f, Mathf.Sin(_cameraRotation.x), 0f, Mathf.Cos(_cameraRotation.x)); #drank effect
                //transform.rotation = new Quaternion(0f, Mathf.Sin(_cameraRotation.x), 0f, Mathf.Cos(_cameraRotation.x)) * new Quaternion(Mathf.Sin(_cameraRotation.y), 0f, 0f, Mathf.Cos(_cameraRotation.y));
                //_mousePositionDelta = Input.mousePosition;
                var delta = (_mousePositionDelta - Input.mousePosition) * _cameraSensitivity;
                var newPivotRotation = transform.rotation.eulerAngles + new Vector3(delta.y, -delta.x);
                transform.rotation = Quaternion.Euler(newPivotRotation);
                _mousePositionDelta = Input.mousePosition;
            }
        }

        private void SetPivotPosition()
        {
            if (Input.GetMouseButtonDown(2))
            {
                _mousePositionDelta = Input.mousePosition;
            }
            if (Input.GetMouseButton(2))
            {
                var delta = (_mousePositionDelta - Input.mousePosition) * _cameraSensitivity / 10;
                _velocity += (Camera.main.transform.up * delta.y + Camera.main.transform.right * delta.x);
                _mousePositionDelta = Input.mousePosition;
            }
        }
    }
    public interface ICameraPivot
    {
        public Vector3 PivotPosition { get; }
        public Transform PivotTransform { get; }
    }
}