using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

namespace Battleground
{
    public class CameraModeChanger : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _lookAtCamera;
        [SerializeField] private FreeCameraPivotMover _freeCameraPivot;
        private ICameraPivot _pivot;
        private PlayerInput _inputActions;
        #region Zoom
        private List<float[]> _orbitsHeightsAndRadiuses;
        private float _minZoom = 0.5f;
        private float _maxZoom = 2f;
        [SerializeField] private float _zoomSensitivity = 0.5f;
        private float _zoomValue;
        #endregion


        private void OnEnable()
        {
            _inputActions = new PlayerInput();
            _inputActions.Enable();
            _orbitsHeightsAndRadiuses = GetOrbitsRadiuses();
            SetDefaultPivot();
        }

        private void Update()
        {
            if (_pivot != null)
            {
                if (_inputActions.Battle.CameraMove.IsPressed() 
                    || _inputActions.Battle.CameraVecticalMove.IsPressed()
                    || Input.GetMouseButtonDown(2))
                    SetDefaultPivot();
                var scroll = _inputActions.Battle.CameraScroll.ReadValue<float>();
                if (scroll != 0)
                {
                    _zoomValue += -scroll * _zoomSensitivity * Time.deltaTime;
                    _zoomValue = Mathf.Clamp(_zoomValue, _minZoom, _maxZoom);
                    SetOrbitsRadiuses(_zoomValue);
                }
            }
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public void SetPivot(ICameraPivot pivot)
        {
            _lookAtCamera.enabled = true;
            _pivot = pivot;
            _lookAtCamera.Follow = pivot.PivotTransform;
            _lookAtCamera.LookAt = pivot.PivotTransform;

        }

        public void SetDefaultPivot()
        {
            _lookAtCamera.enabled = false;
            _pivot = null;
        }

        private List<float[]> GetOrbitsRadiuses()
        {
            var radiuses = new List<float[]>();
            foreach (var orbit in _lookAtCamera.m_Orbits)
            {
                radiuses.Add(new float[2] { orbit.m_Height, orbit.m_Radius });
            }
            return radiuses;
        }

        private void SetOrbitsRadiuses(float zoomValue)
        {
            for (int i = 0; i < _lookAtCamera.m_Orbits.Length; i++)
            {
                _lookAtCamera.m_Orbits[i].m_Height = _orbitsHeightsAndRadiuses[i][0] * Mathf.Max(zoomValue, 0.5f);
                _lookAtCamera.m_Orbits[i].m_Radius = _orbitsHeightsAndRadiuses[i][1] * zoomValue;
            }   
        }
    }
}

