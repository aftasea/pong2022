using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine {

    private State currentState;
    public interface State {
        public StateId GetId();
        public void OnEnter();
        public void Execute(GameStateMachine stateMachine);
    }
    
    public enum StateId {
        WaitingToStart,
        Playing,
        GameOver,
        RetryMessage,
    }
    
    private Dictionary<StateId, State> states;
    
    public GameStateMachine(State[] states) {
        this.states = new Dictionary<StateId, State>();
        foreach (var state in states) {
            // TODO: handle exceptions 
            this.states.Add(state.GetId(), state);
        }
        this.currentState = states[0];
    }

    public void ChangeState(StateId stateId) {
        if (this.states.ContainsKey(stateId))
            this.currentState = this.states[stateId];
        else
            Debug.LogError($"State key not found: {stateId}");
    }

    public void Update() {
        this.currentState?.Execute(this);
    }
}
