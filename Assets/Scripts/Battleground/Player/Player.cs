using Battleground.Grid;
using Battleground.UI;
using System;
using System.Collections.Generic;
using Units;
using Units.Items;
using UnityEngine;

namespace Battleground
{
    public class Player : MonoBehaviour, IObjectForInfoRenderer
    {
        [SerializeField] private Transform _pieceConteiner;
        [SerializeField] private Piece _piecePrefab;
        [SerializeField] private BattleSceneUI UI;
        [SerializeField] private CameraModeChanger _cameraMode;

        [SerializeField] private UnitRace[] _races;
        [SerializeField] private UnitClass[] _classes;
        [SerializeField] private HeadArmor[] _hats;
        [SerializeField] private BodyArmor[] _armors;

        public GridMap Map;
        public Timeline Timeline;
        public List<Unit> Units;
        public PlayerStateMachine StateMachine;
        public event Action MoveFinished;
        public bool IsMoveFinished { get; private set; }


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
            for (int i = 0; i < Units.Count; i++)
            {
                Units[i].Inventory.SetArmor(_hats[i]);
                Units[i].Inventory.SetArmor(_armors[i]);
            }
            StateMachine = new PlayerStateMachine(this, UI, _cameraMode);
        }

        public void InstantiatePiece(Unit unit, RaycastHit hit)
        {
            Units.Remove(unit);
            var cell = hit.collider.GetComponent<GridCell>();
            var piece = Instantiate(_piecePrefab, cell.SpawnPosition, _piecePrefab.transform.rotation, _pieceConteiner);
            piece.Init(unit, this);
            piece.SetPosition(cell);
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

        public InfoForInfoRenderer GetInfo()
        {
            return new InfoForInfoRenderer
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

