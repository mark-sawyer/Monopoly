
public class RDSellOrMortgageBuildingButton : SellOrMortgageBuildingButton {
    #region public
    public override void buttonClicked() {
        void sellClicked(PlayerInfo turnPlayer) {
            DataEventHub.Instance.call_EstateRemovedBuilding(EstateInfo);
            DataUIPipelineEventHub.Instance.call_MoneyRaisedForDebt(GameState.game.TurnPlayer, EstateInfo.BuildingSellCost);
        }
        void mortgageClicked(PlayerInfo turnPlayer) {
            DataEventHub.Instance.call_PropertyMortgaged(EstateInfo);
            DataUIPipelineEventHub.Instance.call_MoneyRaisedForDebt(GameState.game.TurnPlayer, EstateInfo.MortgageValue);
        }



        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        if (CurrentMode == ButtonMode.SELL) sellClicked(turnPlayer);
        else mortgageClicked(turnPlayer);
        ResolveDebtEventHub.Instance.call_ResolveDebtVisualRefresh();
    }
    public override void adjustToAppropriateOption() {
        base.adjustToAppropriateOption();

        if (GameState.game.TurnPlayer.DebtInfo == null) {
            Button.interactable = false;
        }
    }
    #endregion
}
