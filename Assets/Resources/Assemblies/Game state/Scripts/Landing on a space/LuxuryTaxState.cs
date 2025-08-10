using UnityEngine;

[CreateAssetMenu(menuName = "State/LuxuryTaxState")]
internal class LuxuryTaxState : State {
    private bool animationOver;


    #region
    public override void enterState() {
        ScreenOverlayEventHub.Instance.sub_RemoveScreenAnimation(animationOverCalled);
        animationOver = false;
        DataEventHub.Instance.call_PlayerIncurredDebt(
            GameState.game.TurnPlayer,
            GameState.game.BankCreditor,
            GameConstants.LUXURY_TAX
        );
        ScreenOverlayEventHub.Instance.call_LuxuryTaxAnimationBegins();
    }
    public override bool exitConditionMet() {
        return animationOver;
    }
    public override void exitState() {
        ScreenOverlayEventHub.Instance.unsub_RemoveScreenAnimation(animationOverCalled);
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    #endregion


    private void animationOverCalled() {
        animationOver = true;
    }
}
