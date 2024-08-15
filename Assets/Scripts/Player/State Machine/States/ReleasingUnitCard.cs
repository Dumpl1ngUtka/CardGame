using Units;
using UnityEngine;

namespace Battleground
{
    public class ReleasingUnitCard : State
    {
        private Unit _unit;

        public ReleasingUnitCard(PlayerStateMachine stateMachine, Unit unit) : base(stateMachine)
        {
            _unit = unit;
        }

        public override LayerMask LayerMask => LayerMask.GetMask(GroundLayer, PlayerUnitLayer);

        public override void Enter()
        {
            base.Enter();
            StateMachine.UI.ShowInfo(_unit);
        }

        public override void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                StateMachine.ChangeState(new SelectUnitCard(StateMachine));

            base.Update();
        }

        protected override void LeftMouseButtonDown(RaycastHit hit)
        {
            if (hit.collider.GetComponent<Piece>() == null)
            {
                StateMachine.Player.InstantiatePiece(_unit,hit);
                StateMachine.ChangeState(new SelectUnitCard(StateMachine));
            }
        }

        protected override void RightMouseButtonDown(RaycastHit hit)
        {

        }
    }

}
