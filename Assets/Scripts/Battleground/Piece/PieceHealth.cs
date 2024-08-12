using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Battleground
{
    [RequireComponent(typeof(Piece))]
    public class PieceHealth
    {
        private const int _defaultHealthValue = 20;
        private const int _additionHealthValue = 2;

        private int _maxHealth;
        private int _health;
        private Piece _piece;

        public event Action<float> HealthChanged;
        public event Action Died;

        public int Health
        {
            get { return _health; }
            private set
            {
                _health = value;
                HealthChanged?.Invoke((float)_health / _maxHealth);
            }
        }

        public PieceHealth(Piece piece, int healthAttribute)
        {
            _piece = piece;
            _maxHealth = _defaultHealthValue + _additionHealthValue * healthAttribute;
            _health = _maxHealth;
        }

        public void ApplyDamage(int value)
        {
            Health -= value;
            if (Health < 0)
                Died?.Invoke();
        }
    }
}