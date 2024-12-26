using System.Collections;
using System.Collections.Generic;
using UI.Marker;
using Units;
using UnityEngine;

namespace Battleground
{
    [CreateAssetMenu(menuName = "Config/Spells/Teleportation")]

    public class Teleportation : Spell
    {
        private Piece _target;
        private Marker _targetMarker;

        #region UI
        public override Sprite TypeIcon => Resources.Load<Sprite>("Sprites/CardTypeIcons/Spell");

        public override Sprite ParamIcon1 => Resources.Load<Sprite>("Sprites/ClassIcons/Wizzard");

        public override Sprite ParamIcon2 => Resources.Load<Sprite>("Sprites/Empty");

        public override string ParamValue1 => "0";

        public override string ParamValue2 => "0";

        #endregion

        public override void StartRelease()
        {
            base.StartRelease();
            _target = null;
            _targetMarker = Player.UI.InstantiateMarker(MarkerType.Cylinder, new Color(0.5f, 0, 0.5f), Radius);
            _targetMarker.gameObject.SetActive(false);
        }

        public override void Release(RaycastHit hit)
        {
            base.Release(hit);
            if (_target != null && _targetMarker != null)
            {
                _targetMarker.SetPosition(_target.transform.position);
            }
        }

        public override void EndRelease()
        {
            base.EndRelease();
            if (_targetMarker != null)
                Destroy(_targetMarker.gameObject);
        }

        public override void LeftMouseClick(RaycastHit hit)
        {
            base.LeftMouseClick(hit);
            if (hit.collider != null && hit.collider.TryGetComponent(out Piece piece))
            {
                if (_target == null)
                {
                    _target = piece;
                    _targetMarker.gameObject.SetActive(true);
                }
                else if (_target == piece)
                {
                    _target = null;
                    _targetMarker.gameObject.SetActive(false);
                }
                else
                {
                    (_target.transform.position, piece.transform.position) = (piece.transform.position, _target.transform.position);
                    IsSpellReleased = true;
                    Destroy(_targetMarker.gameObject);
                }
            }
        }
    }
}

