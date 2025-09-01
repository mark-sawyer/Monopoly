using UnityEngine;

[CreateAssetMenu(menuName = "State/PayRentState")]
internal class PayRentState : State {
    private bool animationOver;



    #region State
    public override void enterState() {
        animationOver = false;
        ScreenOverlayFunctionEventHub.Instance.sub_RemoveScreenOverlay(animationOverCalled);

        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        PropertySpaceInfo propertySpaceInfo = (PropertySpaceInfo)turnPlayer.SpaceInfo;
        PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
        PlayerInfo owner = propertyInfo.Owner;
        int rent = propertyInfo.Rent;

        DataEventHub.Instance.call_PlayerIncurredDebt(turnPlayer, owner, rent);
        SingleCreditorDebtInfo debtInfo = (SingleCreditorDebtInfo)turnPlayer.DebtInfo;
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
