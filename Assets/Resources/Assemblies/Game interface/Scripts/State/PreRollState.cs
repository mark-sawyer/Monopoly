using UnityEngine;

[CreateAssetMenu(menuName = "State/PreRollState")]
public class PreRollState : State {
    [SerializeField] private GameEvent regularTurnBegin;
    [SerializeField] private GameEvent rollButtonClickedEvent;
    [SerializeField] private GameEvent managePropertiesClickedEvent;
    [SerializeField] private GameEvent diceAnimationOver;
    private bool rollAnimationOver;
    private bool managePropertiesClicked;



    #region GameState
    public override void enterState() {
        rollAnimationOver = false;
        managePropertiesClicked = false;
        rollButtonClickedEvent.Listeners += rollButtonListener;
        managePropertiesClickedEvent.Listeners += managePropertiesListener;
        regularTurnBegin.invoke();
    }
    public override bool exitConditionMet() {
        return rollAnimationOver
            || managePropertiesClicked;
    }
    public override void exitState() {
        rollButtonClickedEvent.Listeners -= rollButtonListener;
        managePropertiesClickedEvent.Listeners -= managePropertiesListener;
    }
    public override State getNextState() {
        if (rollAnimationOver) {
            if (GameState.game.DiceInfo.ThreeDoublesInARow) return allStates.getState<PoliceAnimationState>();
            else return allStates.getState<MoveTokenState>();
        }
        if (managePropertiesClicked) return allStates.getState<ManagePropertiesState>();
        throw new System.Exception();
    }
    #endregion



    #region private
    private void rollButtonListener() {
        int turnIndex = GameState.game.IndexOfTurnPlayer;
        TokenScaler turnTokenScaler = TokenVisualManager.Instance.getTokenScaler(turnIndex);
        TokenVisual turnTokenVisual = TokenVisualManager.Instance.getTokenVisual(turnIndex);
        turnTokenScaler.beginScaleChange(InterfaceConstants.SCALE_FOR_MOVING_TOKEN);
        turnTokenVisual.changeLayer(InterfaceConstants.MOVING_TOKEN_LAYER_NAME);
        WaitFrames.Instance.exe(
            InterfaceConstants.DIE_FRAMES_PER_IMAGE * InterfaceConstants.DIE_IMAGES_BEFORE_SETTLING,
            () => {
                rollAnimationOver = true;
                diceAnimationOver.invoke();
            }
        );
    }
    private void managePropertiesListener() {
        managePropertiesClicked = true;
    }
    #endregion
}
