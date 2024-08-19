using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battleground
{
    [RequireComponent(typeof(Piece))]
    public class PieceUIRenderer : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private TMP_Text _healthValue;
        [SerializeField] private Image _powerBar;
        private PieceHealth _pieceHealth;

        public void Init(Piece piece)
        {
            _pieceHealth = piece.Health;
            _pieceHealth.HealthChanged += ChangeHealth;
            ChangeHealth(1);
        }

        private void ChangeHealth(float fillValue)
        {
            _healthBar.fillAmount = fillValue;
            //_healthValue.text = _healthBar.fillAmount.ToString();
        }

        private void OnDisable()
        {
            _pieceHealth.HealthChanged -= ChangeHealth;
        }
    }

}
