using UnityEngine;

[CreateAssetMenu(menuName = "State/UnaffordablePropertyState")]
internal class UnaffordablePropertyState : State {
    private bool animationOver;



    #region State
    public override void enterState() {
        animationOver = false;
        ScreenOverlayFunctionEventHub.Instance.sub_RemoveScreenOverlayKeepCover(animationOverListening);

        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        PropertySpaceInfo propertySpaceInfo = (PropertySpaceInfo)turnPlayer.SpaceInfo;
        PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
        ScreenOverlayStarterEventHub.Instance.call_UnaffordableProperty(propertyInfo);
    }
    public override bool exitConditionMet() {
        return animationOver;
    }
    public override void exitState() {
        ScreenOverlayFunctionEventHub.Instance.unsub_RemoveScreenOverlayKeepCover(animationOverListening);
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
