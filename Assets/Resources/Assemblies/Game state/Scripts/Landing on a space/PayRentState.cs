using UnityEngine;

[CreateAssetMenu(menuName = "State/PayRentState")]
internal class PayRentState : State {
    private bool animationOver;



    #region State
    public override void enterState() {
        animationOver = false;
        ScreenOverlayFunctionEventHub.Instance.sub_RemoveScreenOverlay(animationOverCalled);
        PropertySpaceInfo propertySpaceInfo = (PropertySpaceInfo)GameState.game.TurnPlayer.SpaceInfo;
        PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
        PlayerInfo owner = propertyInfo.Owner;
        int rent = propertyInfo.Rent;
        DataEventHub.Instance.call_PlayerIncurredDebt(GameState.game.TurnPlayer, owner, rent);
        SingleCreditorDebtInfo debtInfo = (SingleCreditorDebtInfo)GameState.game.TurnPlayer.DebtInfo;
        ScreenOverlayStarterEventHub.Instance.call_PayingRentAnimationBegins(debtInfo);
    }
    public override bool exitConditionMet() {
        return animationOver;
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    public override void exitState() {
        ScreenOverlayFunctionEventHub.Instance.unsub_RemoveScreenOverlay(animationOverCalled);
    }
    #endregion



    private void animationOverCalled() {
        WaitFrames.Instance.beforeAction(
            FrameConstants.SCREEN_COVER_TRANSITION + 20,
            () => animationOver = true
        );
    }
}
