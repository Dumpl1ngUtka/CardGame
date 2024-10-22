using UnityEngine;

namespace Battleground
{
    public class Default : PlayerState
    {
        public Default(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override LayerMask LayerMask => ~0;

        public override void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Escape))
            //    StateMachine.ChangeState(new PauseMenu(StateMachine));

            base.Update();
        }

        protected override void LeftMouseButtonDown(RaycastHit hit)
        {
            //if (hit.collider.TryGetComponent<Piece>(out var piece))
            //    StateMachine.ChangeState(new SelectSpellCard(StateMachine, piece));
            if (hit.collider.TryGetComponent(out ICameraPivot pivot))
                StateMachine.CameraMode.SetPivot(pivot);
        }

        protected override void RightMouseButtonDown(RaycastHit hit)
        {
        }
    }
}