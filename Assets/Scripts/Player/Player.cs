using Units;
using UnityEngine;

namespace Battleground
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private LayerMask _interactableObjectsMask;
        [SerializeField] private BattleSceneUI _unitCardRenderer;

        private void Update()
        {
            bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, _interactableObjectsMask) && !isOverUI)
            {
                var rightMouseButtonDown = Input.GetMouseButtonDown(1);
                var unit = hit.collider.GetComponent<Piece>().Unit;
                if (unit != null && rightMouseButtonDown)
                {
                    _unitCardRenderer.ShowUnitInfo(unit);
                }
            }
        }
    }
}

