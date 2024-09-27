using System;

namespace Battleground
{
    public class PieceHealth
    {
        private PieceAttributes _pieceAttributes;
        private float _health;

        public bool IsDied = false;
        public event Action<float> HealthChanged;
        public event Action Died;

        public float Health
        {
            get { return _health; }
            private set
            {
                _health = value;
                HealthChanged?.Invoke(_health / MaxHealth);
            }
        }
        public float MaxHealth => _pieceAttributes.MaxHealth;
        public float HealthFill => Health / MaxHealth;

        public PieceHealth(PieceAttributes pieceAttributes)
        {
            _pieceAttributes = pieceAttributes;
            Health = MaxHealth;
        }

        public void ApplyDamage(float value)
        {
            Health -= value;
            if (Health <= 0)
            {
                IsDied = true;
                Died?.Invoke();
            }
        }
    }
}