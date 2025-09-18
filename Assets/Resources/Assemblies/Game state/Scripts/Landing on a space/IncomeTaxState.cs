using UnityEngine;

[CreateAssetMenu(menuName = "State/IncomeTaxState")]
internal class IncomeTaxState : State {
    private bool questionAnswered;



    #region GameState
    public override void enterState() {
        questionAnswered = false;
        ScreenOverlayFunctionEventHub.Instance.sub_RemoveScreenOverlay(screenAnimationRemoved);

        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        ScreenOverlayStarterEventHub.Instance.call_IncomeTaxQuestion(playerInfo);
    }
    public override bool exitConditionMet() {
        return questionAnswered;
    }
    public override void exitState() {
        ScreenOverlayFunctionEventHub.Instance.unsub_RemoveScreenOverlay(screenAnimationRemoved);
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    #endregion



    private void screenAnimationRemoved() {
        WaitFrames.Instance.beforeAction(
            FrameConstants.SCREEN_COVER_TRANSITION,
            () => questionAnswered = true
        );
    }
}
