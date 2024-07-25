using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Config/Races")]
    public class UnitRace: ScriptableObject
    {
        public string Name;
        public bool IsCanUseWeapon = true;
        public bool IsCanUseArmor = true;
        public List<PassiveEffect> PassiveEffects;
        public List<UnitClass> AvailableClasses;
        public UnitResists Resists;
        public SkillLevels MinLevels;
        public Spell[] Spells;
        public string[] AvailableNames;

        public string GetRandomName()
        {
            var index = Random.Range(0, AvailableNames.Length);
            return AvailableNames[index];
        }
    }
}
