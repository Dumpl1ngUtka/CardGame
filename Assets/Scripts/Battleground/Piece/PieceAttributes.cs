using Units;

namespace Battleground
{
    public class PieceAttributes
    {
        private Unit _unit;
        public float MaxHealth
        {
            get
            {
                return _unit.SkillLevels.Health * 10;
            }
        }

        public float Accuracy
        {
            get
            {
                return _unit.Inventory.GetAccuracy();
            }
        }

        public float DodgeChance
        {
            get
            {
                return _unit.SkillLevels.Dexterity * 0.02f;
            }
        }

        public PieceAttributes(Piece piece)
        {
            _unit = piece.Unit;
        }
    }
}

