using UnityEngine;
using Cinemachine;

namespace Battleground
{
    public class CameraModeChanger : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _lookAtCamera;
        [SerializeField] private FreeCameraPivotMover _freeCameraPivot;
        private ICameraPivot _pivot;

        private void Start()
        {
            SetDefaultPivot();
            
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
    }
}

