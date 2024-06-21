using UnityEngine;

namespace Battleground
{
    [RequireComponent(typeof(Piece))]
    public class PieceUseSpell : MonoBehaviour
    {
        private void Awake()
        {
            //StartUseSpell(GetComponent<Piece>().spell);
        }

        public void StartUseSpell(Spell spell)
        {
            //spell.StartUse();
            //StartCoroutine(spell.SetDirection());
        }
    }
}
