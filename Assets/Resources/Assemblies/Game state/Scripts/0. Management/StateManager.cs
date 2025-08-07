using System.Collections;
using UnityEngine;

public class StateManager : MonoBehaviour {
    private State currentGameState;
    [SerializeField] private State firstState;
    [SerializeField] private AllStates allStates;



    #region MonoBehaviour
    private void Start() {
        State.initialiseAllStates(allStates);
        currentGameState = firstState;
    }
    private void Update() {
        if (currentGameState.exitConditionMet()) {
            currentGameState.exitState();
            currentGameState = currentGameState.getNextState();
            currentGameState.enterState();
        }
    }
    #endregion
}
