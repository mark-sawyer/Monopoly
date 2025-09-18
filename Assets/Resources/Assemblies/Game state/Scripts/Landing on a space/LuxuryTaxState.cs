using UnityEngine;

[CreateAssetMenu(menuName = "State/LuxuryTaxState")]
internal class LuxuryTaxState : State {
    private bool animationOver;


    #region
    public override void enterState() {
        ScreenOverlayFunctionEventHub.Instance.sub_RemoveScreenOverlay(animationOverCalled);
        animationOver = false;
        DataEventHub.Instance.call_PlayerIncurredDebt(
            GameState.game.TurnPlayer,
            GameState.game.BankCreditor,
            GameConstants.LUXURY_TAX
        );
        ScreenOverlayStarterEventHub.Instance.call_LuxuryTaxAnimationBegins();
    }
    public override bool exitConditionMet() {
        return animationOver;
    }
    public override void exitState() {
        ScreenOverlayFunctionEventHub.Instance.unsub_RemoveScreenOverlay(animationOverCalled);
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    #endregion


    private void animationOverCalled() {
        WaitFrames.Instance.beforeAction(
            FrameConstants.SCREEN_COVER_TRANSITION,
            () => animationOver = true
        );
    }
}
