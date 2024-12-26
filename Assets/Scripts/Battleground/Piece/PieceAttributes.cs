using System;
using Units;

namespace Battleground
{
    [Serializable]
    public class PieceAttributes
    {
        private Attributes _unitAttributes;
        private UnitInventory _unitInventory;
        private PieceEffectsHolder _effectHolder;
        private AdditionalPieceAttributes _sumAttibutes;
        private AdditionalPieceAttributes _inventoryAttibutes;
        private AdditionalPieceAttributes _effectsAttibutes;

        public float MaxHealth
        {
            get
            {
                float value = 50 + _unitAttributes.Health * 10;
                value += _sumAttibutes.Health;
                value += value * _sumAttibutes.HealthPercent / 100;
                return value;
            }
        }
        public float AccuracyPercent
        {
            get
            {
                return _sumAttibutes.AccuracyPercent;
            }
        }
        public float DodgeChancePercent
        {
            get
            {
                float value = _unitAttributes.Dexterity * 2f;
                value += _sumAttibutes.DodgeChancePercent;
                return value;
            }
        }
        public float BlockChancePercent
        {
            get
            {
                float value = 0;
                value += _sumAttibutes.BlockChancePercent;
                return value;
            }
        }
        public float MaxWeight
        {
            get
            {
                float value = 30 + _unitAttributes.Capacity * 5f;
                value += _sumAttibutes.MaxWeight;
                return value;
            }
        }
        public float Weight
        {
            get
            {
                return _unitInventory.GetItemsWeight();
            }
        }
        public float MeleeAttackRangePercent
        {
            get
            {
                return 100 + _sumAttibutes.MeleeAttackRangeAdditionPercent;
            }
        }
        public float MoveSpeed
        {
            get
            {
                return 2 + ((float)_unitAttributes.Dexterity / 3 ) + _sumAttibutes.MoveSpeed;
            }
        }


        #region Distance Attack
        public float DistanceAttackRangePercent
        {
            get
            {
                return 100 + _sumAttibutes.DistanceAttackRangeAdditionPercent;
            }
        }        
        public float DistanceAttackDistancePercent
        {
            get
            {
                return 100 + _sumAttibutes.DistanceAttackDistanceAdditionPercent;
            }
        }
        #endregion

        public PieceAttributes(Piece piece)
        {
            _unitAttributes = piece.Unit.Attributes;
            _unitInventory = piece.Unit.Inventory;
            _unitInventory.InventoryChanged += InventoryChanged;

            _effectHolder = piece.EffectsHolder;
            _effectHolder.EffectsChanged += EffectsChanged;
        }

        private void InventoryChanged()
        {
            _inventoryAttibutes = _unitInventory.GetAdditionalAttributes();
            _sumAttibutes = _inventoryAttibutes + _effectsAttibutes;
        }

        private void EffectsChanged()
        {
            _effectsAttibutes = _effectHolder.GetAdditionalAttributes();
            _sumAttibutes = _inventoryAttibutes + _effectsAttibutes;
        }
    }
}

