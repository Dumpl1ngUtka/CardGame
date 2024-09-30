using UnityEngine;

namespace Battleground
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _scrollSpeed = 0.1f;
        [SerializeField] private float _lerpScrollSpeed = 2f;
        [SerializeField] private float _cameraSensitivity = 2f;
        [SerializeField] private float _mapSize = 5f;
        [SerializeField] private Transform _defaultPivot;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _pivotSpeed = 0.4f;
        #region SpringMove
        private float _spring = 0.1f;
        private float _drag = 0.3f;
        private Vector3 _vel = Vector3.zero;
        #endregion
        private float _lerpScroll;
        private float _zoom;
        private Vector3 _mousePositionDelta;
        private ICameraPivot _pivot;
        private Vector3 _pivotPos;

        private void Start()
        {
            _pivotPos = transform.position;
        }

        private void Update()
        {
            InputScroll();
            ChangeZoom();
            RotateCamera();
            SetCameraPosition();
            if (_pivot != null)
                _pivotPos = _pivot.PivotPosition;
            SpringCameraMove();
        }

        private void InputScroll()
        {
            _zoom -= Input.mouseScrollDelta.y * _scrollSpeed;
            _zoom = Mathf.Clamp01(_zoom);
            _lerpScroll = Mathf.MoveTowards(_lerpScroll, _zoom, Time.deltaTime * _lerpScrollSpeed);
        }

        private void ChangeZoom()
        {
            var height = Mathf.Lerp(5, 40, _lerpScroll);
            _camera.transform.localPosition = new(0,0 , -height);
            //transform.rotation = Quaternion.Euler(Vector3.Lerp(_lowPosition.Rotation, _highPosition.Rotation, _lerpScroll));
        }

        private void RotateCamera()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _mousePositionDelta = Input.mousePosition;
            }
            if (Input.GetMouseButton(1))
            {
                var delta = (_mousePositionDelta - Input.mousePosition) * GetCameraSensitivityByDistance();
                var newPivotRotation = transform.rotation.eulerAngles + new Vector3(delta.y, delta.x);
                transform.rotation = Quaternion.Euler(newPivotRotation);
                _mousePositionDelta = Input.mousePosition;
            }
        }

        private void SpringCameraMove()
        {
            _vel += (_pivotPos - transform.position) * _spring;
            _vel -= _vel * _drag;
            transform.position += _vel;
            //transform.position = Vector3.Lerp(transform.position, _pivotPos, Time.deltaTime * _pivotSpeed);
        }

        private void SetCameraPosition()
        {
            if (Input.GetMouseButtonDown(2))
            {
                _mousePositionDelta = Input.mousePosition;
                _pivot = null;
            }
            if (Input.GetMouseButton(2))
            {
                var delta = (_mousePositionDelta - Input.mousePosition) * GetCameraSensitivityByDistance() / 10;
                var newPivotPosition = _pivotPos + (transform.up * delta.y + transform.right * delta.x);
                if (!IsOnCollision(_pivotPos, newPivotPosition))
                {
                    _pivotPos = newPivotPosition;
                }
                _mousePositionDelta = Input.mousePosition;
            }
        }

        private bool IsOnCollision(Vector3 startPosition, Vector3 endPosition)
        {
            var distance = (startPosition - endPosition).magnitude;
            return Physics.Raycast(startPosition, (endPosition - startPosition).normalized, distance);
        }

        private float GetCameraSensitivityByDistance()
        {
            //if (GetMapSizeByZoom() == 0)
            //    return 0;
            //var distance = 1 - new Vector2(transform.position.x, transform.position.z).magnitude / GetMapSizeByZoom() + _borderOffset;
            //var currentSensitivity = Mathf.Lerp(0, _cameraSensitivity, distance);
            return _cameraSensitivity;
        }

        private float GetMapSizeByZoom()
        {
            var mapSize = Mathf.Lerp(0, _mapSize, 1 - _zoom);
            return mapSize;
        }

        public void SetPivot(ICameraPivot pivot)
        {
            _pivot = pivot;
        }

        public void SetPivot()
        {
            _pivot = null;
        }
    }
    public interface ICameraPivot
    {
        public Vector3 PivotPosition { get; }
    }
}