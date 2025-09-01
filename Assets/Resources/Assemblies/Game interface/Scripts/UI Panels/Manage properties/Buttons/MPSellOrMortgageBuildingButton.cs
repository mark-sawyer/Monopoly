
public class MPSellOrMortgageBuildingButton : SellOrMortgageBuildingButton {
    #region SellOrMortgageBuildingButton
    public override void buttonClicked() {
        void sellClicked(PlayerInfo selectedPlayer) {
            DataEventHub.Instance.call_EstateRemovedBuilding(EstateInfo);
            DataUIPipelineEventHub.Instance.call_MoneyAdjustment(selectedPlayer, EstateInfo.BuildingSellCost);
        }
        void mortgageClicked(PlayerInfo selectedPlayer) {
            DataEventHub.Instance.call_PropertyMortgaged(EstateInfo);
            DataUIPipelineEventHub.Instance.call_MoneyAdjustment(selectedPlayer, EstateInfo.MortgageValue);
        }



        PlayerInfo selectedPlayer = ManagePropertiesPanel.Instance.SelectedPlayer;
        SoundPlayer.Instance.play_MoneyChing();
        if (CurrentMode == ButtonMode.SELL) sellClicked(selectedPlayer);
        else mortgageClicked(selectedPlayer);
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualRefresh(selectedPlayer, true);
    }
    #endregion



    #region public
    public override void adjustToAppropriateOption() {
        base.adjustToAppropriateOption();
        bool appropriateOption = Button.interactable;
        if (ManagePropertiesWipe.Instance.WipeInProgress && appropriateOption == true) {
            Button.interactable = false;
            ManagePropertiesEventHub.Instance.sub_PanelUnpaused(correctStatusAfterWipe);
        }
    }
    public void adjustForBuildingPlacementMode() {
        if (EstateInfo.BuildingCount > 0) {
            toggleMode(ButtonMode.SELL);
            Button.interactable = false;
        }
        else {
            toggleMode(ButtonMode.MORTGAGE);
            Button.interactable = false;
        }
    }
    #endregion



    #region private
    private void correctStatusAfterWipe() {
        Button.interactable = true;
        ManagePropertiesEventHub.Instance.unsub_PanelUnpaused(correctStatusAfterWipe);
    }
    #endregion
}
