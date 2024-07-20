using Battleground;
using System.Collections;
using UnityEngine;

namespace Units
{
    public abstract class Spell : ScriptableObject
    {
        public LayerMask Mask;
        protected Piece Piece;
        public bool IsSpellFinished { get; protected set; }

        public virtual void Init(Piece piece) 
        {
            IsSpellFinished = false;
            Piece = piece;
        }

        public abstract void Start();

        public abstract void Update();

        public abstract void LeftMouseClick(RaycastHit hit);

        public abstract void RightMouseClick(RaycastHit hit);
    }
}

