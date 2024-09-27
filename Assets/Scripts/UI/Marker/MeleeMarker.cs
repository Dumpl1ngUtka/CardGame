using Battleground;
using UnityEngine;

namespace UI.Marker
{
    public class MeleeMarker : Marker
    {
        [SerializeField] private GameObject _targetObject;
        
        private MeleeAttackZone _hitBox;
        private Vector3 _startPos;
        private LayerMask _layerMask;

        protected override LayerMask Mask => _layerMask;

        public void Init(LayerMask layerMask, MeleeAttackZone obj, Vector3 startPos)
        {
            _layerMask = layerMask;
            _hitBox = obj;
            transform.position = startPos;
            _startPos = startPos;
            Debug.Log(_hitBox);
            _targetObject.GetComponent<MeshFilter>().sharedMesh = _hitBox.HitboxForm.GetComponent<MeshFilter>().sharedMesh;
            _targetObject.transform.localPosition = new Vector3(0, 0, _hitBox.Offset);
            _targetObject.transform.localScale *= _hitBox.SizeMultiply;
        }

        protected override void Render(RaycastHit point)
        {
            transform.rotation = Quaternion.Euler(_hitBox.GetHitBoxRotation(_startPos, point.point));
        }
    }
}

