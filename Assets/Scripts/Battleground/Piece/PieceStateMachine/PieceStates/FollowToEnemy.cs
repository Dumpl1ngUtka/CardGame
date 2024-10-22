using UnityEngine;

namespace Battleground
{
    public class FollowToEnemy : PieceState
    {
        private Vector3 _direction;
        private float _updateTime = 0.5f;
        private float _timer;

        public FollowToEnemy(PieceStateMachine pieceStateMachine) : base(pieceStateMachine)
        {
            Piece.Stop();
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
                foreach (var sector in StateMachine.SituationAnalyzer.DPSMatrix.GetSectorAmounts())
                {
                    if (sector.Value < SelfWeight.DamagePerMinute)
                    {
                        var vector = Quaternion.Euler(0, sector.Key, 0) * Piece.Transform.forward;
                        Debug.Log(vector);
                        Piece.MoveTo(vector.normalized * 10);
                        break;
                    }
                }
            }
        }

        protected override PieceState CheckTransitionConditions()
        {
            return null;
        }
    }
}

