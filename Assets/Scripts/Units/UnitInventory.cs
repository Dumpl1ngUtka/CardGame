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

        public UnitInventory()
        {

        }

        public Spell[] GetSpells()
        {
            var spells = new List<Spell>();

            foreach (var item in EquippedItems)
                if (item != null && item.Spells.Count > 0)
                    item.Spells.AddRange(spells);

            return spells.ToArray();
        }

        public float GetAccuracy()
        {
            var accurancy = 0f;
            accurancy += MainWeapon.Accuracy;
            accurancy += SecondWeapon.Accuracy;
            return accurancy;
        }
    }
}