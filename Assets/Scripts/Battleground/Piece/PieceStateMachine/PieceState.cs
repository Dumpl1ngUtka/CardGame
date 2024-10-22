using AI;
using UnityEngine;

namespace Battleground
{
    public abstract class PieceState
    {
        private float _minStateTime = 5f;
        private float _timer;
        protected SituationAnalyzer SituationAnalyzer;
        protected PieceStateMachine StateMachine;
        protected IAIWeightPoint SelfWeight;
        protected Piece Piece => StateMachine.Piece;

        public PieceState(PieceStateMachine pieceStateMachine)
        {
            StateMachine = pieceStateMachine;
            SituationAnalyzer = pieceStateMachine.SituationAnalyzer;
            SelfWeight = pieceStateMachine.Piece;
        }

        public virtual void Update()
        {
            if (_timer < _minStateTime)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                var nextState = CheckTransitionConditions();
                if (nextState != null)
                    StateMachine.ChangeState(nextState);
            }
        }

        protected abstract PieceState CheckTransitionConditions();
    }
}