using Battleground.UI;
using System.Collections;
using System.Collections.Generic;
using Units.Items;
using UnityEngine;

namespace Battleground
{
    public class DuckUICard : UICard, ICardHolder
    {
        [SerializeField] private RectTransform _hatCardPivot;
        [SerializeField] private RectTransform _armorCardPivot;
        [SerializeField] private RectTransform _weaponCardPivot;
        [SerializeField] private RectTransform _accessory1CardPivot;
        [SerializeField] private RectTransform _accessory2CardPivot;
        public HeadArmor HeadArmor { get; private set; } = null;
        public BodyArmor BodyArmor { get; private set; } = null;
        public Weapon Weapon { get; private set; } = null;
        public Accessory Accessory1 { get; private set; } = null;
        public Accessory Accessory2 { get; private set; } = null;

        public bool Add(UICard card)
        {
            var result = true;
            var item = card.ObjectForUICard;
            if (item is HeadArmor headArmor)
            {
                HeadArmor = headArmor;
                card.SetParent(_hatCardPivot, this);
            }
            else if (item is BodyArmor bodyArmor)
            {
                BodyArmor = bodyArmor;
                card.SetParent(_armorCardPivot, this);
            }
            else if (item is Weapon weapon)
            {
                Weapon = weapon;
                card.SetParent(_weaponCardPivot, this);
            }
            else if (item is Accessory accessory)
            {
                if (Accessory1 == null)
                {
                    card.SetParent(_accessory1CardPivot, this);
                    Accessory1 = accessory;
                }
                else
                {
                    card.SetParent(_accessory2CardPivot, this);
                    Accessory2 = accessory;
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
            
        }
    }
}

