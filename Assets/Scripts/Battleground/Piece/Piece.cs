using Units;
using UnityEngine;

namespace Battleground
{
    public class Piece : MonoBehaviour, IObjectForCardRenderer
    {
        [SerializeField] private PieceUIRenderer _UIRenderer;

        public PieceCondition Condition { get; private set; }
        public PieceHealth Health { get; private set; }
        public Unit Unit { get; private set; }

        public void Init(Unit unit)
        {
            Unit = unit;
            Health = new PieceHealth(this, unit.SkillLevels.Health);
            Health.Died += Died;
            _UIRenderer.Init(this);
        }

        private void Died()
        {
            ChangeCondition(PieceCondition.Died);
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

        public void ChangeCondition(PieceCondition newCondition)
        {
            Condition = newCondition;
        }

        private void OnDisable()
        {
            Health.Died -= Died;
        }
    }
}

