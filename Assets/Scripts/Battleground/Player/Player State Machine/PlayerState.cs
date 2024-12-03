using UnityEngine;
using UnityEngine.EventSystems;

namespace Battleground
{
    public abstract class PlayerState
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
        public RaycastHit LastHit { get; private set; }

        public PlayerState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void Enter() { }

        public virtual void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask))
            {
                if (Input.GetMouseButtonDown(0))
                    LeftMouseButtonDown(hit);

                if (Input.GetMouseButtonDown(1))
                    RightMouseButtonDown(hit);

                LastHit = hit;
            }
        }

        public virtual void Exit() { }

        protected abstract void LeftMouseButtonDown(RaycastHit hit);

        protected abstract void RightMouseButtonDown(RaycastHit hit);

        public virtual void LeftMouseButtonDownOverUI(RaycastResult hit) { }

    }
}
