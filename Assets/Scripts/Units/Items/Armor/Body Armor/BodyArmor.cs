using UnityEngine;

namespace Units.Items
{
    [CreateAssetMenu(menuName = ("Config/Item/Body Armor"))]
    public class BodyArmor : Armor
    {
        [Header("Model parameters")]
        public GameObject Model;

        public override Sprite TypeIcon => Resources.Load<Sprite>("Sprites/CardTypeIcons/Armor");
    }
}
