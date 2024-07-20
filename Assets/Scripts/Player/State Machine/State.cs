using UnityEngine;

namespace Battleground
{
    public abstract class State
    {
        #region LayersName
        protected const string PlayerUnitLayer = "Player Unit";
        protected const string EnemyUnitLayer = "Enemy Unit";
        protected const string GroundLayer = "Ground";
        protected const string CardLayer = "Card";
        protected const string UILayer = "UI";
        #endregion

        protected StateMachine StateMachine;
        
        public abstract LayerMask LayerMask { get; }

        public State(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void Enter() { }

        public virtual void Update()
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

        public virtual void Exit() { }

        protected abstract void LeftMouseButtonDown(RaycastHit hit);

        protected abstract void RightMouseButtonDown(RaycastHit hit);
    }
}
