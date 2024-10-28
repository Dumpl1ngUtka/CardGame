using AI;
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

        protected override float MinStateTime => 0;

        public override void Enter()
        {
            base.Enter();
            _currentMapAnchor = 0;
            SetNewPath();
        }

        public WalkAlone(PieceStateMachine pieceStateMachine) : base(pieceStateMachine)
        {
            _anchors = Piece.Player.Map.Anchors;
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

        public override float GetMetric(SituationAnalyzer situationAnalyzer)
        {
            return situationAnalyzer.WeightPoints.Count == 0 ? 2 : 0.1f;
        }
    }
}

