using System;
using UI;
using Units;
using UnityEngine;

namespace Guild
{
    public class CardsGenerator : MonoBehaviour
    {
        [SerializeField] private UnitRace[] _availableRaces;
        [SerializeField] private UnitClass[] _availableClasses;

        protected Unit GenerateUnit()
        {
            var starCount = UnityEngine.Random.Range(0, 6);
            var unitRace = GetRandomFrom(_availableRaces);
            var unitClass = GetRandomFrom(_availableClasses);
            return new Unit(starCount, unitRace, unitClass);
        }

        private T GetRandomFrom<T>(T[] list)
        {
            var index = UnityEngine.Random.Range(0, list.Length);
            return list[index];
        }
    }
}