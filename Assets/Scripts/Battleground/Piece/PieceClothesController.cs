using Units;
using Units.Items;
using UnityEngine;

namespace Battleground
{
    public class PieceClothesController : MonoBehaviour
    {
        [SerializeField] private Transform _headPoint;
        [SerializeField] private Transform _bodyPoint;
        private UnitInventory _unitInventory;

        public void Init(UnitInventory inventory)
        {
            _unitInventory = inventory;
            _unitInventory.InventoryChanged += SetClothes;
            SetClothes();
        }

        private void SetClothes()
        {
            SetHeadCloth(_unitInventory.HeadArmor);
            SetBodyCloth(_unitInventory.Armor);
        }

        public void SetHeadCloth(HeadArmor armor)
        {
            ClearContainer(_headPoint);
            var inst = Instantiate(armor.HatModel, _headPoint);
            inst.transform.localPosition = Vector3.zero;
            inst.transform.localRotation = Quaternion.identity;
        }

        public void SetBodyCloth(BodyArmor armor)
        {
            ClearContainer(_bodyPoint);
            var inst = Instantiate(armor.Model, _bodyPoint);
            inst.transform.localPosition = Vector3.zero;
            inst.transform.localRotation = Quaternion.identity;
        }

        private void ClearContainer(Transform containder)
        {
            foreach (Transform child in containder)
                Destroy(child.gameObject);
        }

        private void OnEnable()
        {
            if (_unitInventory != null)
                _unitInventory.InventoryChanged += SetClothes;
        }

        private void OnDisable()
        {
            if (_unitInventory != null)
                _unitInventory.InventoryChanged -= SetClothes;
        }
    }
}