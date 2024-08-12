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
        public SkillLevels MinLevels;
        public Spell[] Spells;
    }
}
