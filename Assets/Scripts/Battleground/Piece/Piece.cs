using System;
using System.Collections;
using Units;
using UnityEngine;

namespace Battleground
{
    public class Piece : MonoBehaviour
    {
        [SerializeField] private UnitRace _race;
        [SerializeField] private UnitClass _class;
        [SerializeField] private PieceUIRenderer _UIRenderer;

        public PieceHealth Health { get; private set; }
        public Unit Unit { get; private set; }

        public LayerMask AvailableLayers => LayerMask.GetMask("Enemy Unit", "Player Unit", "Card");

        private void Awake()
        {
            Init(new Unit(3, _race, _class));
        }

        public void Init(Unit unit)
        {
            Unit = unit;
            Health = new PieceHealth(this, unit.SkillLevels.Health);
            _UIRenderer.Init(this);
        }

        public void Move(Vector3 position)
        {
            transform.position = position;
        }

        public void ApplyDamage(int value)
        {
            Health.ApplyDamage(value);
        }
    }
}

