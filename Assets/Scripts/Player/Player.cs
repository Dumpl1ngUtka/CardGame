using System;
using Units;
using UnityEngine;
using Battleground.UI;

namespace Battleground
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private LayerMask _interactableObjectsMask;
        [SerializeField] private BattleSceneUI _unitCardRenderer;
        public Unit CurrentUnit { get; private set; }

        private void Update()
        {
            bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, _interactableObjectsMask) && !isOverUI)
            {
                var unit = hit.collider.GetComponent<Piece>()?.Unit;
                if (unit == null)
                    return;

                if (Input.GetMouseButtonDown(0))
                    LeftMouseButtonDown(unit);

                if (Input.GetMouseButtonDown(1))
                    RightMouseButtonDown(unit);
            }
        }

        private void RightMouseButtonDown(Unit unit)
        {
            Debug.Log(unit);
            _unitCardRenderer.ShowUnitInfo(unit);
        }

        private void LeftMouseButtonDown(Unit unit)
        {
            Debug.Log(unit);
            CurrentUnit = unit;
        }
    }
}

