using System.Linq;
using UnityEngine;

namespace Units
{
    public class Unit
    {
        private const int _additionalLevelsForStars = 2;

        private UnitClass _unitClass;
        private UnitRace _unitRace;

        public string Name { get; private set; }
        public int StarCount { get; private set; }
        public SkillLevels SkillLevels { get; private set; }

        public Spell[] Spells;

        public Unit(int starCount, UnitRace unitRace, UnitClass unitClass)
        {
            StarCount = starCount;
            _unitRace = unitRace;   
            _unitClass = unitClass;
            Name = "Test Name";
            SetSkillLevels();
            Spells = GetSpellArray();
        }

        private void SetSkillLevels()
        {
            var minSkillLevels = new int[4];
            minSkillLevels[0] = Mathf.Max(new int[3] { 1, _unitRace.MinLevels.Health, _unitClass.MinLevels.Health});
            minSkillLevels[1] = Mathf.Max(new int[3] { 1, _unitRace.MinLevels.Energy, _unitClass.MinLevels.Energy });
            minSkillLevels[2] = Mathf.Max(new int[3] { 1, _unitRace.MinLevels.Dexterity, _unitClass.MinLevels.Dexterity});
            minSkillLevels[3] = Mathf.Max(new int[3] { 1, _unitRace.MinLevels.Strength, _unitClass.MinLevels.Strength});
            SkillLevels = new SkillLevels(20 + StarCount * _additionalLevelsForStars, minSkillLevels);
        }

        public Spell[] GetSpellArray()
        {
            var spellCount = _unitClass.Spells.Length + _unitRace.Spells.Length;
            var spellArray = new Spell[spellCount];
            var index = 0;
            foreach (var spell in _unitClass.Spells)
                spellArray[index++] = spell;
            foreach (var spell in _unitRace.Spells)
                spellArray[index++] = spell;
            return spellArray;
        }
    }
}
