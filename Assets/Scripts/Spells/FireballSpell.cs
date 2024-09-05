using Battleground;
using System.Runtime.InteropServices;
using UI.Marker;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/Fireball")]

    public class FireballSpell : Spell
    {
        [SerializeField] private Fireball _fireballPrefab;

        public Vector3 _targetPosition;
        public Vector3 _startPosition;
        public Fireball _fireball;

        public override void LeftMouseClick(RaycastHit hit)
        {
            var inst = Instantiate(this);
            inst.Init(Piece);
            inst._startPosition = Piece.transform.position;
            inst._targetPosition = hit.point;
            inst.StartIndex = Piece.Player.Timeline.GetIndex;
            if (Piece.AddActivity(inst))
            {
                Debug.Log(StartIndex);
                inst._fireball = Instantiate(_fireballPrefab);
                inst._fireball.Init(inst.Piece.Player, inst._startPosition, inst._targetPosition, inst.EndIndex);
                IsSpellFinished = true;
            }
            else
                Debug.Log("Ќедостаточно времени на выполнение заклинани€");
        }
        public override void Release(float time = 0)
        {
            //play anim
        }

        public override void RightMouseClick(RaycastHit hit)
        {
        }

        public override void RemoveFromTimeline()
        {
            Destroy(_fireball.gameObject); 
        }

        public override void Update()
        {
        }

        public override void Start()
        {
            var a = Instantiate(MarkerPrefab);
            MarkerPrefab.Init(Mask, _startPosition);
        }
    }
}