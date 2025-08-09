
public class MPSellOrMortgageBuildingButton : SellOrMortgageBuildingButton {
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
        if (CurrentMode == ButtonMode.SELL) sellClicked(selectedPlayer);
        else mortgageClicked(selectedPlayer);
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualRefresh(selectedPlayer);
    }
}
