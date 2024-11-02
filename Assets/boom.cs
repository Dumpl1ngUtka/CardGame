using AI;
using Battleground;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PieceMover>(out var Piece))
        {
            var direction = (other.transform.position - transform.position).normalized;
            direction = (direction + Vector3.up).normalized;
            Piece.Rigidbody.AddForce(direction * 30000);
        }
    }
}
