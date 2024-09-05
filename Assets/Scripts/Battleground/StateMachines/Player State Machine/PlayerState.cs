using UnityEngine;

namespace Battleground
{
    public abstract class PlayerState : State
    {
        #region LayersName
        protected const string PlayerUnitLayer = "Player Unit";
        protected const string EnemyUnitLayer = "Enemy Unit";
        protected const string GroundLayer = "Ground";
        protected const string CardLayer = "Card";
        protected const string UILayer = "UI";
        #endregion

        protected PlayerStateMachine StateMachine;
        public abstract LayerMask LayerMask { get; }

        public PlayerState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public override void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask))
            {
                if (Input.GetMouseButtonDown(0))
                    LeftMouseButtonDown(hit);

                if (Input.GetMouseButtonDown(1))
                    RightMouseButtonDown(hit);
            }
        }

        protected abstract void LeftMouseButtonDown(RaycastHit hit);

        protected abstract void RightMouseButtonDown(RaycastHit hit);
    }
}
