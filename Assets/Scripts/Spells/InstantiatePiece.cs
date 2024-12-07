using Battleground;
using UI.Marker;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/InstantiatePiese")]
    public class InstantiatePiece : Spell
    {
        [SerializeField] private Piece _piecePrefab;
        private Transform _pieceConteiner;
        private Unit _unit;
        private DuckUICard _selfCard;

        #region CardUI
        public override Sprite Image => _unit.Race.Icon;

        public override Sprite TypeIcon => Resources.Load<Sprite>("Sprites/CardTypeIcons/Duck");

        public override Sprite ParamIcon1 => _unit.Class.Icon;

        public override Sprite ParamIcon2 => Resources.Load<Sprite>("Sprites/Empty");

        public override string Title => _unit.Name;

        public override string ParamValue1 => _unit.Race.Name;

        public override string ParamValue2 => _unit.Class.Name;

        #endregion


        public void Init(Player player, Unit unit, DuckUICard selfCard)
        {
            base.Init(player);
            Player = player;
            _unit = unit;
            _selfCard = selfCard;
            _pieceConteiner = player.PieceConteiner;
            IsSpellReleased = false;
        }

        public override void LeftMouseClick(RaycastHit hit)
        {
            base.LeftMouseClick(hit);
            if (hit.collider.GetComponent<Piece>() == null)
            {
                var piece = Instantiate(_piecePrefab, hit.point, _piecePrefab.transform.rotation, _pieceConteiner);
                piece.Init(_unit, Player);
                SetItems();
                IsSpellReleased = true;
            }
        }

        private void SetItems()
        {
            _unit.Inventory.SetItem(_selfCard.HeadArmor);
            _unit.Inventory.SetItem(_selfCard.BodyArmor);
            _unit.Inventory.SetItem(_selfCard.Weapon);
            _unit.Inventory.SetItem(_selfCard.Accessory1);
            _unit.Inventory.SetItem(_selfCard.Accessory2);
        }
    }
}
