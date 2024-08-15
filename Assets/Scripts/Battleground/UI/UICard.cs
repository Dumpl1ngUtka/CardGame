using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground.UI
{
    public class UICard : MonoBehaviour
    {
        [SerializeField] private CardRenderer _renderer;
        private IObjectForCardRenderer _containedObj;
        public Spell Spell => _containedObj as Spell;
        public Unit Unit => _containedObj as Unit;

        public void Init(IObjectForCardRenderer obj)
        {
            _containedObj = obj;
            if (Spell != null)
                _renderer.Render(Spell);
            else if (Unit != null)
                _renderer.Render(Unit);
        }
        public void Init(Spell spell)
        {
            _containedObj = spell;
            _renderer.Render(spell);
        }
        public void Init(Unit unit)
        {
            _containedObj = unit;
            _renderer.Render(unit);
        }
    }
}

