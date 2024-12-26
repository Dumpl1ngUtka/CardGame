using Units;
using UnityEngine;

namespace Battleground
{
    [CreateAssetMenu(menuName = "Config/Spells/KineticBoom")]
    public class KineticBoom : Spell
    {
        public override Sprite TypeIcon => Resources.Load<Sprite>("Sprites/CardTypeIcons/Spell");

        public override Sprite ParamIcon1 => Resources.Load<Sprite>("Sprites/ClassIcons/Wizzard");

        public override Sprite ParamIcon2 => Resources.Load<Sprite>("Sprites/Empty");

        public override string ParamValue1 => "0";

        public override string ParamValue2 => "0";

        public override void LeftMouseClick(RaycastHit hit)
        {
            base.LeftMouseClick(hit);
            var objects = Physics.OverlapSphere(hit.point, Radius);
            foreach (var item in objects)
            {
                if (item.TryGetComponent<PieceMover>(out var Piece))
                {
                    var direction = item.transform.position - hit.point;
                    direction = (direction + Vector3.up * 2).normalized;
                    Piece.Rigidbody.AddForce(direction * 40000);
                }
            }
            IsSpellReleased = true;
        }
    }
}

