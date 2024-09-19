using System.Collections;
using System.Collections.Generic;
using TMPro;
using Units;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace Battleground.UI
{
    public class CardRenderer : MonoBehaviour
    {
        [SerializeField] private Image _specializationIcon;
        [SerializeField] private Image _mainIcon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _actionTime;
        [SerializeField] private TMP_Text _consumptions;

        public void Render(Spell spell)
        {
            _specializationIcon.sprite = spell.SpecializationIcon;
            _mainIcon.sprite = spell.MainBackground;
            _name.text = spell.name;
            _actionTime.text = spell.ActionTime.ToString();
            _consumptions.text = spell.Ñonsumption.ToString();
        }

        public void Render(Unit unit)
        {
            _name.text = unit.Name;
            _actionTime.text = "-";
            _consumptions.text = "-";
        }
    }
}

