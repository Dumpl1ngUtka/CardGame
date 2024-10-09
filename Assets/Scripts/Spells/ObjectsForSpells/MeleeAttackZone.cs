using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground
{
    public class MeleeAttackZone : SpellObject
    {
        [SerializeField, Min(0)] private float _offset;
        [SerializeField, Min(0)] private float _hitboxSize = 1;
        private List<IDamageable> _collidedObjects;

        public float Offset => _offset;
        public float HitboxSize => _hitboxSize * _piece.Attributes.MeleeAttackRangePercent / 100;
        public GameObject HitboxForm => _hitbox.gameObject;

        public void Init(Piece piece, MeleeAttackSpell spell,Vector3 direction,float startTime)
        {
            base.Init(piece, startTime);
            transform.position = piece.transform.position;
            HitboxForm.transform.localScale = Vector3.one * HitboxSize;
            HitboxForm.transform.localPosition = new Vector3(0, 0, Offset);
            transform.rotation = Quaternion.Euler(GetHitBoxRotation(transform.position, direction));
            _collidedObjects = new List<IDamageable>();
        }

        public override void MoveByTimeline(float time, bool isSimulation = false)
        {
            base.MoveByTimeline(time, isSimulation);

            if (isSimulation)
            {
                foreach (var collidedObject in _collidedObjects)
                    collidedObject.ApplyDamage(new Damage(_damage, collidedObject, _piece));
                Destroy(gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable obj) && !_collidedObjects.Contains(obj))
                _collidedObjects.Add(obj);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamageable obj) && _collidedObjects.Contains(obj))
                _collidedObjects.Remove(obj);
        }
        public Vector3 GetHitBoxRotation(Vector3 startPos, Vector3 pointPosition)
        {
            var angle = Vector3.Angle((Vector3.forward), (pointPosition - startPos).normalized);
            var sign = pointPosition.x > startPos.x ? 1 : -1;
            return new Vector3(0, sign * angle, 0);
        }
    }
}

