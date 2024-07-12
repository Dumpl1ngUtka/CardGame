using System.Collections.Generic;
using UnityEngine;

namespace Units.Weapons
{
    [CreateAssetMenu(menuName = ("Config/Weapon"))]
    public class Weapon : ScriptableObject
    {
        public string Name;
        public WeaponTypes Type;
        public DamageTypes MainDamageType;
        public DamageTypes SecondaryDamageType;
        [Range(1, 5)] public int Damage = 1;
        [Range(1, 5)] public int Usability = 1;
        [Range(1, 5)] public int Weight = 1;
        public GameObject Model;
        public Sprite Icon;

        public List<PassiveEffect> passiveEffects;
    }
}

