using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class Battleground : MonoBehaviour
    {
        private const int gridVeticalStep = 2;
        private const int gridHorizontalStep = 2;

        [SerializeField] private BattlegroundCell _cellPrefab;
        [SerializeField] private Transform _cellContainer;
        private List<BattlegroundCell> _cells = new List<BattlegroundCell>();

        private void Start()
        {
            GenerateGrid(3);
        }

        private void GenerateGrid(int lineCount)
        {
            lineCount += (lineCount + 1) % 2; 

            int centerLineIndex = lineCount / 2 + 1;

            for (int i = 0; i < lineCount; i++)
            {
                var distanceToCenterLine = Mathf.Abs(centerLineIndex - i - 1);
                var lineStartPosition = new Vector2(distanceToCenterLine, i);
                GenerateGridLine(lineStartPosition, lineCount - distanceToCenterLine);  
            }
        }

        private void GenerateGridLine(Vector2 startPos, int cellsCount)
        {
            for (int i = 0; i < cellsCount; i++)
            {
                var cell = Instantiate(_cellPrefab, _cellContainer);
                cell.transform.localPosition = new Vector3(startPos.x + i * gridHorizontalStep, startPos.y * gridVeticalStep, 0);
                cell.Init(_cells.Count, cell.transform.localPosition);
                MakeTransitions(cell);
                _cells.Add(cell);
                
            }
        }

        private void MakeTransitions(BattlegroundCell cellForTransitions)
        {
            if (_cells.Count == 0)
                return;

            foreach (var cell in _cells)
            {
                var posDifference = cell.Position - cellForTransitions.Position;
                if (posDifference == new Vector2(-gridHorizontalStep, 0))
                {
                    cell.AddTransition(cellForTransitions, TransitionType.Horizontal);
                    cell.AddTransition(cell, TransitionType.Horizontal);
                }
                else if (posDifference == new Vector2(-gridHorizontalStep / 2, -gridVeticalStep))
                {
                    cell.AddTransition(cellForTransitions, TransitionType.Diagonal);
                    cell.AddTransition(cell, TransitionType.Diagonal);
                }
                else if (posDifference == new Vector2(0, -gridVeticalStep))
                {
                    cell.AddTransition(cellForTransitions, TransitionType.Vertical);
                    cell.AddTransition(cell, TransitionType.Vertical);
                }
                else if (posDifference == new Vector2(gridHorizontalStep / 2, -gridVeticalStep))
                {
                    cell.AddTransition(cellForTransitions, TransitionType.Diagonal);
                    cell.AddTransition(cell, TransitionType.Diagonal);
                }

            }
        }
    }
}
