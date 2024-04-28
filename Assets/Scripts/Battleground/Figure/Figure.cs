using System;
using Units;
using UnityEngine;

namespace Battleground
{
    public class Figure : MonoBehaviour
    {
        private Unit _unit;

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
                MouseLeftClick();
            if (Input.GetMouseButtonDown(1))
                MouseRightClick();
        }

        private void MouseLeftClick()
        {

        }

        private void MouseRightClick()
        {
            
        }
    }
}

