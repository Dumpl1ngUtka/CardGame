using Battleground;
using UnityEngine;

namespace UI.Marker
{
    public class MeleeMarker : Marker
    {
        [SerializeField] private GameObject _targetObject;
        [SerializeField] private float Mult;        

        private MeleeAttackZone _hitBox;
        private Vector3 _startPos;
        private LayerMask _layerMask;

        protected override LayerMask Mask => _layerMask;

        public void Init(Piece piece, LayerMask layerMask, MeleeAttackZone obj, Vector3 startPos)
        {
            _layerMask = layerMask;
            _hitBox = obj;
            transform.position = startPos;
            _startPos = startPos;
            _targetObject.GetComponent<MeshFilter>().sharedMesh = _hitBox.HitboxForm.GetComponent<MeshFilter>().sharedMesh;
            _targetObject.transform.localPosition = new Vector3(0, 0, 0);
            _targetObject.transform.localScale *= piece.Attributes.MeleeAttackRangePercent / 100;
        }

        protected override void Render(RaycastHit point)
        {
            transform.rotation = Quaternion.Euler(_hitBox.GetHitBoxRotation(_startPos, point.point));
        }
    }
}

