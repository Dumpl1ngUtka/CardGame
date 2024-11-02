using AI;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public abstract class PieceState
    {
        private float _timer = 0f;
        private PieceAbility _currentAbility;

        protected SituationAnalyzer SituationAnalyzer;
        protected PieceStateMachine StateMachine;
        protected IAIWeightPoint SelfWeight;

        protected Piece Piece => StateMachine.Piece;
        protected List<PieceState> TransitionStates => StateMachine.TransitionStates;
        protected bool IsStateCanBeChanged => (_timer >= MinStateTime) && !IsAbilityUsed;
        protected bool IsAbilityUsed => _currentAbility != null;
        protected abstract float MinStateTime { get; }
        protected abstract List<PieceAbility> AvailableAbilityList { get; }

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
            if (IsStateCanBeChanged)
            {
                var nextState = CheckTransitionConditions();
                if (nextState != this)
                {
                    StateMachine.ChangeState(nextState);
                    return;
                }
            }
            if (IsAbilityUsed && _currentAbility.IsUsed)
            {
                _currentAbility.Update();
                return;
            }
            else if (IsAbilityUsed && !_currentAbility.IsUsed)
            {
                _currentAbility.EndRelease();
                _currentAbility = null;
            }
            else
            {
                var ability = CheckAbilityTransition();
                if (ability != null)
                {
                    StartUseAbility(ability);
                    return;
                }
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

        private PieceAbility CheckAbilityTransition()
        {
            PieceAbility nextAbility = null;
            var topMetrix = 1f;
            foreach (var ability in AvailableAbilityList)
            {
                var metrix = ability.GetMetric(SituationAnalyzer);
                if (metrix > topMetrix)
                {
                    nextAbility = ability;
                    topMetrix = metrix;
                }
            }
            return nextAbility;
        }

        public virtual void Enter() 
        {
            _timer = 0f;
        }

        public virtual void Exit() { }

        public abstract float GetMetric(SituationAnalyzer situationAnalyzer);

        private void StartUseAbility(PieceAbility ability)
        {
            _currentAbility = ability;
        }
    }
}