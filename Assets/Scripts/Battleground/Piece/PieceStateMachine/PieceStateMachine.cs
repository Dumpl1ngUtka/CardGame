using AI;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class PieceStateMachine
    {
        [SerializeField] private PieceState[] _availableStates;
        private PieceState _currentState;
        public SituationAnalyzer SituationAnalyzer {get; private set;}
        public Piece Piece { get; private set; }
        public List<PieceState> TransitionStates;

        public PieceStateMachine(Piece piece)
        {
            Piece = piece;
            SituationAnalyzer = new SituationAnalyzer(Piece);
            TransitionStates = new List<PieceState>()
            {
                new WalkAlone(this),
                new RunAway(this),
                new FollowToEnemy(this),
            };
            ChangeState(TransitionStates[0]);
        }

        public void Update()
        {
            _currentState.Update();
            SituationAnalyzer.Update();
        }

        public void ChangeState(PieceState state)
        {
            //Debug.Log(state);
            state?.Exit();
            _currentState = state;
            state?.Enter();
        }
    }

}
