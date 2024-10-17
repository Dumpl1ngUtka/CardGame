using UnityEngine;

namespace Units.Items
{
    [CreateAssetMenu(menuName = ("Config/Head Armor"))]
    public class HeadArmor : Armor
    {
        [Header("Model parameters")]
        public GameObject HatModel;
        public bool IsHairVisible;
        public bool IsEarsVisible;
        public bool IsMoustacheVisible;

        [Header("Other")]
        public Sprite FrontImage;
    }
}
