using Battleground.UI;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battleground
{
    public class SelectCard : PlayerState
    {
        public override LayerMask LayerMask => LayerMask.GetMask(PlayerUnitLayer, EnemyUnitLayer, CardLayer,UILayer);
        
        public SelectCard(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            StateMachine.Player.SetCardsVisable(true);
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && StateMachine.Player.HasPlayablePiece || StateMachine.Player.IsUnitsListEmpty)
                StateMachine.ChangeState(new Default(StateMachine));

            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            StateMachine.Player.SetCardsVisable(false);
        }

        protected override void LeftMouseButtonDown(RaycastHit hit)
        {

        }

        public override void LeftMouseButtonDownOverUI(RaycastResult hit)
        {
            hit.gameObject.TryGetComponent<UICard>(out var card);
            if (card == null)
                return;

            if (card.ObjectForUICard as Spell)
            {
                StateMachine.ChangeState(new ReleasingCard(StateMachine, card));
            }
        }

        protected override void RightMouseButtonDown(RaycastHit hit)
        {
        }
    }
}
