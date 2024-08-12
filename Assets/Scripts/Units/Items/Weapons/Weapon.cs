using System.Collections.Generic;
using UnityEngine;

namespace Units.Items
{
    [CreateAssetMenu(menuName = ("Config/Weapon"))]
    public class Weapon : Item
    {
        public WeaponTypes Type;
        public DamageTypes MainDamageType;
        public DamageTypes SecondaryDamageType;
        [Range(1, 5)] public int Damage = 1;
        [Range(1, 5)] public int Usability = 1;
        public GameObject Model;
    }
}

