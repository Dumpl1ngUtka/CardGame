using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Items
{
    public abstract class Item : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        [Min(0)] public float Weight;
        public List<Spell> Spells;
    }
}

