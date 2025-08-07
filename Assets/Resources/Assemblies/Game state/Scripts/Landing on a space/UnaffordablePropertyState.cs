using UnityEngine;

[CreateAssetMenu(menuName = "State/UnaffordablePropertyState")]
internal class UnaffordablePropertyState : State {
    private ScreenAnimationEventHub screenAnimationEvents;
    private bool animationOver;



    #region State
    public override void enterState() {
        if (screenAnimationEvents == null) screenAnimationEvents = ScreenAnimationEventHub.Instance;
        animationOver = false;
        screenAnimationEvents.sub_RemoveScreenAnimationKeepCover(animationOverListening);

        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        PropertySpaceInfo propertySpaceInfo = (PropertySpaceInfo)turnPlayer.SpaceInfo;
        PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
        ScreenAnimationEventHub.Instance.call_UnaffordableProperty(propertyInfo);
    }
    public override bool exitConditionMet() {
        return animationOver;
    }
    public override void exitState() {
        screenAnimationEvents.unsub_RemoveScreenAnimationKeepCover(animationOverListening);
    }
    public override State getNextState() {
        return allStates.getState<AuctionPropertyState>();
    }
    #endregion



    #region private
    private void animationOverListening() {
        animationOver = true;
    }
    #endregion
}
