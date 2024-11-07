using AI;
using System;
using System.Collections;
using UnityEngine;

namespace Battleground
{
    public abstract class PieceAbility : ScriptableObject, IObjectForInfoRenderer
    {
        #region Main Fields
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _releaseTime;
        [SerializeField] private float _cooldown;
        [SerializeField] private bool _isCanMoveWhileUse;
        #endregion

        private float _releaseTimer = 0f;
        private bool _isReadyToUse = true;

        #region Properties
        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public float ReleaseTime => _releaseTime;
        public float Cooldown => _cooldown;
        public bool IsCanMoveWhileUse => _isCanMoveWhileUse;
        public bool IsReadyToUse => _isReadyToUse;

        #endregion

        public Action ReleaseOver { get; set; }
        public Action CooldownOver { get; set; }

        protected PieceState CallingState;

        public InfoForInfoRenderer GetInfo()
        {
            return new InfoForInfoRenderer
            {
                Title = Name,
                UnderTitle = Description,
                ContentLine1 = "Release time = " + ReleaseTime,
                ContentLine2 = "Cooldown time = " + Cooldown,
            };
        }

        public abstract float GetMetric(SituationAnalyzer situationAnalyzer);

        public virtual void StartRelease(PieceState pieceState)
        {
            CallingState = pieceState;
            _isReadyToUse = false;
            _releaseTimer = ReleaseTime;
        }

        public virtual void Update()
        {
            if (_releaseTimer > 0f)
            {
                Release();
                _releaseTimer -= Time.deltaTime;
            }
            else
            {    
                EndRelease();
            }
        }

        public IEnumerator Charge()
        {
            yield return new WaitForSeconds(Cooldown);
            _isReadyToUse = true;
            CooldownOver?.Invoke();
        }

        protected abstract void Release();

        public virtual void EndRelease()
        {
            ReleaseOver.Invoke();
        }
    }
}
