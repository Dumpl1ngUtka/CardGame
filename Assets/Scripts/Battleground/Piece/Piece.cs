using System;
using Units;
using UnityEngine;

namespace Battleground
{
    public class Piece : MonoBehaviour, IInteractableForPlayer
    {
        [SerializeField] private UnitRace _race;
        [SerializeField] private UnitClass _class;

        public Unit Unit { get; private set; }

        private void Awake()
        {
            Unit = new Unit(3, _race, _class);
        }

        public void LeftMouseButtonDown(Player player)
        {
            player.UI.ShowUnitInfo(Unit);
        }

        public void RightMouseButtonDown(Player player)
        {
            throw new NotImplementedException();
        }
    }
}

