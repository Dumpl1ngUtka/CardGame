using UnityEngine;

namespace Battleground
{
    public class MovePoint : MonoBehaviour
    {
        [SerializeField] private Color _default;
        [SerializeField] private Color _cantMove;
        private SpriteRenderer _image;

        public bool IsCanMoveHere { get; private set; }

        private void Awake()
        {
            _image = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            Render();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.GetComponent<Piece>())
                IsCanMoveHere = false;
            Render();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Piece>())
                IsCanMoveHere = true;
            Render();
        }

        private void Render()
        {
            _image.color = IsCanMoveHere ? _default : _cantMove;
        }
    }
}

