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
                return 50 + _unit.SkillLevels.Health * 10;
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

        public float MaxWeight
        {
            get
            {
                return 30 + _unit.SkillLevels.Capacity * 5f;
            }
        }

        public int WeightGroup
        {
            get
            {
                int groupNumber = 1;
                var excessPercent = _unit.Inventory.GetItemsWeight() / MaxWeight;
                for (float i = 0; i <= 1; i += 0.5f)
                {
                    if (excessPercent < i)
                        break;
                    groupNumber++;
                }
                return groupNumber;
            }
        }

        public PieceAttributes(Piece piece)
        {
            _unit = piece.Unit;

        }
    }
}

