using Battleground;
using System.Collections;
using UnityEngine;


namespace Units
{
    public abstract class Spell : ScriptableObject, IObjectForCardRenderer
    {
        public string Name;
        public LayerMask Mask;
        public Sprite SpecializationIcon;
        public Sprite MainBackground;
        public int Ñonsumption;
        public int StepCount;

        protected Piece Piece;
        public int StartIndex { get; protected set; }
        public int EndIndex => StartIndex + StepCount;

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

        public InfoForCardRenderer GetInfo()
        {
            return new InfoForCardRenderer
            {
                Title = Name,
                UnderTitle = Ñonsumption.ToString(),
            };
        }

        public abstract void Release(float time = 0);
    }
}

