using System.Collections;
using System.Collections.Generic;
using UI;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Guild
{
    [RequireComponent(typeof(GuildUnitCardRenderer))]
    public class GuildUnitCard : MonoBehaviour
    { 
        [Header("Visualization objects")]
        [SerializeField] private Light[] _lights;
        [SerializeField] private Image _rerollLockButton;
        [Header("Sprites")] 
        [SerializeField] private Sprite _lockIcon;
        [SerializeField] private Sprite _unlockIcon;
        private GuildUnitCardRenderer _cardRenderer;
        private Unit _unit;
        private GuildCardAnimator _cardAnimator;

        public bool IsRerollLock { get; private set; } = true;

        private void Awake()
        {
            _cardRenderer = GetComponent<GuildUnitCardRenderer>();
            _cardAnimator = GetComponent<GuildCardAnimator>();
        }

        public void Init(Unit unit, bool isUpdateRenrerer = false)
        {
            _unit = unit;
            _cardRenderer.Init(unit);
            if (isUpdateRenrerer)
                _cardRenderer.Render();
            ChangeLights(unit.StarCount);
        }

        private void ChangeLights(int starCount)
        {
            foreach (var light in _lights)
            {
               light.color = RareColors.GetColorByStarCount(starCount);
            }
        }

        public void ChangeRerollLock()
        {
            IsRerollLock = !IsRerollLock;
            _rerollLockButton.sprite = IsRerollLock ? _lockIcon : _unlockIcon;
        }

        public void Reroll()
        {
            _cardAnimator.RerollAnimation();
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
                Debug.Log("Left");

            if (Input.GetMouseButtonDown(1))
                ChangeRerollLock();
        }
    }
}