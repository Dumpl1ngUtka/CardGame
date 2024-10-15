using Battleground;
using Battleground.Grid;
using System.Collections.Generic;
using UI.Marker;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/SimpleZone")]

    public class ZoneSpell : Spell
    {
        [SerializeField] private SimpleZone _zonePrefab;

        private SimpleZone _zone;

        private ZoneMarker _marker;

        public override void LeftMouseClick(RaycastHit hit)
        {
            StartTime = Piece.Player.Timeline.GetTime;
            if (Piece.AddActivity(this))
            {
                _zone = Instantiate(_zonePrefab);
                _zone.Init(Piece, _marker.MarkerPos(), ActionTime * 0.7f + StartTime);
                IsSpellFinished = true;
            }
            else
                Debug.Log("Ќедостаточно времени на выполнение заклинани€");
        }
        public override void Release(float time = 0)
        {
            Piece.Animator.Play("MagicSpell");
            Piece.Animator.SetFloat("Progress", time/ActionTime);
        }

        public override void RightMouseClick(RaycastHit hit)
        {
        }

        public override void RemoveFromTimeline()
        {
            if (_zone.gameObject != null)
                Destroy(_zone.gameObject); 
        }

        public override void Update()
        {
        }

        public override void StartRelease()
        {
            _zonePrefab.Init(Piece, StartTime);
            var range = GetRange();
            _marker = Instantiate(MarkerPrefab) as ZoneMarker;
            _marker.Init(Piece, Mask, _zonePrefab, range);
            Piece.Player.Map.SetSelectedGrid(range);
        }

        private List<GridCell> GetRange()
        {
            var range = new List<GridCell>();
            foreach (GridCell cell in Piece.Player.Map.GridCells)
            {
                if (GridCell.GetDistance(cell, Piece.CurrentGridCell) <= _zonePrefab.MaxDistance)
                {
                    range.Add(cell);
                }
            }
            return range;
        }

        public override void EndRelease()
        {
            Destroy(_marker.gameObject);
            Piece.Player.Map.RemoveSelectedGrid();
        }
    }
}