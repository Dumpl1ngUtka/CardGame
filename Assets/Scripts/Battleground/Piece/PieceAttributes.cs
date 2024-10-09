using System;

namespace Battleground
{
    [Serializable]
    public class PieceAttributes
    {
        private Units.Attributes _unitAttributes;
        private Units.UnitInventory _unitInventory;
        private AdditionalPieceAttributes _additionalPieceAttributes;

        public float MaxHealth
        {
            get
            {
                float value = 50 + _unitAttributes.Health * 10;
                value += _additionalPieceAttributes.Health;
                value *= _additionalPieceAttributes.HealthPercent / 100;
                return value;
            }
        }
        public float AccuracyPercent
        {
            get
            {
                return _additionalPieceAttributes.AccuracyPercent;
            }
        }
        public float DodgeChancePercent
        {
            get
            {
                float value = _unitAttributes.Dexterity * 2f;
                value += _additionalPieceAttributes.DodgeChancePercent;
                return value;
            }
        }
        public float BlockChancePercent
        {
            get
            {
                float value = 0;
                value += _additionalPieceAttributes.BlockChancePercent;
                return value;
            }
        }
        public float MaxWeight
        {
            get
            {
                float value = 30 + _unitAttributes.Capacity * 5f;
                value += _additionalPieceAttributes.MaxWeight;
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
                return 100 + _additionalPieceAttributes.MeleeAttackRangeAdditionPercent;
            }
        }

        #region Distance Attack
        public float DistanceAttackRangePercent
        {
            get
            {
                return 100 + _additionalPieceAttributes.DistanceAttackRangeAdditionPercent;
            }
        }        
        public float DistanceAttackDistancePercent
        {
            get
            {
                return 100 + _additionalPieceAttributes.DistanceAttackDistanceAdditionPercent;
            }
        }
        #endregion

        public PieceAttributes(Piece piece)
        {
            _unitAttributes = piece.Unit.Attributes;
            _unitInventory = piece.Unit.Inventory;
            _unitInventory.InventoryChanged += InventoryChanged;
        }

        public void InventoryChanged()
        {
            _additionalPieceAttributes = _unitInventory.GetAdditionalAttributes();
        }
    }
}

