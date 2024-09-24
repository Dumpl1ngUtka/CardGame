using UnityEngine;
using UnityEngine.EventSystems;

namespace Battleground.UI
{
    public class CardHolderFilterButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private CardHolder _cardHolder;
        public void OnPointerClick(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }

}
