using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground.UI
{
    public class SpellCard : MonoBehaviour
    {
        [SerializeField] private SpellCardRenderer _renderer;

        public Spell Spell { get; private set; }

        public LayerMask AvailableLayers => Spell.Mask;

        public void Init(Spell spell)
        {
            Spell = spell;
            _renderer.Render(spell);
        }
    }
}

