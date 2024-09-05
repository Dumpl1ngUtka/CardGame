using Battleground;
using UnityEngine;

namespace UI.Marker
{
    public class Marker : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        private Vector3 _piecePosition;
        private LayerMask _layerMask;

        public void Init(LayerMask layerMask, Vector3 piecePosition)
        {
            _layerMask = layerMask;
            _piecePosition = piecePosition;
        }

        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                _lineRenderer.SetPosition(0, _piecePosition);
                _lineRenderer.SetPosition(1, hit.point);
            }
        }
    }
}

