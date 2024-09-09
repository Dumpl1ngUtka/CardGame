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

        public override void Init(SpellObject obj, Vector3 startPos)
        {
            _obj = obj as SimpleMissile;
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

