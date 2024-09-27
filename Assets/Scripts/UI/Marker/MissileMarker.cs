using Battleground;
using UnityEngine;

namespace UI.Marker
{
    public class MissileMarker : Marker
    {
        [SerializeField] private LineRenderer _lineRenderer;
        private SimpleMissile _obj;
        private Vector3 _startPos;
        private const int StepCount = 15;
        private LayerMask _layerMask;

        protected override LayerMask Mask => _layerMask;

        public void Init(LayerMask layerMask, SimpleMissile prefab, Vector3 startPos)
        {
            _layerMask = layerMask;
            _obj = prefab;
            _startPos = startPos;
            _lineRenderer.positionCount = StepCount;
        }

        protected override void Render(RaycastHit point)
        {
            for (int i = 0; i < StepCount; i++)
            {
                _lineRenderer.SetPosition(i, _obj.GetPositionByProgress(_startPos, point.point, (float)i / StepCount));
            }
        }
    }
}

