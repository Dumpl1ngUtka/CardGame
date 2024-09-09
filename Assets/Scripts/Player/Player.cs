using Battleground.UI;
using System;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground
{
    public class Player : MonoBehaviour, IObjectForCardRenderer
    {
        [SerializeField] private Transform _pieceConteiner;
        [SerializeField] private Piece _piecePrefab;
        [SerializeField] private BattleSceneUI UI;

        [SerializeField] private UnitRace[] _races;
        [SerializeField] private UnitClass[] _classes;

        public Timeline Timeline;
        public List<Unit> Units;
        public PlayerStateMachine StateMachine;
        public event Action MoveFinished;
        public bool IsMoveFinished;


        public bool HasPlayablePiece => PlayablePieceCount() > 0;
        public bool IsUnitsListEmpty => Units.Count == 0;


        private void Awake()
        {
            Units = new List<Unit>
            {
                new Unit(3,  _races[0], _classes[0]),
                new Unit(1,  _races[1], _classes[1]),
                new Unit(2,  _races[2], _classes[2]),
                new Unit(3,  _races[3], _classes[3]),
            };
            StateMachine = new PlayerStateMachine(this, UI);
        }

        public void InstantiatePiece(Unit unit, RaycastHit hit)
        {
            Units.Remove(unit);
            var piece = Instantiate(_piecePrefab, hit.point, _piecePrefab.transform.rotation, _pieceConteiner);
            piece.Init(unit, this);
        }

        public int PlayablePieceCount()
        {
            var count = 0;
            foreach (Transform child in _pieceConteiner)
            {
                var piece = child.GetComponent<Piece>();
                if (!piece.Health.IsDied)
                    count++;
            }
            return count;
        }

        public InfoForCardRenderer GetInfo()
        {
            return new InfoForCardRenderer
            {
                ObjectsForCardRenderers = Units.ToArray()
            };
        }

        public void StartNewMove(float minTime, float maxTime)
        {
            Timeline.SetTimeBounds(minTime, maxTime);
            foreach (Transform child in _pieceConteiner)
            {
                var piece = child.GetComponent<Piece>();
                piece.NextMove();
            }
            IsMoveFinished = false;
            StateMachine.ChangeState(new SelectUnitCard(StateMachine));
        }

        public void MoveFinishedTrigger()
        {
            MoveFinished?.Invoke();
            IsMoveFinished = true;
        }
    }
}

