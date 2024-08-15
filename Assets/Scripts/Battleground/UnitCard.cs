using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground.UI
{
    public class UnitCard : MonoBehaviour
    {
        [SerializeField] private CardRenderer _renderer;

        public Unit Unit { get; private set; }

        public void Init(Unit unit)
        {
            Unit = unit;
            _renderer.Render(unit);
        }
    }
}
