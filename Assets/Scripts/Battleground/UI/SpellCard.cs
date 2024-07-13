using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground.UI
{
    public class SpellCard : MonoBehaviour, IInteractableForPlayer
    {
        private Spell _spell;

        public void Init(Spell spell)
        {
            _spell = spell;
        }

        public void LeftMouseButtonDown(Player player)
        {
            StartCoroutine(_spell.Activate());
        }

        public void RightMouseButtonDown(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}

