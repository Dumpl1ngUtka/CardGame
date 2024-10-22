using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class WalkAlone : PieceState
    {
        #region Const
        private const float _minDistanceToCorner = 0.5f;
        #endregion

        private List<Vector3> _path;
        private int _pathCornerIndex;
        private int _currentMapAnchor;
        private Transform[] _anchors;

        public WalkAlone(PieceStateMachine pieceStateMachine) : base(pieceStateMachine)
        {
            _anchors = Piece.Player.Map.Anchors;
            _currentMapAnchor = 0;
            SetNewPath();
        }

        public override void Update()
        {
            base.Update();
            if (_pathCornerIndex < _path.Count)
                MoveByPath();
            else
                SetNewPath();
        }

        private void SetNewPath()
        {
            _pathCornerIndex = 0;
            _path = new List<Vector3>();

            var randomPoint = Random.Range(0, _anchors.Length - 1);
            _currentMapAnchor += randomPoint;
            if (_currentMapAnchor >= _anchors.Length)
                _currentMapAnchor -= _anchors.Length;

            var targetPoint = _anchors[_currentMapAnchor];
            Piece.Agent.destination = targetPoint.position;
            _path.AddRange(Piece.Agent.path.corners);
            _path.Add(targetPoint.position);
        }

        private void MoveByPath()
        {
            if (Vector3.Distance(_path[_pathCornerIndex], Piece.Position) > _minDistanceToCorner)
                Piece.MoveTo(_path[_pathCornerIndex]);
            else
                _pathCornerIndex++;
        }

        protected override PieceState CheckTransitionConditions()
        {
            if (!StateMachine.SituationAnalyzer.DPSMatrix.IsEmpty)
            {
                foreach (var sectorDPS in StateMachine.SituationAnalyzer.DPSMatrix.GetSectorAmounts())
                {
                    if (sectorDPS.Value < SelfWeight.DamagePerMinute)
                        return new FollowToEnemy(StateMachine);
                }
            }
            return null;
        }
    }
}

