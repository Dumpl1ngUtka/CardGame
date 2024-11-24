using Battleground.UI;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private int _teamID;

        [SerializeField] private UnitRace[] _races;
        [SerializeField] private UnitClass[] _classes;
        [SerializeField] private HeadArmor[] _hats;
        [SerializeField] private BodyArmor[] _armors;

        [SerializeField] private bool _isTestPlayer = false;

        private CardHolder _cardHolder => UI.CardHolder;

        public BattlegroundMap Map;
        public List<Unit> Units;
        public List<IObjectForUICard> Cards = new List<IObjectForUICard>();
        public PlayerStateMachine StateMachine;

        public Transform PieceConteiner => _pieceConteiner;
        public int TeamID => _teamID;
        public bool HasPlayablePiece => PlayablePieceCount() > 0;
        public bool IsUnitsListEmpty => Units.Count == 0;
        public Action CardsChanged;

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
            if (!_isTestPlayer)
            {
                StateMachine = new PlayerStateMachine(this, UI, _cameraMode);
                UI.Init(this);
            }
        }

        public void InstantiatePiece(Unit unit, RaycastHit hit)
        {
            Units.Remove(unit);
            var piece = Instantiate(_piecePrefab, hit.point, _piecePrefab.transform.rotation, _pieceConteiner);
            piece.Init(unit, this);
        }

        public void InstantiatePiece(Unit unit, Vector3 position)
        {
            Units.Remove(unit);
            var piece = Instantiate(_piecePrefab, position, _piecePrefab.transform.rotation, _pieceConteiner);
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

        public void AddCards(List<IObjectForUICard> cards)
        {
            Cards = Cards.Concat(cards).ToList();
            CardsChanged?.Invoke();
            _cardHolder.AddNewCards(cards);
        }

        public InfoForInfoRenderer GetInfo()
        {
            return new InfoForInfoRenderer
            {
                ObjectsForCardRenderers = Cards
            };
        }
    }
}

