using Battleground.UI;
using System.Collections;
using System.Collections.Generic;
using Units.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

namespace Battleground
{
    public class DuckUICard : UICard, ICardHolder
    {
        [SerializeField] private RectTransform _hatCardPivot;
        [SerializeField] private RectTransform _armorCardPivot;
        [SerializeField] private RectTransform _weaponCardPivot;
        [SerializeField] private RectTransform _accessory1CardPivot;
        [SerializeField] private RectTransform _accessory2CardPivot;
        private HeadArmor _headArmor = null;
        private BodyArmor _bodyArmor = null;
        private Weapon _weapon = null;
        private Accessories _accessory1 = null;
        private Accessories _accessory2 = null;

        public bool Add(UICard card)
        {
            var result = true;
            var item = card.ObjectForUICard;
            if (item is HeadArmor headArmor)
            {
                _headArmor = headArmor;
                card.SetParent(_hatCardPivot, this);
            }
            else if (item is BodyArmor bodyArmor)
            {
                _bodyArmor = bodyArmor;
                card.SetParent(_armorCardPivot, this);
            }
            else if (item is Weapon weapon)
            {
                _weapon = weapon;
                card.SetParent(_weaponCardPivot, this);
            }
            else if (item is Accessories accessory)
            {
                if (_accessory1 == null)
                {
                    card.SetParent(_accessory1CardPivot, this);
                    _accessory1 = accessory;
                }
                else
                {
                    card.SetParent(_accessory2CardPivot, this);
                    _accessory2 = accessory;
                }
            }
            else
            {
                result = false;
            }


            if (result)
            {
                card.SetPosition(Vector2.zero);
                card.SetRotation(0);
                card.SetSize(TargetSize);
            }

            return result;
        }

        public void Remove(UICard card)
        {
            Destroy(card.gameObject); 
        }

        public void SelectCardEvent()
        {
            Debug.Log("DD");
        }
    }
}

