using UnityEngine;

[CreateAssetMenu(menuName = "State/PreRollState")]
public class PreRollState : State {
    private bool rollAnimationOver;
    private bool managePropertiesClicked;
    private bool tradeClicked;



    #region GameState
    public override void enterState() {
        rollAnimationOver = false;
        managePropertiesClicked = false;
        tradeClicked = true;

        UIEventHub.Instance.sub_RollButtonClicked(rollButtonListener);
        ManagePropertiesEventHub.Instance.sub_ManagePropertiesOpened(managePropertiesListener);
        ScreenAnimationEventHub.Instance.sub_TradeOpened(tradeListener);

        DataEventHub.Instance.call_TurnBegin(false);
    }
    public override bool exitConditionMet() {
        return rollAnimationOver
            || managePropertiesClicked;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_RollButtonClicked(rollButtonListener);
        ManagePropertiesEventHub.Instance.unsub_ManagePropertiesOpened(managePropertiesListener);
        ScreenAnimationEventHub.Instance.unsub_TradeOpened(tradeListener);
    }
    public override State getNextState() {
        if (rollAnimationOver) {
            if (GameState.game.DiceInfo.ThreeDoublesInARow) return allStates.getState<MoveTokenToJailState>();
            else return allStates.getState<MoveTokenState>();
        }
        if (managePropertiesClicked) return allStates.getState<ManagePropertiesState>();
        if (tradeClicked) return allStates.getState<TradeState>();
        throw new System.Exception();
    }
    #endregion



    #region private
    private void rollButtonListener() {
        int turnIndex = GameState.game.IndexOfTurnPlayer;
        TokenVisual turnTokenVisual = TokenVisualManager.Instance.getTokenVisual(turnIndex);
        turnTokenVisual.prepForMoving();
        WaitFrames.Instance.exe(
            InterfaceConstants.DIE_FRAMES_PER_IMAGE * InterfaceConstants.DIE_IMAGES_BEFORE_SETTLING,
            () => {
                rollAnimationOver = true;
                UIEventHub.Instance.call_DoublesTickBoxUpdate();
            }
        );
    }
    private void managePropertiesListener() {
        managePropertiesClicked = true;
    }
    private void tradeListener() {
        tradeClicked = true;
    }
    #endregion
}
