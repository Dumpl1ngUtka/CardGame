using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Units
{
    public class Unit
    {
        private const int _additionalLevelsForStars = 2;
        private const int _defaultSkillPoints = 25;

        public string Name { get; private set; }
        public UnitClass Class { get; private set; }
        public UnitRace Race { get; private set; }
        public int StarCount { get; private set; }
        public SkillLevels SkillLevels { get; private set; }
        public UnitInventory Inventory { get; private set; }

        public Spell[] Spells => GetSpellArray();

        public Unit(int starCount, UnitRace unitRace, UnitClass unitClass)
        {
            StarCount = starCount;
            Race = unitRace;   
            Class = unitClass;
            Name = Race.GetRandomName();
            Inventory = new UnitInventory();
            SetSkillLevels();
        }

        private void SetSkillLevels()
        {
            var minSkillLevels = new int[6];
            minSkillLevels[0] = Mathf.Max(new int[3] { 1, Race.MinLevels.Health, Class.MinLevels.Health});
            minSkillLevels[1] = Mathf.Max(new int[3] { 1, Race.MinLevels.Energy, Class.MinLevels.Energy });
            minSkillLevels[2] = Mathf.Max(new int[3] { 1, Race.MinLevels.Dexterity, Class.MinLevels.Dexterity});
            minSkillLevels[3] = Mathf.Max(new int[3] { 1, Race.MinLevels.Strength, Class.MinLevels.Strength});
            minSkillLevels[4] = Mathf.Max(new int[3] { 1, Race.MinLevels.Intelligence, Class.MinLevels.Intelligence});
            minSkillLevels[5] = Mathf.Max(new int[3] { 1, Race.MinLevels.Capacity, Class.MinLevels.Capacity});
            SkillLevels = new SkillLevels(_defaultSkillPoints + StarCount * _additionalLevelsForStars, minSkillLevels);
        }

        public Spell[] GetSpellArray()
        {
            var spells = new List<Spell>();
            spells.AddRange(Class.Spells);
            spells.AddRange(Race.Spells);
            spells.AddRange(Inventory.GetSpells());
            spells = spells.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            return spells.ToArray();
        }
    }
}
