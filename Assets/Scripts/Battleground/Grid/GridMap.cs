using System.Collections.Generic;
using UnityEngine;

namespace Battleground.Grid
{
    public class GridMap : MonoBehaviour
    {
        public List<GridCell> GridCells { get; private set; } = new List<GridCell>();

        private void Awake()
        {
            foreach (Transform child in transform)
                GridCells.Add(child.GetComponent<GridCell>());
            RemoveSelectedGrid();
        }

        public void SetSelectedGrid(List<GridCell> selectedCells)
        {
            foreach (var cell in GridCells)
            {
                cell.SetColor(new Color(0, 0, 0, 1));
            }
            foreach (var cell in selectedCells)
            {
                cell.SetColor(new Color(1, 1, 1, 1));
            }
        }

        public void RemoveSelectedGrid()
        {
            foreach (var cell in GridCells)
            {
                cell.SetColor(new Color(0, 1, 0, 1));
            }
        }
    }
}

