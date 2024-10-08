using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Items
{
    [CreateAssetMenu(menuName = ("Config/Body Armor"))]
    public class BodyArmor : Armor
    {
        [Header("Model parameters")]
        public GameObject Model;
    }
}
