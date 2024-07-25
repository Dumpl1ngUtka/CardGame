using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Battleground.UI
{
    public class SpellCardRenderer : MonoBehaviour
    {
        [SerializeField] private Image _specializationIcon;
        [SerializeField] private Image _background;

        public void Render(Spell spell)
        {
            _specializationIcon.sprite = spell.SpecializationIcon;
            _background.sprite = spell.MainBackground;
        }
    }
}

