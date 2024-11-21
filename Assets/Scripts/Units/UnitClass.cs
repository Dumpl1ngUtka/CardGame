using Units.Items;
using System.Collections.Generic;
using UnityEngine;
using Battleground;

namespace Units
{
    [CreateAssetMenu(menuName = "Config/Class")]
    public class UnitClass : ScriptableObject
    {
        public string Name;
        public Sprite Icon; 
        public List<WeaponTypes> AvailableWeaponsTypes;
        public List<PassiveEffect> PassiveEffects;
        public Attributes MinLevels;
        public PieceAbility[] Abilites;
    }
}
