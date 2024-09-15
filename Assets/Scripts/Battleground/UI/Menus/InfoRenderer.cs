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
        [SerializeField] private CardHolder _spellCardHolder;
        private PlayerState _callbackState;   


        public IObjectForCardRenderer RenderedObj { get; private set; }


        public void Init(IObjectForCardRenderer obj, PlayerState callbackState)
        {
            RenderedObj = obj;
            _callbackState = callbackState;
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
            _spellCardHolder.ShowCards(info.ObjectsForCardRenderers, _callbackState);
        }

        public override void Close()
        {
            _spellCardHolder.HideCards();

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
