using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine {
    
    public enum StateId {
        WaitingToStart,
        Playing,
        GameOver,
    }
    
    public interface IState {
        public StateId GetId();
        public void OnEnter();
        public void Execute(GameStateMachine stateMachine);
    }

    private Dictionary<StateId, IState> states;
    private IState currentState;
    
    public GameStateMachine(IState[] states) {
        this.states = new Dictionary<StateId, IState>();
        foreach (var state in states) {
            StateId key = state.GetId();
            if (this.states.ContainsKey(key))
                Debug.LogError($"State {key} was already added to the GameStateMachine");
            else
                this.states.Add(key, state);
        }

        this.currentState = states[0];
    }

    public void ChangeState(StateId stateId) {
        if (this.states.ContainsKey(stateId)) {
            this.currentState = this.states[stateId];
            this.currentState.OnEnter();
        }
        else {
            Debug.LogError($"State key not found: {stateId}");
        }
    }

    public void Update() {
        this.currentState?.Execute(this);
    }
}
