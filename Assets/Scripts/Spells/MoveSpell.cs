using Battleground;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/MoveSpell")]
    public class MoveSpell : Spell
    {
        public override void Start()
        {
            
        }

        public override void Update()
        {

        }

        public override void LeftMouseClick(RaycastHit hit)
        {
            if (hit.collider.GetComponent<Piece>() == null)
                Piece.Move(hit.point);
            IsSpellFinished = true;
        }

        public override void RightMouseClick(RaycastHit hit)
        {
            
        }
    }
}