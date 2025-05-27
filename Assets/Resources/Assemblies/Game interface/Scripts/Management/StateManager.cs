using UnityEngine;

public class StateManager : MonoBehaviour {
    private State currentGameState;
    [SerializeField] ReferencePasser rp;
    [SerializeField] State[] states;
    #region private properties
    private PreRollState PreRollState { get => (PreRollState)getStateType<PreRollState>(); }
    private RollAnimationState RollAnimationState { get => (RollAnimationState)getStateType<RollAnimationState>(); }
    private MoveTokenState MoveTokenState { get => (MoveTokenState)getStateType<MoveTokenState>(); }
    private BuyPropertyOptionState BuyPropertyOptionState { get => (BuyPropertyOptionState)getStateType<BuyPropertyOptionState>(); }
    private ResolveTurnState ResolveTurnState { get => (ResolveTurnState)getStateType<ResolveTurnState>(); }
    #endregion



    #region MonoBehaviour
    private void Start() {
        setupGameStates();
        currentGameState = PreRollState;
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



    #region private
    private void setupGameStates() {
        PreRollState.setup(rp.RollButton);
        RollAnimationState.setup(rp.TokenVisualManager);
        MoveTokenState.setup(rp.SpaceVisualManager, rp.TokenVisualManager);
    }
    private State getStateType<T>() {
        for (int i = 0; i < states.Length; i++) {
            if (states[i] is T) return states[i];
        }
        throw new System.Exception("State not found.");
    }
    #endregion
}
