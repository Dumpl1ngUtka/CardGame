using AI;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public abstract class PieceState
    {
        private float _timer;

        protected SituationAnalyzer SituationAnalyzer;
        protected PieceStateMachine StateMachine;
        protected IAIWeightPoint SelfWeight;

        protected Piece Piece => StateMachine.Piece;
        protected List<PieceState> TransitionStates => StateMachine.TransitionStates;
        protected abstract float MinStateTime { get; }

        public PieceState(PieceStateMachine pieceStateMachine)
        {
            StateMachine = pieceStateMachine;
            SituationAnalyzer = pieceStateMachine.SituationAnalyzer;
            SelfWeight = pieceStateMachine.Piece;
        }

        public virtual void Update()
        {
            if (_timer < MinStateTime)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                var nextState = CheckTransitionConditions();
                Debug.Log(nextState);
                if (nextState != this)
                    StateMachine.ChangeState(nextState);
            }
        }

        private PieceState CheckTransitionConditions()
        {
            PieceState nextState = null;
            var topMetrix = 0f;
            foreach (var state in TransitionStates)
            {
                var metrix = state.GetMetric(SituationAnalyzer);
                if (metrix > topMetrix)
                {
                    nextState = state;
                    topMetrix = metrix;
                }
            }
            return nextState;
        }

        public virtual void Enter() { }

        public virtual void Exit() { }

        public abstract float GetMetric(SituationAnalyzer situationAnalyzer);
    }
}