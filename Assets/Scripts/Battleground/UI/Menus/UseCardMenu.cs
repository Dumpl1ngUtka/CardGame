using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using Units;
using UnityEngine;

namespace Battleground.UI
{
    public class UseCardMenu : UIMenu
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private CellsProgressBar _starsRenderer;
        [SerializeField] private CardHolder _spellCardHolder;
        private const string _isEnable = "IsEnable";
        private Animator _animator;

        public Spell Spell { get; private set; }

        private void Awake()
        {
            //_animator = GetComponent<Animator>();
        }

        public void Init(Spell spell)
        {
            Spell = spell;
        }

        public override void Open()
        {
            //_animator.SetBool(_isEnable, true);
            //_name.text = Spell.Name;
           // _starsRenderer?.Render(Spell.StarCount);
            //_spellCardHolder.ShowCards(Spell.Spells);
        }

        public override void Close()
        {
            //_animator.SetBool(_isEnable, false);
            _spellCardHolder.HideCards();
        }

    }
}

