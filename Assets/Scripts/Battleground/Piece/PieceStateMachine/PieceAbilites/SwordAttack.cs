using AI;
using Units;
using UnityEngine;

namespace Battleground
{
    [CreateAssetMenu(menuName = "Ability/SwordAttack")]
    public class SwordAttack : PieceAbility, IDamageAbility, IMoveAbility
    {
        [SerializeField] private float _attackDistance = 3f;
        [SerializeField] private float _damage = 5f;
        public float Damage => _damage;
        public float DPM => Damage * (60/(Cooldown + ReleaseTime));
        public Transform Target => CallingState.Target;
        public PieceAbility Ability => this;

        public float DistancePerSecond => throw new System.NotImplementedException();

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
                return 1f;
            }
            return 0f;
        }

        public override void StartRelease(PieceState pieceState)
        {
            base.StartRelease(pieceState);
            pieceState.Piece.Animator.Play("SwordAttack");
        }

        protected override void Release()
        {
            Debug.Log("attack");
        }

        public override void EndRelease()
        {
            base.EndRelease();
            CallingState.Piece.Animator.SetTrigger("Stop");
        }
    }
}
