using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {
    private State currentGameState;
    [SerializeField] State firstState;
    [SerializeField] AllStates allStates;



    #region MonoBehaviour
    private void Start() {
        State.initialiseAllStates(allStates);
        currentGameState = firstState;
        currentGameState.enterState();
    }
    private void Update() {
        currentGameState.update();
        if (currentGameState.exitConditionMet()) {
            currentGameState.exitState();
            currentGameState = currentGameState.getNextState();
            currentGameState.enterState();
        }
    }
    #endregion
}
