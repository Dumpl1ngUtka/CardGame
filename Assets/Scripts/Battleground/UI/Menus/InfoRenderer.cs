using TMPro;
using UI;
using UnityEngine;

namespace Battleground.UI
{
    public class InfoRenderer : UIMenu
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _underTitle;
        [SerializeField] private TMP_Text _contentLine1;
        [SerializeField] private TMP_Text _contentLine2;
        [SerializeField] private TMP_Text _contentLine3;
        [SerializeField] private ProgressBar _healthBar;
        [SerializeField] private ProgressBar _staminaBar;
        //[SerializeField] private CellsProgressBar _starsRenderer;

        public IObjectForInfoRenderer RenderedObj { get; private set; }


        public void Init(IObjectForInfoRenderer obj)
        {
            RenderedObj = obj;
        }

        public override void Open()
        {
            var info = RenderedObj.GetInfo();
            _title.text = info.Title;
            _underTitle.text = info.UnderTitle;
            _contentLine1.text = info.ContentLine1;
            _contentLine2.text = info.ContentLine2;
            _contentLine3.text = info.ContentLine3;
            if (info.HealthBarFill != default)
                _healthBar.Render(info.HealthBarFill);
            else
                _healthBar.SetActive(false);

            if (info.StaminaBarFill != default)
                _staminaBar.Render(info.StaminaBarFill);
            else
                _staminaBar.SetActive(false);

            //_starsRenderer?.Render(Unit.StarCount);
        }

        public override void Close()
        {
            _title.text = "";
            _underTitle.text = "";
            _contentLine1.text = "";
            _contentLine2.text = "";
            _contentLine3.text = "";
            _healthBar.SetActive(false);
            _staminaBar.SetActive(false);
        }
    }
}
