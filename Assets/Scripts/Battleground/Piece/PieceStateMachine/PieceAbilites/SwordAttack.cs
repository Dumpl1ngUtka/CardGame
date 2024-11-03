using AI;
using Units;
using UnityEngine;

namespace Battleground
{
    [CreateAssetMenu(menuName = "Ability/SwordAttack")]
    public class SwordAttack : PieceAbility, IDamageAbility
    {
        [SerializeField] private float _attackDistance = 3f;
        [SerializeField] private float _damage = 5f;
        public float Damage => _damage;
        public float DPM => Damage * (60/(Cooldown + ReleaseTime));

        public override float GetMetric(SituationAnalyzer situationAnalyzer)
        {
            if (!IsReadyToUse)
                return 0f;
            if (situationAnalyzer.ClosestEnemy == null)
                return 0f;
            if (Vector3.Distance(situationAnalyzer.ClosestEnemy.Position, situationAnalyzer.SelfWeight.Position) < _attackDistance)
            {
                return 1f;
            }
            return 0f;
        }

        protected override void Release()
        {
            Debug.Log("attack");
        }
    }
}
