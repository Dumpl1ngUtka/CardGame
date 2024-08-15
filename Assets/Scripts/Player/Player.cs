using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _pieceConteiner;
        [SerializeField] private Piece _piecePrefab;

        [SerializeField] private UnitRace[] _races;
        [SerializeField] private UnitClass[] _classes;

        public List<Unit> Units;
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
        }

        public void InstantiatePiece(Unit unit, RaycastHit hit)
        {
            Units.Remove(unit);
            var piece = Instantiate(_piecePrefab, hit.point, _piecePrefab.transform.rotation, _pieceConteiner);
            piece.Init(unit);
            PlayablePieceCount();
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
    }
}

