using Battleground;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Items
{
    public abstract class Item : ScriptableObject, IObjectForUICard
    {
        public string Name;
        public Sprite Icon;
        [Min(0)] public float Weight;
        [Min(0)] public float Price;
        public List<PieceAbility> Abilites;
        public AdditionalPieceAttributes Attributes;

        #region CardUI
        public abstract Sprite TypeIcon { get; }
        public Sprite Image => Icon;
        public Sprite ParamIcon1 => Resources.Load<Sprite>("Sprites/Weight");
        public string Title => Name;
        public string ParamValue1 => Weight.ToString();
        public Sprite ParamIcon2 => Resources.Load<Sprite>("Sprites/Coin");
        public string ParamValue2 => Price.ToString();
        public Spell Spell => null;
        
        #endregion
    }
}

