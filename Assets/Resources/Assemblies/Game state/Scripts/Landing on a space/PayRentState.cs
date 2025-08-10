using UnityEngine;

[CreateAssetMenu(menuName = "State/PayRentState")]
internal class PayRentState : State {
    private bool animationOver;



    #region State
    public override void enterState() {
        animationOver = false;
        ScreenOverlayEventHub.Instance.sub_RemoveScreenAnimation(animationOverCalled);
        PropertySpaceInfo propertySpaceInfo = (PropertySpaceInfo)GameState.game.TurnPlayer.SpaceInfo;
        PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
        PlayerInfo owner = propertyInfo.Owner;
        int rent = propertyInfo.Rent;
        DataEventHub.Instance.call_PlayerIncurredDebt(GameState.game.TurnPlayer, owner, rent);
        ScreenOverlayEventHub.Instance.call_PayingRentAnimationBegins(GameState.game.TurnPlayer.DebtInfo);
    }
    public override bool exitConditionMet() {
        return animationOver;
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    public override void exitState() {
        ScreenOverlayEventHub.Instance.unsub_RemoveScreenAnimation(animationOverCalled);
    }
    #endregion



    private void animationOverCalled() {
        WaitFrames.Instance.beforeAction(
            FrameConstants.SCREEN_COVER_TRANSITION + 20,
            () => animationOver = true
        );
    }
}
