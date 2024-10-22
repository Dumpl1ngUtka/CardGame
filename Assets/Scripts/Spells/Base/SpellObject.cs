using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public abstract class SpellObject : MonoBehaviour, IMoveByTimeline
    {
        [SerializeField] protected float _damage;
        [SerializeField] protected float _activeTime;
        [SerializeField] protected Collider _hitbox;
        [SerializeField] protected Renderer _renderer;
        protected float _startTime;
        protected Piece _piece;

        public virtual void Init(Piece piece, float startTime)
        {
            _piece = piece;
            _startTime = startTime;
            MoveByTimeline(0);
        }

        public virtual void MoveByTimeline(float time, bool isSimulation = false)
        {
            var currentTime = time - _startTime;
            if (currentTime < 0)
            {
                SetHide(true);
                return;
            }
            if (currentTime > _activeTime)
            {
                if (isSimulation)
                    Destroy(gameObject);
                else
                    SetHide(true);
                return;
            }
            SetHide(false);
        }

        protected void SetHide(bool isHide)
        {
            if (_hitbox != null)
                _hitbox.enabled = !isHide;
            if (_renderer != null)
                _renderer.enabled = !isHide;
        }

        private void OnDisable()
        {
        }

        public virtual void NextMove()
        {
        }
    }
}

