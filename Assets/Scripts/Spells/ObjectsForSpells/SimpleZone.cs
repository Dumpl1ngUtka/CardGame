using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground
{
    public class SimpleZone : SpellObject
    {
        [SerializeField] private float _damagePeriod;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _diameter;
        private List<IDamageable> _objectsInZone;
        private int damageCount;

        public float MaxDistance
        {
            get 
            {
                var value = _maxDistance;
                if (_piece != null)
                    value *= _piece.Attributes.DistanceAttackDistancePercent / 100;
                return value; 
            }
        }
        public float Diameter
        {
            get
            {
                var value = _diameter;
                if (_piece != null)
                    value *= _piece.Attributes.DistanceAttackRangePercent / 100;
                return value;
            }
        }
        public GameObject HitboxForm => _hitbox.gameObject;

        public void Init(Piece piece, Vector3 endPos, float startTime)
        {
            base.Init(piece, startTime);
            transform.position = GetHitBoxPosition(piece.transform.position, endPos);
            transform.localScale = Vector3.one * Diameter;
        }

        public override void MoveByTimeline(float time, bool isSimulation = false)
        {
            base.MoveByTimeline(time, isSimulation);

            if (isSimulation)
            {
                if (time > _damagePeriod * damageCount)
                {
                    foreach (var obj in _objectsInZone)
                        obj.ApplyDamage(new Damage(_damage, obj));
                    damageCount++;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out IDamageable damageable);
            if (damageable != null)
                _objectsInZone.Add(damageable);
        }

        private void OnTriggerExit(Collider other)
        {
            other.TryGetComponent(out IDamageable damageable);
            if (damageable != null)
                _objectsInZone.Remove(damageable);
        }

        public Vector3 GetHitBoxPosition(Vector3 startPos, Vector3 endPos)
        {
            var dir = endPos - startPos;
            var distance = Mathf.Min(dir.magnitude, MaxDistance);
            return startPos + (dir.normalized * distance);
        }

        public override void NextMove()
        {
        }
    }
}

