using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Battleground.UI
{
    public class CardRenderer : MonoBehaviour
    {
        [SerializeField] private Image _specializationIcon;
        [SerializeField] private Image _background;

        public void Render(Spell spell)
        {
            _specializationIcon.sprite = spell.SpecializationIcon;
            _background.sprite = spell.MainBackground;
        }

        public void Render(Unit unit)
        {
            //_specializationIcon.sprite = spell.SpecializationIcon;
            //_background.sprite = spell.MainBackground;
        }
    }
}

