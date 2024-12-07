using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Items
{
    [CreateAssetMenu(menuName = ("Config/Accessory"))]
    public class Accessory : Item
    {
        public override Sprite TypeIcon => Resources.Load<Sprite>("Sprites/CardTypeIcons/Accessories");
    }

}
