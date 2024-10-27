using AI;
using UnityEngine;

namespace Battleground
{
    public class FollowToEnemy : PieceState
    {
        private Vector3 _direction;
        private float _updateTime = 0.5f;
        private float _timer;
        private IAIWeightPoint _target;

        public FollowToEnemy(PieceStateMachine pieceStateMachine, IAIWeightPoint target) : base(pieceStateMachine)
        {
            _target = target;
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
                _direction = (_target.Position - SelfWeight.Position).normalized;
                Piece.MoveTo(_target.Position);
            }
        }

        protected override PieceState CheckTransitionConditions()
        {
            if (StateMachine.SituationAnalyzer.WeightPoints.Count == 0)
            {
                return new WalkAlone(StateMachine);
            }
            foreach (var weightPoint in StateMachine.SituationAnalyzer.WeightPoints)
            {
                if (SelfWeight.DamagePerMinute > weightPoint.DamagePerMinute)
                {
                    return new FollowToEnemy(StateMachine, weightPoint);
                }
                else
                {
                    return new RunAway(StateMachine);
                }
            }
            return null;
        }
    }
}

