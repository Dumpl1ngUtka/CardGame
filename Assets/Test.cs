using UnityEngine;
using Battleground;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Piece pieceHealth))
        {
            pieceHealth.ApplyDamage(10);

        }
    }
}
