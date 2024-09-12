using UnityEngine;

namespace Battleground
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _scrollSpeed = 0.1f;
        [SerializeField] private float _lerpScrollSpeed = 2f;
        [SerializeField] private float _cameraSensitivity = 2f;
        [SerializeField] private float _mapSize = 5f;
        private CameraPosition _lowPosition = new(5, new(25, 0, 0)); //new(25, new(60, 0, 0))
        private CameraPosition _highPosition = new(20, new(80, 0, 0));
        private float _lerpScroll;
        private float _zoom;
        private Vector3 _mousePositionDelta;
        private float _borderOffset = 0.4f;
        private float _returnInFieldSpeed = 40;

        private void Update()
        {
            InputScroll();
            ChangeZoom();
            MoveCamera();
        }

        private void InputScroll()
        {
            _zoom -= Input.mouseScrollDelta.y * _scrollSpeed;
            _zoom = Mathf.Clamp01(_zoom);
            _lerpScroll = Mathf.MoveTowards(_lerpScroll, _zoom, Time.deltaTime * _lerpScrollSpeed);
        }

        private void ChangeZoom()
        {
            var height = Mathf.Lerp(_lowPosition.Height, _highPosition.Height, _lerpScroll);
            transform.position = new(transform.position.x, height, transform.position.z);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(_lowPosition.Rotation, _highPosition.Rotation, _lerpScroll));
        }

        private void MoveCamera()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _mousePositionDelta = Input.mousePosition;
            }
            if (Input.GetMouseButton(1))
            {
                var delta = (_mousePositionDelta - Input.mousePosition) * GetCameraSensitivityByDistance();
                var newPosition = transform.position + new Vector3(delta.x, 0, delta.y);
                transform.position = newPosition;
                _mousePositionDelta = Input.mousePosition;
            }
            else if (new Vector2(transform.position.x, transform.position.z).magnitude > GetMapSizeByZoom())
            {
                var fieldCenter = new Vector3(0, transform.position.y, 0);
                transform.position = Vector3.MoveTowards(transform.position, fieldCenter, Time.deltaTime * _returnInFieldSpeed);
            }
        }

        private float GetCameraSensitivityByDistance()
        {
            if (GetMapSizeByZoom() == 0)
                return 0;
            var distance = 1 - new Vector2(transform.position.x, transform.position.z).magnitude / GetMapSizeByZoom() + _borderOffset;
            var currentSensitivity = Mathf.Lerp(0, _cameraSensitivity, distance);
            return currentSensitivity;
        }

        private float GetMapSizeByZoom()
        {
            var mapSize = Mathf.Lerp(0, _mapSize, 1 - _zoom);
            return mapSize;
        }
    }
}