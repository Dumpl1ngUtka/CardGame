using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using Units;
using UnityEngine;

namespace Battleground.UI
{
    public class UnitParameters : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private ProgressBar _health;
        [SerializeField] private ProgressBar _energy;
        [SerializeField] private ProgressBar _dexterity;
        [SerializeField] private ProgressBar _strength;
        [SerializeField] private CellsProgressBar _starsRenderer;
        [SerializeField] private SpellCardHolder _spellCardHolder;

        public Unit Unit { get; private set; }

        public void Init(Unit unit)
        {
            Unit = unit;
            Render();
        }

        private void OnEnable()
        {
            _spellCardHolder?.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            if (_spellCardHolder == null)
                return;

            _spellCardHolder?.gameObject.SetActive(false);
        }

        public void Render()
        {
            _name.text = Unit.Name;
            _starsRenderer?.Render(Unit.StarCount);
            _health?.Render(Unit.SkillLevels.Health);
            _energy?.Render(Unit.SkillLevels.Energy);
            _dexterity?.Render(Unit.SkillLevels.Dexterity);
            _strength?.Render(Unit.SkillLevels.Strength);
        }
    }
}
