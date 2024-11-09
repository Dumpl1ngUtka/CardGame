using AI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battleground
{
    public abstract class PieceState : ScriptableObject
    {
        private float _timer = 0f;

        protected SituationAnalyzer SituationAnalyzer;
        protected PieceStateMachine StateMachine;

        protected abstract float MinStateTime { get; }
        protected abstract float MaxStateTime { get; }
        protected abstract List<PieceAbility> AvailableAbilityList { get; }
        public abstract Transform Target { get; }

        protected Piece Piece => StateMachine.Piece;
        protected IAIWeightPoint SelfWeight => StateMachine;
        protected List<PieceState> TransitionStates => StateMachine.TransitionStates;
        protected bool IsStateCanBeChanged => _timer >= MinStateTime;
        protected bool IsStateMustBeChanged => _timer >= MaxStateTime;

        public virtual void Init(PieceStateMachine pieceStateMachine)
        {
            StateMachine = pieceStateMachine;
            SituationAnalyzer = pieceStateMachine.SituationAnalyzer;
        }

        public virtual void Update()
        {
            _timer += Time.deltaTime;
            if (IsStateCanBeChanged)
            {
                CheckTransitionConditions(out PieceState nextState, out PieceState _);
                if (nextState != this)
                {
                    StateMachine.ChangeState(nextState);
                    return;
                }
                if (CheckTransitionAbilites(out PieceAbility ability))
                {
                    StateMachine.ChangeState(ability);
                    return;
                }
            }
            if (IsStateMustBeChanged)
            {
                CheckTransitionConditions(out PieceState firstState, out PieceState secondState);
                StateMachine.ChangeState(firstState != this? firstState : secondState);
                return;
            }
        }

        private void CheckTransitionConditions(out PieceState bestState, out PieceState secondState)
        {
            var list = TransitionStates.OrderByDescending(x => x.GetMetric(SituationAnalyzer)).ToList();
            bestState = list[0];
            secondState = list[1];
        }

        private bool CheckTransitionAbilites(out PieceAbility bestAbility)
        {
            var bestMextix = GetMetric(SituationAnalyzer);
            bestAbility = null;

            foreach (var ability in AvailableAbilityList)
                if (ability.GetMetric(SituationAnalyzer) > bestMextix)
                    bestAbility = ability;

            return bestAbility != null;
        }

        public virtual void Enter(PieceState previousState) 
        {
            _timer = 0f;
        }

        public virtual void Exit() { }

        public abstract float GetMetric(SituationAnalyzer situationAnalyzer);
    }
}