using Battleground.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Units;
using Units.Items;
using UnityEngine;

namespace Battleground
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _pieceConteiner;
        [SerializeField] private Piece _piecePrefab;
        [SerializeField] private BattleSceneUI UI;
        [SerializeField] private CardHolder _cardHolder;
        [SerializeField] private CameraModeChanger _cameraMode;
        [SerializeField] private int _teamID;

        [SerializeField] private UnitRace[] _races;
        [SerializeField] private UnitClass[] _classes;
        [SerializeField] private HeadArmor[] _hats;
        [SerializeField] private BodyArmor[] _armors;

        [SerializeField] private bool _isTestPlayer = false;

        public BattlegroundMap Map;
        public List<Unit> Units;
        public PlayerStateMachine StateMachine;

        public Transform PieceConteiner => _pieceConteiner;
        public int TeamID => _teamID;
        public bool HasPlayablePiece => PlayablePieceCount() > 0;
        public bool IsUnitsListEmpty => Units.Count == 0;
        public CardHolder CardHolder => _cardHolder;

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
                UI.Init(this);
                StateMachine = new PlayerStateMachine(this, UI, _cameraMode);
                _cardHolder.Init(StateMachine, UI);
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

        public void AddCards(List<UICard> cards) => _cardHolder.Add(cards);
        public void RemoveCard(UICard card) => CardHolder.Remove(card);
        public void SetCardsVisable(bool isVisable) => CardHolder.SetCardsVisable(isVisable);
    }
}

