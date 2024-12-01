using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground
{
    [CreateAssetMenu(menuName = "Spells/KineticBoom")]
    public class KineticBoom : Spell
    {
        [SerializeField] private Sprite _image;

        public override Sprite TypeIcon => Resources.Load<Sprite>("Sprites/CardTypeIcons/Spell");

        public override Sprite ParamIcon1 => Resources.Load<Sprite>("Sprites/ClassIcons/Wizzard");

        public override Sprite ParamIcon2 => Resources.Load<Sprite>("Sprites/Empty");

        public override string ParamValue1 => throw new System.NotImplementedException();

        public override string ParamValue2 => throw new System.NotImplementedException();
    }
}

