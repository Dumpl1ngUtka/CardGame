using AI;
using UnityEngine;

namespace Battleground
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Missile : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private float _damage;

        public void Init(float damage)
        {
            _damage = damage;
        }

        public void AddForceToTarget(IAIWeightPoint target, float forceValue, MissileMode mode = MissileMode.Physics)
        {
            var direction = target.Position - transform.position;
            direction.Normalize();
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.AddForce(100 * forceValue * direction);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(new Damage(_damage));
            }
            Destroy(gameObject);
        }
    }

    public enum MissileMode
    {
        Physics,
        ConstantSpeed,
    }
}