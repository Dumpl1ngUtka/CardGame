using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground
{
    [CreateAssetMenu(menuName = "Config/Spells/AddEffectSpell")]
    public class AddEffectSpell : Spell
    {
        [SerializeField] private PieceEffect _effect;
        [SerializeField] private Sprite _typeIcon;
        [SerializeField] private Sprite _paramIcon1;
        [SerializeField] private Sprite _paramIcon2;
        [SerializeField] private string _paramValue;

        public override Sprite TypeIcon => _typeIcon;

        public override Sprite ParamIcon1 => _paramIcon1;

        public override Sprite ParamIcon2 => _paramIcon2;

        public override string ParamValue1 => _effect.IsInfinite? _effect.Duration.ToString() : "INF";

        public override string ParamValue2 => _paramValue;

        public override void LeftMouseClick(RaycastHit hit)
        {
            base.LeftMouseClick(hit);
            var objects = Physics.OverlapSphere(hit.point, Radius);
            foreach (var item in objects)
            {
                if (item.TryGetComponent<IEffectHolder>(out var effectHolder))
                {
                    effectHolder.EffectsHolder.AddEffect(_effect);
                }
            }
            IsSpellReleased = true;
        }
    }
}



