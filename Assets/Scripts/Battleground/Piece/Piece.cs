using System.Diagnostics;
using Units;
using UnityEngine;

namespace Battleground
{
    public class Piece : MonoBehaviour, IObjectForCardRenderer
    {
        [SerializeField] private PieceUIRenderer _UIRenderer;

        public PieceHealth Health { get; private set; }
        public Unit Unit { get; private set; }

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

        public InfoForCardRenderer GetInfo()
        {
            var info = Unit.GetInfo();
            info.HealthBarFill = Health.HealthFill;
            return info;
        }
    }
}

