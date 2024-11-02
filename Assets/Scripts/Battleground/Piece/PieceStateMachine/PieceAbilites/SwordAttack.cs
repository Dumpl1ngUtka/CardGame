using AI;
using Units;
using UnityEngine;

namespace Battleground
{
    [CreateAssetMenu(menuName = "Ability/SwordAttack")]
    public class SwordAttack : PieceAbility, IDamageAbility
    {
        [SerializeField] private float _attackDistance = 3f;
        public override float ReleaseTime => 2;
        public override float Cooldown => 5;
        public override bool IsCanMoveWhileUse => false;
        public float Damage => 5f;
        public float DPM => Damage * (60/(Cooldown + ReleaseTime));

        public override float GetMetric(SituationAnalyzer situationAnalyzer)
        {
            if (!IsReadyToUse)
                return 0;
            if (Vector3.Distance(situationAnalyzer.ClosestEnemy.Position, situationAnalyzer.SelfWeight.Position) < _attackDistance)
            {
                return 1f;
            }
            return 0f;
        }

        protected override void Release()
        {
            Debug.Log("");
        }
    }
}
