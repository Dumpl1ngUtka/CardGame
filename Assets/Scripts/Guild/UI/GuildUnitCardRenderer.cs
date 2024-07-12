using TMPro;
using Units;
using UnityEngine;

namespace UI
{
    public class GuildUnitCardRenderer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private ProgressBar _health;
        [SerializeField] private ProgressBar _energy;
        [SerializeField] private ProgressBar _dexterity;
        [SerializeField] private ProgressBar _strength;
        [SerializeField] private CellsProgressBar _starsRenderer;

        public Unit Unit { get; private set; }

        public void Init(Unit unit)
        {
            Unit = unit;
        }

        public void Render()
        {
            _name.text = Unit.Name;
            _starsRenderer.Render(Unit.StarCount);
            _health.Render(Unit.SkillLevels.Health);
            _energy.Render(Unit.SkillLevels.Energy);
            _dexterity.Render(Unit.SkillLevels.Dexterity);
            _strength.Render(Unit.SkillLevels.Strength);
        }
    }
}
