using TMPro;
using UI;
using Units;
using UnityEngine;

namespace Battleground.UI
{
    public class UnitInfoRenderer : UIMenu
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private CellsProgressBar _starsRenderer;
        [SerializeField] private CardHolder _spellCardHolder;
        private const string _isEnable = "IsEnable";
        private Animator _animator;

        public Unit Unit { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();   
        }

        public void Init(Unit unit)
        {
            Unit = unit;
        }

        public override void Open()
        {
            _animator.SetBool(_isEnable,true);
            _name.text = Unit.Name;
            _starsRenderer?.Render(Unit.StarCount);
            _spellCardHolder.ShowCards(Unit.Spells);
        }

        public override void Close()
        {
            _animator.SetBool(_isEnable,false);
            _spellCardHolder.HideCards();
        }
    }
}
