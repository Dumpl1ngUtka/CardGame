using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground
{
    public class Piece : MonoBehaviour, IObjectForCardRenderer
    {
        [SerializeField] private PieceUIRenderer _UIRenderer;
        private int _timelineSize => Player.Timeline.MaxIndex;

        public PieceHealth Health { get; private set; }
        public Unit Unit { get; private set; }
        public Player Player { get; private set; }
        public List<Spell> Activities { get; private set; }

        public void Init(Unit unit, Player player)
        {
            Unit = unit;
            Health = new PieceHealth(this, unit.SkillLevels.Health);
            Health.Died += Died;
            _UIRenderer.Init(this);
            Player = player;
            Player.Timeline.OnValueChanged += MoveToTimeline;
            NewMove(Player.Timeline.MaxIndex);
        }


        private void Died()
        {
        }

        public void Move(Vector3 position)
        {
            transform.position = position;
        }

        public void ApplyDamage(int value)
        {
            Health.ApplyDamage(value);
        }

        public InfoForCardRenderer GetInfo()
        {
            var info = Unit.GetInfo();
            info.HealthBarFill = Health.HealthFill;
            return info;
        }

        private void OnDisable()
        {
            Health.Died -= Died;
            Player.Timeline.OnValueChanged -= MoveToTimeline;
        }

        public void NewMove(int indexCount)
        {
            Activities = new();
        }

        private void MoveToTimeline(int index)
        {
            var startAndEndIndexes = new List<int[]>();
            foreach (var spell in Activities)
            {
                startAndEndIndexes.Add(new int[2] { spell.StartIndex, spell.EndIndex});
            }

            foreach (var startEndIndex in startAndEndIndexes)
            {
                if (index >= startEndIndex[0] && index <= startEndIndex[1])
                {
                    foreach (var spell  in Activities)
                    {
                        if (spell.StartIndex == startEndIndex[0])
                        {
                            spell.Release(index - startEndIndex[0]);
                        }
                    }
                }
            }
        }

        public bool AddActivity(Spell activity)
        {
            if (activity.EndIndex > _timelineSize)
                return false;

            var startEndIndexesPair = GetStartEndIndexes();

            foreach (var startEndIndex in startEndIndexesPair)
            {
                var startInOtherActivity = activity.StartIndex > startEndIndex.Key && 
                    activity.StartIndex < startEndIndex.Value;
                var endInOtherActivity = activity.EndIndex > startEndIndex.Key && 
                    activity.EndIndex < startEndIndex.Value;
                if (startInOtherActivity || endInOtherActivity)
                    return false;
            }

            Activities.Add(activity);
            return true;
        }

        private Dictionary<int,int> GetStartEndIndexes()
        {
            var startEndPair = new Dictionary<int,int>();    
            foreach (var activity in Activities)
            {
                startEndPair.Add(activity.StartIndex, activity.EndIndex);
            }
            return startEndPair;
        }
    }

 }



