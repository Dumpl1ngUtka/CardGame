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

        private void MoveToTimeline(float index)
        {
            bool indexInSpell = false;

            foreach (var spell in Activities)
            {
                if (index >= spell.StartIndex && index <= spell.EndIndex)
                {
                    indexInSpell = true;
                    spell.Release(index - spell.StartIndex);
                    break;
                }
            }

            if (!indexInSpell)
            {
                Spell lastSpell = null;
                foreach (var spell in Activities)
                    if (index > spell.EndIndex && (lastSpell == null || spell.EndIndex > lastSpell.EndIndex))
                        lastSpell = spell;
                lastSpell?.Release(lastSpell.StepCount);
            }
        }

        public bool AddActivity(Spell newActivity)
        {
            if (newActivity.EndIndex > _timelineSize)
                return false;

            foreach (var activity in Activities)
            {
                var startInOtherActivity = newActivity.StartIndex >= activity.StartIndex &&
                    newActivity.StartIndex <= activity.EndIndex;
                var endInOtherActivity = newActivity.EndIndex >= activity.StartIndex &&
                    newActivity.EndIndex <= activity.EndIndex;
                if (startInOtherActivity || endInOtherActivity)
                    return false;
            }

            Activities.Add(newActivity);
            return true;
        }

        public void RemoveActivityByStartIndex(int startIndex)
        {
            MoveToTimeline(startIndex);
            for (int i = 0; i < Activities.Count; i++)
            {
                if (Activities[i].StartIndex >= startIndex)
                {
                    Activities[i].RemoveFromTimeline();
                    Activities.Remove(Activities[i--]);
                }
            }
            Player.StateMachine.UI.ShowInfo(this);
        }
    }

 }



