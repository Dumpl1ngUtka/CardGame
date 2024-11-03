using AI;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class FollowToEnemy : PieceState
    {
        private const float _updateTime = 0.5f;
        private float _timer;
        private IAIWeightPoint _target;
        private List<PieceAbility> _availableAbilities;

        protected override float MinStateTime => 3;
        protected override List<PieceAbility> AvailableAbilityList => _availableAbilities;

        public FollowToEnemy(PieceStateMachine pieceStateMachine) : base(pieceStateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            foreach (var weightPoint in StateMachine.SituationAnalyzer.WeightPoints)
            {
                if (SelfWeight.DamagePerMinute > weightPoint.DamagePerMinute)
                {
                    _target = weightPoint;
                    break;
                }
            }
            _availableAbilities = Piece.DamageAbilites;
            Debug.Log(_availableAbilities);
        }

        public override float GetMetric(SituationAnalyzer situationAnalyzer)
        {
            foreach (var weightPoint in StateMachine.SituationAnalyzer.WeightPoints)
            {
                if (SelfWeight.DamagePerMinute > weightPoint.DamagePerMinute)
                {
                    return 1;
                }
            }
            return 0;
        }

        public override void Update()
        {
            base.Update();
            if (_timer < _updateTime)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                _timer = 0f;
                Piece.MoveTo(_target.Position);
            }
        }
    }
}

