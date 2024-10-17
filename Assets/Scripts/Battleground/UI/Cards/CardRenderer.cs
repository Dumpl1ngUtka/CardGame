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
            _specializationIcon.sprite = Resources.Load<Sprite>("Sprites/SpellTypes/" + spell.Type.ToString());
            _mainIcon.sprite = spell.MainBackground;
            _name.text = spell.Name;
            _actionTime.text = spell.ActionTime.ToString();
            _consumptions.text = spell.Ñonsumption.ToString();
        }

        public void Render(Unit unit)
        {
            _specializationIcon.sprite = unit.Class.Icon;
            _mainIcon.sprite = unit.Race.FaceSprite;
            _name.text = unit.Name;
            _actionTime.text = "-";
            _consumptions.text = "-";
        }
    }
}

