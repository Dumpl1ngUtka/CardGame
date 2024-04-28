using System;
using UI;
using Units;
using UnityEngine;

namespace Guild
{
    public class CandsGenerator : MonoBehaviour
    {
        [SerializeField] private GuildUnitCard _unitCard;
        [SerializeField] private UnitRace[] _availableRaces;
        [SerializeField] private UnitClass[] _availableClasses;

        private void Start()
        {
            GenerateCard();
        }

        private void GenerateCard()
        {
            var starCount = UnityEngine.Random.Range(0, 6);
            var unitRace = GetRandomFrom(_availableRaces);
            var unitClass = GetRandomFrom(_availableClasses);
            var unit = new Unit(starCount, unitRace, unitClass);
            _unitCard.Init(unit);
        }

        private T GetRandomFrom<T>(T[] list)
        {
            var index = UnityEngine.Random.Range(0, list.Length);
            return list[index];
        }
    }
}