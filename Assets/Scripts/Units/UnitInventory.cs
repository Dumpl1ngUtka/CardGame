using AI;
using Battleground;
using System;
using System.Collections.Generic;
using Units.Items;

namespace Units
{
    public class UnitInventory
    {
        public Weapon MainWeapon { get; private set; }
        public Weapon SecondWeapon { get; private set; }
        public HeadArmor HeadArmor { get; private set; }
        public BodyArmor Armor { get; private set; }
        public Accessory Accessory1 { get; private set; }
        public Accessory Accessory2 { get; private set; }
        public Item[] EquippedItems
        {
            get
            {
                return new Item[] {MainWeapon, SecondWeapon, HeadArmor, Armor, Accessory1, Accessory2};
            }
        }

        public Action InventoryChanged;

        public PieceAbility[] GetAbilites()
        {
            if (EquippedItems.Length == 0)
                return null;

            var abilites = new List<PieceAbility>();

            foreach (var item in EquippedItems)
                if (item != null && item.Abilites.Count > 0)
                    item.Abilites.AddRange(abilites);
            return abilites.ToArray();
        }

        public AdditionalPieceAttributes GetAdditionalAttributes()
        {
            var additionalAttributes = new AdditionalPieceAttributes();
            foreach (var item in EquippedItems)
            {
                if (item == null)
                    continue;

                additionalAttributes += item.Attributes;
            }
            return additionalAttributes;
        }

        public float GetItemsWeight()
        {
            var weight = 0f;
            foreach (var item in EquippedItems)
                weight += item != null ? item.Weight : 0;

            return weight;
        }

        public void SetItem(Item item)
        {
            if (item == null)
                return;

            if (item is HeadArmor headArmor)
                HeadArmor = headArmor;
            else if (item is BodyArmor bodyArmor)
                Armor = bodyArmor;
            else if (item is Weapon weapon)
                MainWeapon = weapon;
            else if (item is Accessory accessory)
                if (Accessory1 == null)
                    Accessory1 = accessory;
                else
                    Accessory2 = accessory;

            InventoryChanged?.Invoke();
        }
    }
}