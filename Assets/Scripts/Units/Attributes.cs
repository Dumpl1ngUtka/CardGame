using System;
using UnityEngine;

namespace Units
{
    [Serializable]
    public class Attributes
    {
        private const int maxLevel = 10;

        [SerializeField, Range(0, maxLevel)] private int _health;
        [SerializeField, Range(0, maxLevel)] private int _energy;
        [SerializeField, Range(0, maxLevel)] private int _dexterity;
        [SerializeField, Range(0, maxLevel)] private int _strength;
        [SerializeField, Range(0, maxLevel)] private int _intelligence;
        [SerializeField, Range(0, maxLevel)] private int _capacity;

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }
        public int Energy
        {
            get { return _energy; }
            set { _energy = value; }
        }
        public int Dexterity
        {
            get { return _dexterity; }
            set { _dexterity = value; }
        }
        public int Strength
        {
            get { return _strength; }
            set { _strength = value; }
        }

        public int Intelligence
        {
            get { return _intelligence; }
            set { _intelligence = value; }
        }
                
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        public Attributes(int skillPoints, int[] minSkillLevels)
        {
            var skillCount = minSkillLevels.Length;
            #region Exceptions

            var skillSum = 0;
            for (int i = 0; i < skillCount; i++)
            {
                skillSum += minSkillLevels[i];
                if (minSkillLevels[i] < 1 || minSkillLevels[i] > maxLevel)
                    throw new Exception("incorrect min value");
            }

            if (skillPoints <= skillSum || skillPoints > skillCount * maxLevel)
                throw new Exception("Enter the correct number of skill points");

            #endregion

            var skillLevels = new int[skillCount];
            for (int i = 0; i < skillCount; i++)
                skillPoints -= minSkillLevels[i];

            for (int i = 0; i < skillCount - 1; i++)
            {
                skillLevels[i] = minSkillLevels[i];
                var availablePoints = (skillCount - 1 - i) * maxLevel;
                for (int j = i + 1; j < skillCount; j++)
                    availablePoints -= minSkillLevels[j];
                var lowerRangeBorder = Mathf.Max(skillPoints - availablePoints, 0);

                var upperRangeBorder = Mathf.Min(maxLevel - minSkillLevels[i], skillPoints);

                var additionalPoints = UnityEngine.Random.Range(lowerRangeBorder, upperRangeBorder + 1);
                skillLevels[i] += additionalPoints;
                skillPoints -= additionalPoints;
            }

            Health = skillLevels[0];
            Energy = skillLevels[1];
            Dexterity = skillLevels[2];
            Strength = skillLevels[3];
            Intelligence = skillLevels[4];
            Capacity = minSkillLevels[5] + skillPoints;
        }
    }
}
