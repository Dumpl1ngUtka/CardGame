using Units.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Config/Class")]
    public class UnitClass : ScriptableObject
    {
        public string Name;
        public List<WeaponTypes> AwailableWeaponsTypes;
        public List<PassiveEffect> PassiveEffects;
        public Attributes MinLevels;
        public Spell[] Spells;
        public Sprite Icon;
    }
}
