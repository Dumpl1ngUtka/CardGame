using AI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Battleground
{
    public class Idle : PieceState
    {
        private List<Vector3> _path;
        private int _pathCornerIndex;
        private int _currentMapAnchor;
        private Transform[] _anchors;
        private List<PieceAbility> _availableAbilityList = new List<PieceAbility>();

        protected override float MinStateTime => 0;
        protected override float MaxStateTime => float.PositiveInfinity;
        protected override List<PieceAbility> AvailableAbilityList => _availableAbilityList;
        public override Transform Target => null;

        public override void Enter(PieceState previousState)
        {
            base.Enter(previousState);
            _currentMapAnchor = 0;
            SetNewPath();
            Piece.UI.ChangeGroundIndicator(Color.white);
        }

        public override void Init(PieceStateMachine pieceStateMachine)
        {
            base.Init(pieceStateMachine);
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
            NavMesh.CalculatePath(StateMachine.Position, targetPoint.position, ~0, navMeshPath);
            //Piece.Agent.destination = targetPoint.position;
            _path.AddRange(navMeshPath.corners);
            _path.Add(targetPoint.position);
        }

        private void MoveByPath()
        {

        }

        public override float GetMetric(SituationAnalyzer situationAnalyzer)
        {
            return 0.1f;
        }
    }
}

