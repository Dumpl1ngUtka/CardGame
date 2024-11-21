using Battleground;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/InstantiatePiese")]
    public class InstantiatePiece : Spell
    {
        [SerializeField] private Piece _piecePrefab;
        private Transform _pieceConteiner;
        private Unit _unit;

        #region CardUI
        public override Sprite Image => _unit.Race.Icon;

        public override Sprite TypeIcon => Resources.Load<Sprite>("Sprites/CardTypeIcons/Duck");

        public override Sprite ParamIcon1 => _unit.Class.Icon;

        public override Sprite ParamIcon2 => Resources.Load<Sprite>("Sprites/Empty");

        public override string Title => _unit.Name;

        public override string ParamValue1 => _unit.Race.Name;

        public override string ParamValue2 => _unit.Class.Name;

        #endregion
        public void Init(Player player, Unit unit)
        {
            base.Init(player);
            Player = player;
            _unit = unit;
            _pieceConteiner = player.PieceConteiner;
            IsSpellReleased = false;
        }

        public override void StartRelease()
        {
            base.StartRelease();
            //marker
        }

        public override void Update()
        {
            base.Update();
        }

        public override void EndRelease()
        {
            base.EndRelease();
            //del marker
        }

        public override void LeftMouseClick(RaycastHit hit)
        {
            base.LeftMouseClick(hit);
            if (hit.collider.GetComponent<Piece>() == null)
            {
                var piece = Instantiate(_piecePrefab, hit.point, _piecePrefab.transform.rotation, _pieceConteiner);
                piece.Init(_unit, Player);
            }
        }
    }
}
