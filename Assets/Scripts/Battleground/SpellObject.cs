using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public abstract class SpellObject : MonoBehaviour, IMoveByTimeline
    {
        [SerializeField] protected float _damage;
        [SerializeField] protected float _activeTime;
        [SerializeField] protected Collider _hitbox;
        protected Timeline _timeline;
        protected Vector3 _startPos;
        protected Vector3 _endPos;
        protected float _startTime;
        protected List<IDamageable> _collidedObjects;

        public virtual void Init(Timeline timeline, Vector3 startPos, Vector3 endPos, float startTime)
        {
            _timeline = timeline;
            _startPos = startPos;
            _endPos = endPos;
            _startTime = startTime;
            _collidedObjects = new List<IDamageable>();
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

            if (isSimulation)
                foreach (var collidedObject in _collidedObjects)
                    collidedObject.ApplyDamage(_damage);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamageable obj) && _collidedObjects.Contains(obj))
                _collidedObjects.Remove(obj);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable obj) && !_collidedObjects.Contains(obj))
                _collidedObjects.Add(obj);
        }

        protected void SetHide(bool isHide)
        {
            _hitbox.enabled = !isHide;
            GetComponent<Renderer>().enabled = !isHide;
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

