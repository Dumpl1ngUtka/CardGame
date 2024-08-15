using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

public class ListUnitsFromMainScene : MonoBehaviour
{
    [SerializeField] private UnitRace[] _races;
    [SerializeField] private UnitClass[] _classes;

    public Unit[] Units;

    private void Awake()
    {
        Units = new[]
        {
            new Unit(3,  _races[0], _classes[0]),
            new Unit(1,  _races[1], _classes[1]),
            new Unit(2,  _races[2], _classes[2]),
            new Unit(3,  _races[3], _classes[3]),
        };
    }
}
