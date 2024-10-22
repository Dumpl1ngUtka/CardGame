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

        public float CurrentHealth
        {
            get { return _health; }
            private set
            {
                _health = value;
                HealthChanged?.Invoke(_health / MaxHealth);
            }
        }
        public float MaxHealth => _pieceAttributes.MaxHealth;
        public float HealthFill => CurrentHealth / MaxHealth;

        public PieceHealth(PieceAttributes pieceAttributes)
        {
            _pieceAttributes = pieceAttributes;
            CurrentHealth = MaxHealth;
        }

        public void ApplyDamage(float value)
        {
            CurrentHealth -= value;
            if (CurrentHealth <= 0)
            {
                IsDied = true;
                Died?.Invoke();
            }
        }
    }
}