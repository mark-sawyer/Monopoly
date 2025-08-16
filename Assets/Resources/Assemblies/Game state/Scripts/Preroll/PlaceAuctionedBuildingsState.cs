using UnityEngine;

[CreateAssetMenu(menuName = "State/PlaceAuctionedBuildingsState")]
internal class PlaceAuctionedBuildingsState : State {
    private bool remainingBuildingsPlaced;


    #region State
    public override void enterState() {
        remainingBuildingsPlaced = false;
        ManagePropertiesEventHub.Instance.sub_RemainingBuildingsPlaced(remainingBuildingsPlacedListener);
    }
    public override bool exitConditionMet() {
        return remainingBuildingsPlaced;
    }
    public override void exitState() {
        ManagePropertiesEventHub.Instance.unsub_RemainingBuildingsPlaced(remainingBuildingsPlacedListener);
    }
    public override State getNextState() {
        return allStates.getState<ManagePropertiesState>();
    }
    #endregion



    #region private
    private void remainingBuildingsPlacedListener() {
        WaitFrames.Instance.beforeAction(
            100,
            () => {
                SoundOnlyEventHub.Instance.call_OtherChime();
                ManagePropertiesPanel.Instance.resetAfterBuildingPlacement();
                PlayerInfo selectedPlayer = ManagePropertiesPanel.Instance.SelectedPlayer;
                ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualRefresh(selectedPlayer, true);
                remainingBuildingsPlaced = true;
            }
        );
    }
    #endregion
}
