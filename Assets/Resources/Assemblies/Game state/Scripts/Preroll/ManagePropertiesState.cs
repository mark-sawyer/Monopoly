using UnityEngine;

[CreateAssetMenu(menuName = "State/ManagePropertiesState")]
internal class ManagePropertiesState : State {
    private bool managePropertiesPanelRaised;
    private bool auctionBuildingsButtonClicked;



    #region State
    public override void enterState() {
        managePropertiesPanelRaised = false;
        auctionBuildingsButtonClicked = false;
        ManagePropertiesEventHub.Instance.sub_BackButtonPressed(backButtonListening);
        AuctionEventHub.Instance.sub_AuctionRemainingBuildingsButtonClicked(auctionBuildingsButtonListening);
    }
    public override bool exitConditionMet() {
        return managePropertiesPanelRaised
            || auctionBuildingsButtonClicked;
    }
    public override void exitState() {
        ManagePropertiesEventHub.Instance.unsub_BackButtonPressed(backButtonListening);
        AuctionEventHub.Instance.unsub_AuctionRemainingBuildingsButtonClicked(auctionBuildingsButtonListening);
    }
    public override State getNextState() {
        if (managePropertiesPanelRaised) return allStates.getState<PostManagePropertiesClosedState>();
        if (auctionBuildingsButtonClicked) return allStates.getState<AuctionRemainingBuildingsState>();

        throw new System.Exception();
    }
    #endregion



    #region private
    private void backButtonListening() {
        SoundOnlyEventHub.Instance.call_Swoop();
        WaitFrames.Instance.beforeAction(
            FrameConstants.MANAGE_PROPERTIES_DROP,
            () => managePropertiesPanelRaised = true
        );
    }
    private void auctionBuildingsButtonListening() {
        SoundOnlyEventHub.Instance.call_Swoop();
        WaitFrames.Instance.beforeAction(
            FrameConstants.MANAGE_PROPERTIES_DROP,
            () => auctionBuildingsButtonClicked = true
        );
    }
    #endregion
}
