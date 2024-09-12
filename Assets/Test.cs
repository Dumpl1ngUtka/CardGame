using UnityEngine;
using Battleground;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Piece piece))
        {
            piece.ApplyDamage(new Damage(10, piece));
        }
    }
}
