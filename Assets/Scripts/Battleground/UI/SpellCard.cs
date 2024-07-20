using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground.UI
{
    public class SpellCard : MonoBehaviour, IInteractableForPlayer
    {
        public Spell Spell { get; private set; }

        public LayerMask AvailableLayers => Spell.Mask;

        public void Init(Spell spell)
        {
            Spell = spell;
        }

        public IEnumerator LeftMouseButtonDown(Player player)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator RightMouseButtonDown(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}

