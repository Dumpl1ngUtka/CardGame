using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CellsProgressBar : MonoBehaviour
    {
        [SerializeField] private Image[] _cell;
        [SerializeField] private Sprite _fillSprite;
        [SerializeField] private Sprite _emptySprite;

        public void Render(int cellCount)
        {
            #region Exceptions
            if (cellCount > _cell.Length || cellCount < 0)
                throw new Exception("Enter the correct star count");
            #endregion
            Debug.Log(cellCount);

            for (int i = 0; i < _cell.Length; i++)
            {
                if (i <= cellCount - 1)
                    _cell[i].sprite = _fillSprite;
                else
                    _cell[i].sprite = _emptySprite;
            }
        }
    }
}
