using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground
{
    public interface IObjectForUICard
    {
        public Sprite Image { get; }
        public Sprite TypeIcon { get; }
        public Sprite ParamIcon1 { get; }
        public Sprite ParamIcon2 { get; }
        public string Title { get; }
        public string ParamValue1 { get; }
        public string ParamValue2 { get; }
        public Spell Spell { get; }
    }
}

