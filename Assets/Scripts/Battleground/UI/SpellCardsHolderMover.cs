using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground.UI
{
    public class SpellCardsHolderMover : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;
        [SerializeField] private float _speed;

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _pivot.position, Time.deltaTime * _speed); 
            transform.rotation = Quaternion.Lerp(transform.rotation, _pivot.rotation, Time.deltaTime * _speed); 
        }
    }
}

