using UnityEngine;

[CreateAssetMenu(menuName = "State/PreRollState")]
public class PreRollState : State {
    private bool rollAnimationOver;
    private bool managePropertiesClicked;



    #region GameState
    public override void enterState() {
        rollAnimationOver = false;
        managePropertiesClicked = false;

        UIEventHub.Instance.sub_RollButtonClicked(rollButtonListener);
        ManagePropertiesEventHub.Instance.sub_ManagePropertiesOpened(managePropertiesListener);

        DataEventHub.Instance.call_TurnBegin(false);
    }
    public override bool exitConditionMet() {
        return rollAnimationOver
            || managePropertiesClicked;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_RollButtonClicked(rollButtonListener);
        ManagePropertiesEventHub.Instance.unsub_ManagePropertiesOpened(managePropertiesListener);
    }
    public override State getNextState() {
        if (rollAnimationOver) {
            if (GameState.game.DiceInfo.ThreeDoublesInARow) return allStates.getState<MoveTokenToJailState>();
            else return allStates.getState<MoveTokenState>();
        }
        if (managePropertiesClicked) return allStates.getState<ManagePropertiesState>();
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
    #endregion
}
