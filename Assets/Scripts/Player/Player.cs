using System;
using Units;
using UnityEngine;
using Battleground.UI;

namespace Battleground
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private LayerMask _interactableObjectsMask;

        public BattleSceneUI UI;
        public Unit CurrentUnit { get; private set; }

        private void Update()
        {
            bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, _interactableObjectsMask) && !isOverUI)
            {
                if (!hit.collider.TryGetComponent<IInteractableForPlayer>(out var interactableObject))
                    return;

                if (Input.GetMouseButtonDown(0))
                    interactableObject.LeftMouseButtonDown(this);

                if (Input.GetMouseButtonDown(1))
                    interactableObject.RightMouseButtonDown(this);
            }
        }
    }
}

