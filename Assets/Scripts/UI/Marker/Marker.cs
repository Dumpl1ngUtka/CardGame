using Battleground;
using System.IO;
using Units;
using UnityEngine;

namespace UI.Marker
{
    public class Marker : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        private Fireball _obj;
        private Vector3 _startPos;
        private const int StepCount = 15;

        public void Init(Fireball obj, Vector3 startPos)
        {
            _obj = obj;
            _startPos = startPos;
            _lineRenderer.positionCount = StepCount;
        }

        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                for (int i = 0; i < StepCount; i++)
                {
                    _lineRenderer.SetPosition(i, _obj.GetPositionByProgress(_startPos, hit.point, (float)i / StepCount));
                }
            }
        }
    }
}

