using AI;
using Units;
using UnityEngine;

namespace Battleground
{
    [CreateAssetMenu(menuName = "Ability/SwordAttack")]
    public class SwordAttack : PieceAbility, IDamageAbility
    {
        [Header("Sword Attack")]
        [SerializeField] private float _attackDistance = 3f;
        [SerializeField] private float _damage = 5f;
        public float Damage => _damage;
        public float DPM => Damage * (60/(Cooldown + ReleaseTime));
        public override IAIWeightPoint Target => PriviousState.Target;
        public PieceAbility Ability => this;
        public float PerfectDistance => _attackDistance;

        public override float GetMetric(SituationAnalyzer situationAnalyzer)
        {
            if (!IsReadyToUse)
            {
                return 0f;
            }
            if (situationAnalyzer.ClosestEnemy == null)
            {
                return 0f;
            }
            if (Vector3.Distance(situationAnalyzer.ClosestEnemy.Position, situationAnalyzer.SelfWeight.Position) < _attackDistance)
            {
                return 3f;
            }
            return 0f;
        }

        public override void Enter(PieceState pieceState)
        {
            base.Enter(pieceState);
            Piece.UI.ChangeGroundIndicator(Color.yellow);
        }

        public override void Update()
        {
            base.Update();
            Piece.LookTo(Target.Position - SelfWeight.Position);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
