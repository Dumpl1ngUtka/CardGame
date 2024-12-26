using System.Collections.Generic;
using UnityEngine;

namespace Units.Items
{
    [CreateAssetMenu(menuName = ("Config/Item/Weapon"))]
    public class Weapon : Item
    {
        [Min(0)] public int Damage = 1;
        [Range(0, 1)] public float Accuracy = 0;
        public GameObject Model;

        public override Sprite TypeIcon => Resources.Load<Sprite>("Sprites/SpellTypes/Attack");
    }
}

