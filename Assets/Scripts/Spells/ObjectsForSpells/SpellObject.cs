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
        protected Timeline _timeline;
        protected Vector3 _startPos;
        protected Vector3 _endPos;
        protected float _startTime;
        protected Piece _piece;

        public virtual void Init(Piece piece, Vector3 startPos, Vector3 endPos, float startTime)
        {
            _piece = piece;
            _timeline = piece.Player.Timeline;
            _startPos = startPos;
            _endPos = endPos;
            _startTime = startTime;
            _timeline.OnTimeChanged += MoveByTimeline;
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
            _timeline.OnTimeChanged -= MoveByTimeline;
        }

        public virtual void NextMove()
        {
        }
    }
}

