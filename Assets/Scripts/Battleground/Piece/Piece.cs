using System;
using Units;
using UnityEngine;

namespace Battleground
{
    public class Piece : MonoBehaviour
    {
        [SerializeField] private UnitRace _race;
        [SerializeField] private UnitClass _class;
        [SerializeField] private GameObject _statRenderer;
        public Spell[] spells;

        public Unit Unit { get; private set; }

        private void Awake()
        {
            Unit = new Unit(1, _race, _class);
        }

        private void OnMouseOver()
        {
            bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            if (isOverUI && _statRenderer.activeSelf)
            {
                _statRenderer.SetActive(false);
                return;
            }
            if (Input.GetMouseButtonDown(0))
                MouseLeftClick();
            if (Input.GetMouseButtonDown(1))
                MouseRightClick();
        }

        private void OnMouseEnter()
        {
            bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            if (isOverUI)
                return;
            _statRenderer.SetActive(true);
        }

        private void OnMouseExit()
        {
            bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            if (isOverUI)
                return;
            _statRenderer.SetActive(false);
        }

        private void MouseLeftClick()
        {

        }

        private void MouseRightClick()
        {
            
        }
    }
}

