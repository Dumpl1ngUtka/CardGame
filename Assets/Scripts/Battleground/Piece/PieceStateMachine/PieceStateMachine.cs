using AI;
using UnityEngine;

namespace Battleground
{
    public class PieceStateMachine
    {
        [SerializeField] private PieceState[] _availableStates;
        private PieceState _currentState;
        public SituationAnalyzer SituationAnalyzer {get; private set;}
        public Piece Piece { get; private set; }

        public PieceStateMachine(Piece piece)
        {
            Piece = piece;
            SituationAnalyzer = new SituationAnalyzer(Piece);
            _currentState = new WalkAlone(this);
        }

        public void Update()
        {
            _currentState.Update();
            SituationAnalyzer.UpdateMatrices();
        }

        public void ChangeState(PieceState state)
        {
            Debug.Log(state);
            _currentState = state;
        }
    }

}
