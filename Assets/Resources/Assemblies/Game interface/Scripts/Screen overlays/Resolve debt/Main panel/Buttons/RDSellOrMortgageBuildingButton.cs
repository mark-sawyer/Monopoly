
public class RDSellOrMortgageBuildingButton : SellOrMortgageBuildingButton {
    private PlayerInfo debtor;



    #region public
    public void setup(PlayerInfo debtor, EstateInfo estateInfo) {
        setup(estateInfo);
        this.debtor = debtor;
    }
    public override void buttonClicked() {
        void sellClicked() {
            DataEventHub.Instance.call_EstateRemovedBuilding(EstateInfo);
            DataUIPipelineEventHub.Instance.call_MoneyRaisedForDebt(debtor, EstateInfo.BuildingSellCost);
        }
        void mortgageClicked() {
            DataEventHub.Instance.call_PropertyMortgaged(EstateInfo);
            DataUIPipelineEventHub.Instance.call_MoneyRaisedForDebt(debtor, EstateInfo.MortgageValue);
        }



        if (CurrentMode == ButtonMode.SELL) sellClicked();
        else mortgageClicked();
        ResolveDebtEventHub.Instance.call_ResolveDebtVisualRefresh();
    }
    public override void adjustToAppropriateOption() {
        base.adjustToAppropriateOption();

        if (debtor.DebtInfo == null) {
            Button.interactable = false;
        }
    }
    #endregion
}
