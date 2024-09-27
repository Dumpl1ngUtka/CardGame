using Battleground;
using System;
using System.Collections.Generic;
using System.Linq;
using Units.Items;

namespace Units
{
    public class UnitInventory
    {
        public Weapon MainWeapon { get; private set; }
        public Weapon SecondWeapon { get; private set; }
        public Armor Armor { get; private set; }
        public Accessories[] Accessories { get; private set; } = new Accessories[3];
        public Item[] InBagItems { get; private set; } = new Item[6];
        public Item[] EquippedItems
        {
            get
            {
                Item[] items = new Item[] {MainWeapon, SecondWeapon,  Armor }.Concat(Accessories).ToArray();
                return items;
            }
        }
        public Action InventoryChanged;


        public Spell[] GetSpells()
        {
            var spells = new List<Spell>();

            foreach (var item in EquippedItems)
                if (item != null && item.Spells.Count > 0)
                    item.Spells.AddRange(spells);

            return spells.ToArray();
        }

        public AdditionalPieceAttributes GetAdditionalAttributes()
        {
            var additionalAttributes = new AdditionalPieceAttributes();
            foreach (var item in EquippedItems)
            {
                additionalAttributes += item.Attributes;
            }
            return additionalAttributes;
        }

        public float GetItemsWeight()
        {
            var weight = 0f;
            foreach (var item in EquippedItems)
                weight += item != null ? item.Weight : 0;

            foreach (var item in InBagItems)
                weight += item != null ? item.Weight : 0;

            return weight;
        }

        public void AddItem(Item item)
        {
            InventoryChanged?.Invoke();
        }
    }
}