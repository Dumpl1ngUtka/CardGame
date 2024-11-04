using AI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Battleground
{
    public class WalkAlone : PieceState
    {
        #region Const
        private const float _minDistanceToCorner = 2f;
        #endregion

        private List<Vector3> _path;
        private int _pathCornerIndex;
        private int _currentMapAnchor;
        private Transform[] _anchors;
        private List<PieceAbility> _availableAbilityList = new List<PieceAbility>();

        protected override float MinStateTime => 0;
        protected override List<PieceAbility> AvailableAbilityList => _availableAbilityList;
        public override Transform Target => null;

        public override void Enter()
        {
            base.Enter();
            _currentMapAnchor = 0;
            SetNewPath();
            Piece.UI.ChangeGroundIndicator(Color.white);
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
            var navMeshPath = new NavMeshPath();
            NavMesh.CalculatePath(Piece.Position, targetPoint.position, ~0, navMeshPath);
            //Piece.Agent.destination = targetPoint.position;
            _path.AddRange(navMeshPath.corners);
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

