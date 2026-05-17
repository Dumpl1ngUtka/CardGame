using AI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battleground
{
    public class FollowToEnemy : PieceState
    {
        private const float _updateTime = 0.5f;
        private float _timer;
        private IAIWeightPoint _target;
        private List<PieceAbility> _availableAbilities;

        protected override float MinStateTime => 0;
        protected override float MaxStateTime => float.PositiveInfinity;
        protected override List<PieceAbility> AvailableAbilityList => _availableAbilities;
        public override IAIWeightPoint Target => GetTarget();

        public override void Enter(PieceState previousState)
        {
            base.Enter(previousState);
            _availableAbilities = StateMachine.DamageAbilites.Cast<PieceAbility>().ToList();
            _target = previousState.Target;
            Piece.UI.ChangeGroundIndicator(Color.red);
        }

        private IAIWeightPoint GetTarget()
        {
            var target = SituationAnalyzer.ClosestEnemy;
            return target;
        }

        public override float GetMetric(SituationAnalyzer situationAnalyzer)
        {
            var metrix = 0f;
            if (SituationAnalyzer.StrongestEnemy != null)
            {
                metrix = SelfGroupWeight.DangerWeight / SituationAnalyzer.StrongestEnemy.DangerWeight;
            }
            return metrix;
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
                if (Target != null && Target.IsValid)
                {
                    var direction = Target.Position - SelfWeight.Position;
                    Piece.MoveTo(direction);
                }
            }
        }
    }
}

