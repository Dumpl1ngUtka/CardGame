using AI;
using Units;
using UnityEngine;

namespace Battleground
{
    [CreateAssetMenu(menuName = "Ability/Fireball")]
    public class Fireball : PieceAbility, IDamageAbility
    {
        [SerializeField] private float _attackDistance = 15f;
        [SerializeField] private float _damage = 7f;
        public float Damage => _damage;
        public float DPM => Damage * (60 / (Cooldown + ReleaseTime));
        public override Transform Target => PriviousState.Target;
        public PieceAbility Ability => this;

        public override float GetMetric(SituationAnalyzer situationAnalyzer)
        {
            if (!IsReadyToUse)
                return 0f;
            if (situationAnalyzer.ClosestEnemy == null)
                return 0f;
            var distanceToTarget = Vector3.Distance(situationAnalyzer.ClosestEnemy.Position, situationAnalyzer.SelfWeight.Position);
            if (distanceToTarget < _attackDistance)
            {
                return Mathf.Lerp(0.5f, 1.5f, distanceToTarget/ _attackDistance);
            }
            return 0f;
        }

        public override void Enter(PieceState pieceState)
        {
            base.Enter(pieceState);
            Piece.Animator.Play("MagicSpell");
            Piece.UI.ChangeGroundIndicator(Color.blue);
        }

        public override void Update()
        {
            base.Update();
            Debug.Log("attack");
        }

        public override void Exit()
        {
            base.Exit();
            Piece.Animator.SetTrigger("Stop");
        }
    }
}

