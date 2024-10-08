using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class SimpleZone : SpellObject
    {
        [SerializeField] private float _damagePeriod;
        private List<IDamageable> _objectsInZone;
        private int damageCount;

        public void Init(Piece piece, Vector3 endPos, float startTime)
        {
            base.Init(piece, startTime);
            transform.position = endPos;
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

        public override void NextMove()
        {
        }
    }
}

