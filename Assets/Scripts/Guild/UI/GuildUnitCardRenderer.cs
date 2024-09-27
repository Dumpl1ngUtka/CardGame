using TMPro;
using Units;
using UnityEngine;

namespace UI
{
    public class GuildUnitCardRenderer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _race;
        [SerializeField] private TMP_Text _class;
        [SerializeField] private ProgressBar _health;
        [SerializeField] private ProgressBar _energy;
        [SerializeField] private ProgressBar _dexterity;
        [SerializeField] private ProgressBar _strength;
        [SerializeField] private ProgressBar _intellegence;
        [SerializeField] private ProgressBar _capacity;
        [SerializeField] private CellsProgressBar _starsRenderer;

        public Unit Unit { get; private set; }

        public void Init(Unit unit)
        {
            Unit = unit;
        }

        public void Render()
        {
            _name.text = Unit.Name;
            _race.text = Unit.Race.Name;
            _class.text = Unit.Class.Name;
            _starsRenderer.Render(Unit.StarCount);
            _health.Render(Unit.Attributes.Health);
            _energy.Render(Unit.Attributes.Energy);
            _dexterity.Render(Unit.Attributes.Dexterity);
            _strength.Render(Unit.Attributes.Strength);
            _intellegence.Render(Unit.Attributes.Intelligence);
            _capacity.Render(Unit.Attributes.Capacity);
        }
    }
}
