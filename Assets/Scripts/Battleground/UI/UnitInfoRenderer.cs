using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UI;
using Units;
using UnityEngine;

namespace Battleground.UI
{
    public class UnitInfoRenderer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
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
            _spellCardHolder.RenderCards(Unit.Spells);
        }
    }
}
