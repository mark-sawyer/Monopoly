using UnityEngine;

public class StateManager : MonoBehaviour {
    private State currentGameState;
    private GamePlayer gamePlayer;
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
        currentGameState = PreRollState;
        setupGameStates();
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



    #region public
    public void setup(GamePlayer gamePlayer) {
        this.gamePlayer = gamePlayer;
    }
    #endregion



    #region private
    private void setupGameStates() {
        PreRollState.setup(rp.getRollButton());
        RollAnimationState.setup(rp.getDieVisual(), rp.getTokenVisualManager(), gamePlayer);
        MoveTokenState.setup(rp.getSpaceVisualManager(), rp.getTokenVisualManager(), gamePlayer);
        ResolveTurnState.setup(gamePlayer);
    }
    private State getStateType<T>() {
        for (int i = 0; i < states.Length; i++) {
            if (states[i] is T) return states[i];
        }
        throw new System.Exception("State not found.");
    }
    #endregion
}
