using UnityEngine;

namespace Units.Items
{
    [CreateAssetMenu(menuName = ("Config/Item/Head Armor"))]
    public class HeadArmor : Armor
    {
        [Header("Model parameters")]
        public GameObject HatModel;
        public bool IsHairVisible;
        public bool IsEarsVisible;
        public bool IsMoustacheVisible;

        public override Sprite TypeIcon => Resources.Load<Sprite>("Sprites/CardTypeIcons/Hat");
    }
}
