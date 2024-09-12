using System;

namespace Battleground
{
    public class PieceHealth
    {
        private const int _defaultHealthValue = 20;
        private const int _additionHealthValue = 2;

        private float _maxHealth;
        private float _health;
        private Piece _piece;

        public bool IsDied = false;
        public event Action<float> HealthChanged;
        public event Action Died;

        public float Health
        {
            get { return _health; }
            private set
            {
                _health = value;
                HealthChanged?.Invoke(_health / _maxHealth);
            }
        }

        public float HealthFill => Health / _maxHealth;

        public PieceHealth(Piece piece, float healthAttribute)
        {
            _piece = piece;
            _maxHealth = _defaultHealthValue + _additionHealthValue * healthAttribute;
            _health = _maxHealth;
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