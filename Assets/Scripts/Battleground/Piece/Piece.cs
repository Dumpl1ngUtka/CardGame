using Battleground.Grid;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;

namespace Battleground
{
    public class Piece : MonoBehaviour, IObjectForInfoRenderer, IMoveByTimeline, IDamageable, ICameraPivot
    {
        //[SerializeField] private PieceUIRenderer _UIRenderer;
        private float _timeMaxTime => Player.Timeline.MaxTime;

        public Animator Animator;
        public NavMeshAgent Agent { get; private set; }
        public PieceClothesController Clothes { get; private set; }
        public PieceAttributes Attributes { get; private set; }
        public PieceHealth Health { get; private set; }
        public Unit Unit { get; private set; }
        public Player Player { get; private set; }
        public List<Spell> Activities { get; private set; }
        public Vector3 PivotPosition => transform.position;
        public Transform PivotTransform => transform;
        public Vector3 GridPosition => CurrentGridCell != null? CurrentGridCell.GridPosition : Vector3.one * -1;
        public GridCell CurrentGridCell;

        public void Init(Unit unit, Player player)
        {
            Unit = unit;
            Agent = GetComponent<NavMeshAgent>();

            Clothes = GetComponent<PieceClothesController>();
            Clothes.Init(unit.Inventory);

            Attributes = new(this);
            Health = new(Attributes);
            Health.Died += Died;
            //_UIRenderer.Init(this);
            Player = player;
            Player.Timeline.OnTimeChanged += MoveByTimeline;
            NextMove();
        }

        public void SetPosition(GridCell gridCell)
        {
            CurrentGridCell = gridCell;
        }

        private void Died()
        {
        }

        public InfoForInfoRenderer GetInfo()
        {
            var info = Unit.GetInfo();
            info.HealthBarFill = Health.HealthFill;
            return info;
        }

        private void OnDisable()
        {
            Health.Died -= Died;
            Player.Timeline.OnTimeChanged -= MoveByTimeline;
        }

        public void NextMove()
        {
            Activities = new();
        }

        public void MoveByTimeline(float index, bool isSimulation)
        {
            bool indexInSpell = false;

            foreach (var spell in Activities)
            {
                if (index >= spell.StartTime && index <= spell.EndTime)
                {
                    indexInSpell = true;
                    spell.Release(index - spell.StartTime);
                    break;
                }
            }

            if (!indexInSpell)
            {
                Spell lastSpell = null;
                foreach (var spell in Activities)
                    if (index > spell.EndTime && (lastSpell == null || spell.EndTime > lastSpell.EndTime))
                        lastSpell = spell;
                lastSpell?.Release(lastSpell.ActionTime);
            }
        }

        public bool AddActivity(Spell newActivity)
        {
            if (newActivity.EndTime > _timeMaxTime)
                return false;

            foreach (var activity in Activities)
            {
                var startInOtherActivity = newActivity.StartTime >= activity.StartTime &&
                    newActivity.StartTime <= activity.EndTime;
                var endInOtherActivity = newActivity.EndTime >= activity.StartTime &&
                    newActivity.EndTime <= activity.EndTime;
                if (startInOtherActivity || endInOtherActivity)
                    return false;
            }

            RemoveActivityByStartTime(newActivity.EndTime);

            Activities.Add(newActivity);
            return true;
        }

        public void RemoveActivityByStartTime(float startTime)
        {
            MoveByTimeline(startTime, false);
            for (int i = 0; i < Activities.Count; i++)
            {
                if (Activities[i].StartTime >= startTime)
                {
                    Activities[i].RemoveFromTimeline();
                    Activities.Remove(Activities[i--]);
                }
            }
            Player.StateMachine.UI.ShowInfo(this);
        }

        public void ApplyDamage(Damage damage)
        {
            Health.ApplyDamage(damage.Value);
        }
    }
 }



