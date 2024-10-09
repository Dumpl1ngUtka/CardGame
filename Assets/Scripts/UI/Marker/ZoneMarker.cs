using Battleground;
using UnityEngine;

namespace UI.Marker
{
    public class ZoneMarker : Marker
    {
        [SerializeField] private GameObject _pointer;
        private SimpleZone _simpleZone;
        private LayerMask _layerMask;
        private Piece _piece;

        protected override LayerMask Mask => _layerMask;


        public void Init(Piece piece, LayerMask layerMask, SimpleZone obj)
        {
            _piece = piece;
            _layerMask = layerMask;
            _simpleZone = obj;
            _pointer.GetComponent<MeshFilter>().sharedMesh = _simpleZone.HitboxForm.GetComponent<MeshFilter>().sharedMesh;
            _pointer.transform.localScale = Vector3.one * _simpleZone.Diameter;
        }

        protected override void Render(RaycastHit point)
        {
            transform.position = _simpleZone.GetHitBoxPosition(_piece.transform.position, point.point);
        }
    }
}

