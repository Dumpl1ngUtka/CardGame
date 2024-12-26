using Battleground;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    [CreateAssetMenu(menuName = "Config/Races")]
    public class UnitRace: ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public bool IsCanUseWeapon = true;
        public bool IsCanUseArmor = true;
        public List<UnitClass> AvailableClasses;
        public UnitResists Resists;
        public Attributes MinLevels;
        public PieceAbility[] Abilites;
        public string[] AvailableNames;

        public string GetRandomName()
        {
            var index = Random.Range(0, AvailableNames.Length);
            return AvailableNames[index];
        }
    }
}
