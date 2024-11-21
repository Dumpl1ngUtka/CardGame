using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battleground.UI
{
    public class CardRenderer : MonoBehaviour
    {
        [SerializeField] private Image _specializationIcon;
        [SerializeField] private Image _mainIcon;
        [SerializeField] private Image _paramIcon1;
        [SerializeField] private Image _paramIcon2;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _paramText1;
        [SerializeField] private TMP_Text _paramText2;

        public void Render(IObjectForUICard obj)
        {
            _specializationIcon.sprite = obj.TypeIcon;
            _mainIcon.sprite = obj.Image;
            _title.text = obj.Title;
            _paramIcon1.sprite = obj.ParamIcon1;
            _paramIcon2.sprite = obj.ParamIcon2;
            _paramText1.text = obj.ParamValue1;
            _paramText2.text = obj.ParamValue2;
        }
    }
}

