using AI;
using System.Xml.Serialization;
using UnityEngine;

namespace Battleground
{
    public abstract class PieceAbility : ScriptableObject, IObjectForInfoRenderer
    {
        private float _releaseTimer;
        private float _cooldownTimer = 0f;

        #region Main Settings
        public string Name;
        public string Description;
        public Sprite Icon;
        #endregion
        public abstract float ReleaseTime { get; }
        public abstract float Cooldown{ get; }
        public abstract bool IsCanMoveWhileUse { get; }
        public bool IsReadyToUse => _cooldownTimer <= 0f && !IsUsed;
        public bool IsUsed => _releaseTimer > 0f;

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

        public void StartRelease()
        {
            _releaseTimer = ReleaseTime;
        }

        public void Update()
        {
            if (_releaseTimer > 0f)
            {
                Release();
                _releaseTimer -= Time.deltaTime;
            }

            if (_cooldownTimer > 0f)
            {
                _cooldownTimer -= Time.deltaTime;
            }
        }

        protected abstract void Release();

        public void EndRelease()
        {
            _cooldownTimer = Cooldown;
        }
    }
}
