using AI;
using UnityEngine;

namespace Battleground
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Missile : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        public void AddForceToTarget(IAIWeightPoint target, float forceValue, MissileMode mode = MissileMode.Physics)
        {
            var direction = target.Position - transform.position;
            direction.Normalize();
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.AddForce(100 * forceValue * direction);
        }
    }

    public enum MissileMode
    {
        Physics,
        ConstantSpeed,
    }
}