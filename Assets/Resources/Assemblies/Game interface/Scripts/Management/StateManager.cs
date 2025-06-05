using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {
    private State currentGameState;
    [SerializeField] State firstState;



    #region MonoBehaviour
    private void Start() {
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
