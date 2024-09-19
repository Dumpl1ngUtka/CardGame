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
        private float _lerpScroll;
        private float _zoom;
        private Vector3 _mousePositionDelta;
        private Transform _pivot;
        private Vector3 _pivotPos;

        private Vector3 PivotPos
        {
            get
            {
                return _pivot.position;
            }
            set
            {
                _pivot = transform;
                _pivot.position = value;
            }
        }

        private void Start()
        {
            SetPivot(_defaultPivot);
        }

        private void Update()
        {
            InputScroll();
            ChangeZoom();
            RotateCamera();
            MoveCamera();
            transform.position = Vector3.Lerp(transform.position, PivotPos, Time.deltaTime * _pivotSpeed);
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
                SetPivot();
            }
            if (Input.GetMouseButton(1))
            {
                var delta = (_mousePositionDelta - Input.mousePosition) * GetCameraSensitivityByDistance();
                var newPivotRotation = transform.rotation.eulerAngles + new Vector3(delta.y, delta.x);
                transform.rotation = Quaternion.Euler(newPivotRotation);
                _mousePositionDelta = Input.mousePosition;
            }
        }

        private void MoveCamera()
        {
            if (Input.GetMouseButtonDown(2))
            {
                _mousePositionDelta = Input.mousePosition;
            }
            if (Input.GetMouseButton(2))
            {
                var delta = (_mousePositionDelta - Input.mousePosition) * GetCameraSensitivityByDistance() / 10;
                var newPivotPosition = PivotPos + new Vector3(delta.x , 0 , delta.y);
                PivotPos = newPivotPosition;
                _mousePositionDelta = Input.mousePosition;
            }
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

        public void SetPivot(Transform pivot)
        {
            _pivot = pivot;
        }

        public void SetPivot()
        {
            _pivot = transform;
        }
    }
    public interface ICameraPivot
    {

    }
}