using Battleground;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Units
{
    public class Fireball : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Collider _hitbox;
        private Player _player;
        [SerializeField] private Vector3 _startPos;
        [SerializeField] private Vector3 _endPos;
        private int _startIndex;
        private float _lifeTime;

        public void Init(Player player, Vector3 startPos, Vector3 endPos, int startIndex)
        {
            _startIndex = startIndex;
            _endPos = endPos;
            _player = player;
            _startPos = startPos;
            _lifeTime = (_startPos - _endPos).magnitude / _speed;
            _player.Timeline.OnValueChanged += MoveToTimeline;
            MoveToTimeline(0);
        }

        private void MoveToTimeline(float index)
        {
            var progressIndex = index - _startIndex;
            if (progressIndex < 0)
            {
                SetHide(true);
                return;
            }
            SetHide(false);
            transform.position = Vector3.Lerp(_startPos, _endPos, progressIndex / _lifeTime);
        }

        private void SetHide(bool isHide)
        {
            _hitbox.enabled = !isHide;
            GetComponent<Renderer>().enabled = !isHide;
        }

        private void OnDisable()
        {
            _player.Timeline.OnValueChanged -= MoveToTimeline;
        }
    }
}

