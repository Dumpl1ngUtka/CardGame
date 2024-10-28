using AI;
using Battleground;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PieceMover>(out var IAIWait))
        {
            Debug.Log("ADD FORCE");

            IAIWait.Rigidbody.AddForce(Vector3.up * 10);
        }
    }
}
