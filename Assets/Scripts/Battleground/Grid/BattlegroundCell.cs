using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class BattlegroundCell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private List<Transition> _transitions = new List<Transition>();
     
        public int ID { get; private set; }
        public Vector2 Position { get; private set; }

        public event Action<int> IsPressed;
        
        public void Init(int id,Vector2 position)
        {
            Position = position;
            ID = id;
        }

        public void Press()
        {
            IsPressed?.Invoke(ID);
        }

        public void SetActive(bool isActive)
        {
            _spriteRenderer.color = isActive ? Color.green : Color.red;
        }

        public void AddTransition(BattlegroundCell to, TransitionType type)
        {
            var newTransition = new Transition(this, to, type);
            _transitions.Add(newTransition);
        }
    }
}
