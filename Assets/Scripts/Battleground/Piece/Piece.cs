using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground
{
    public class Piece : MonoBehaviour, IObjectForCardRenderer
    {
        [SerializeField] private PieceUIRenderer _UIRenderer;
        public PieceHealth Health { get; private set; }
        public Unit Unit { get; private set; }

        public ActivitiesList Activities;

        public Player Player { get; private set; }

        public void Init(Unit unit, Player player)
        {
            Unit = unit;
            Health = new PieceHealth(this, unit.SkillLevels.Health);
            Health.Died += Died;
            _UIRenderer.Init(this);
            Player = player;

            NewMove(Player.Timeline.MaxIndex);
        }

        private void Died()
        {
        }



        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(Activities.Release());
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
        }

        public void NewMove(int indexCount)
        {
            Activities = new ActivitiesList(indexCount);
        }
    }

    public class ActivitiesList
    {
        private int _indexCount;
        private List<Activity> _activities;
        public ActivitiesList(int indexCount)
        {
            _activities = new List<Activity>();
            _indexCount = indexCount;
        }

        public bool AddAction(Activity actionEnumerator)
        {
            if (actionEnumerator.StartIndex + actionEnumerator.StepCount >= _indexCount)
                return false;

            var activitiesIndexes = GetStartEndIndexes();
            
            foreach (var activity in activitiesIndexes)
            {
                bool startInOtherActivity = actionEnumerator.StartIndex > activity[0] 
                    && actionEnumerator.StartIndex < activity[1];
                bool endInOtherActivity = actionEnumerator.EndIndex > activity[0] 
                    && actionEnumerator.EndIndex < activity[1];
                if (startInOtherActivity || endInOtherActivity)
                {
                    return false;
                }
            }

            _activities.Add(actionEnumerator);

            //

            

            return true;
        }

        private List<int> GetStartIndexes()
        {
            List<int> indexes = new();
            foreach (var enumerator in _activities)
            {
                indexes.Add(enumerator.StartIndex);
            }
            return indexes;
        }

        private List<int[]> GetStartEndIndexes()
        {
            List<int[]> indexes = new();
            foreach (var enumerator in _activities)
            {
                indexes.Add(new int[2] { enumerator.StartIndex, enumerator.EndIndex });
            }
            return indexes;
        }

        private Activity GetEnumeratorByStartIndex(int startIndex)
        {
            foreach (var enumerator in _activities)
                if (enumerator.StartIndex == startIndex)
                    return enumerator;
            
            return null;
        }

        public IEnumerator Release()
        {
            var delay = new WaitForSeconds(0.1f);
            var startIndexes = GetStartIndexes();

            Activity currentActionEnumerator = null;

            for (int i = 0; i < _indexCount; i++)
            {
                if (startIndexes.Contains(i))
                    currentActionEnumerator = GetEnumeratorByStartIndex(i);

                if (currentActionEnumerator != null && !currentActionEnumerator.MoveNext())
                    currentActionEnumerator = null;
 
                yield return delay;
            }
        }
    }

    public class Activity : IEnumerator
    {
        public readonly IEnumerator ActionEnumerator;
        public readonly int StepCount;
        public readonly int StartIndex;

        public int EndIndex => StartIndex + StepCount;

        public object Current => ActionEnumerator.Current;
        public bool MoveNext() => ActionEnumerator.MoveNext();
        public void Reset() => ActionEnumerator.Reset();
        //public bool MovePrevious() => ActionEnumerator.MoveNext();

        public Activity(IEnumerator actionEnumerator, int stepCount, int startIndex)
        {
            ActionEnumerator = actionEnumerator;
            StepCount = stepCount;
            StartIndex = startIndex;
        }
    }
}



