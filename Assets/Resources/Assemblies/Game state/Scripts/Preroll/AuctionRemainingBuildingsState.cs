using UnityEngine;

[CreateAssetMenu(menuName = "State/AuctionRemainingBuildingsState")]
internal class AuctionRemainingBuildingsState : State {
    private bool auctionOver;
    private bool backButtonClicked;



    #region State
    public override void enterState() {
        auctionOver = false;
        backButtonClicked = false;
        AuctionEventHub.Instance.sub_AllAuctionsFinished(auctionOverListening);
        AuctionEventHub.Instance.sub_AuctionBuildingsBackButtonClicked(backButtonListening);

        BuildingType buildingType = ManagePropertiesPanel.Instance.BuildingTypeAuctioned;
        ScreenOverlayStarterEventHub.Instance.call_AuctionBuildingsBegins(buildingType);
    }
    public override bool exitConditionMet() {
        return auctionOver
            || backButtonClicked;
    }
    public override void exitState() {
        AuctionEventHub.Instance.unsub_AllAuctionsFinished(auctionOverListening);
        AuctionEventHub.Instance.unsub_AuctionBuildingsBackButtonClicked(backButtonListening);
    }
    public override State getNextState() {
        if (auctionOver) return allStates.getState<PlaceAuctionedBuildingsState>();
        if (backButtonClicked) return allStates.getState<ManagePropertiesState>();

        throw new System.Exception();
    }
    #endregion



    #region private
    private void auctionOverListening() {
        auctionOver = true;
    }
    private void backButtonListening() {
        ScreenOverlayFunctionEventHub.Instance.call_RemoveScreenOverlayKeepCover();
        AccompanyingVisualSpawner.Instance.removeObjectAndResetPosition();
        WaitFrames.Instance.beforeAction(
            FrameConstants.MANAGE_PROPERTIES_DROP,
            () => backButtonClicked = true
        );
    }
    #endregion
}
