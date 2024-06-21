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

        public bool IsRerollLock { get; private set; } = false;

        private void Awake()
        {
            _cardRenderer = GetComponent<GuildUnitCardRenderer>();
        }

        public void Init(Unit unit)
        {
            _unit = unit;
            _cardRenderer.Init(unit);
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
    }
}