using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class PieceEffectsHolder
    {
        private Piece _piece;
        private List<PieceEffect> _combinationsEffects = new();
        private List<PieceEffect> _noCombinationsEffects = new();

        public Action EffectsChanged;

        public void Init(Piece piece)
        {
            _piece = piece;
        }

        public void AddEffect(PieceEffect additionalEffect)
        {
            if (!additionalEffect.HasEffectsCombination)
            {
                _noCombinationsEffects.Add(additionalEffect);
                AddTimer(additionalEffect);
            }
            else
            {
                foreach (var combination in additionalEffect.EffectsCombinations)
                {
                    foreach (var effect in _combinationsEffects)
                    {
                        if (effect == combination.AdditionalEffect)
                        {
                            var resultEffect = combination.ResultEffect;
                            if (resultEffect != null)
                            {
                                _combinationsEffects.Add(resultEffect);
                                AddTimer(additionalEffect);
                            }
                            RemoveEffect(effect);
                        }
                    }
                }
            }
            EffectsChanged?.Invoke();
        }

        public void RemoveEffect(PieceEffect removableEffect)
        {
            if (_combinationsEffects.Contains(removableEffect))
                _combinationsEffects.Remove(removableEffect);
            else
                _noCombinationsEffects.Remove(removableEffect);
            EffectsChanged?.Invoke();
        }

        public AdditionalPieceAttributes GetAdditionalAttributes()
        {
            var attributes = new AdditionalPieceAttributes();
            foreach (var effect in _combinationsEffects)
                attributes += effect.PieceAttributes;
            foreach (var effect in _noCombinationsEffects)
                attributes += effect.PieceAttributes;
            return attributes;
        }

        private void AddTimer(PieceEffect effect)
        {
            if (effect.IsInfinite)
                return;

            _piece.StartCoroutine(Timer(this, effect.Duration, effect));
        }

        private IEnumerator Timer(PieceEffectsHolder holder, float time, PieceEffect effect)
        {
            yield return new WaitForSeconds(time);
            holder.RemoveEffect(effect);
        }
    }
}

