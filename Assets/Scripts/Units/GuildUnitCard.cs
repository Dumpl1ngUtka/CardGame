using TMPro;
using Units;
using UnityEngine;

namespace UI
{
    public class GuildUnitCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private CellsProgressBar _health;
        [SerializeField] private CellsProgressBar _energy;
        [SerializeField] private CellsProgressBar _dexterity;
        [SerializeField] private CellsProgressBar _strength;
        [SerializeField] private CellsProgressBar _starsRenderer;

        public Unit Unit { get; private set; }

        public void Init(Unit unit)
        {
            Unit = unit; 
            _name.text = unit.Name;
            _starsRenderer.Render(unit.StarCount);
            _health.Render(unit.SkillLevels.Health);
            _energy.Render(unit.SkillLevels.Energy);
            _dexterity.Render(unit.SkillLevels.Dexterity);
            _strength.Render(unit.SkillLevels.Strength);
        }

        private string FormedValueToString(int currentValue, int maxValue)
        {
            var len = maxValue.ToString().Length;
            var zeroCount = len - currentValue.ToString().Length;
            var newStr = "";
            for (var i = 0; i < zeroCount; i++)
                newStr += "0";
            newStr += currentValue.ToString() + "/" + maxValue.ToString();
            return newStr;
        }
    }
}
