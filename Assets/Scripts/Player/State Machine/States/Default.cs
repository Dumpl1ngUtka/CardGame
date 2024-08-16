using UnityEngine;

namespace Battleground
{
    public class Default : State
    {
        public Default(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override LayerMask LayerMask => LayerMask.GetMask(PlayerUnitLayer, EnemyUnitLayer);

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                StateMachine.ChangeState(new PauseMenu(StateMachine));

            base.Update();
        }

        protected override void LeftMouseButtonDown(RaycastHit hit)
        {
            if (!hit.collider.TryGetComponent<Piece>(out var piece))
                return;

            StateMachine.ChangeState(new SelectSpellCard(StateMachine, piece));
        }

        protected override void RightMouseButtonDown(RaycastHit hit)
        {
            throw new System.NotImplementedException();
        }
    }
}