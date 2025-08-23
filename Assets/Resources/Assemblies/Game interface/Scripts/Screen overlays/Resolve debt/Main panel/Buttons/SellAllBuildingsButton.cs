using UnityEngine;
using UnityEngine.UI;

public class SellAllBuildingsButton : MonoBehaviour {
    [SerializeField] private Button button;
    private EstateGroupInfo estateGroupInfo;
    private PlayerInfo debtor;



    public void setup(EstateGroupInfo estateGroupInfo, PlayerInfo debtor) {
        this.estateGroupInfo = estateGroupInfo;
        this.debtor = debtor;
    }
    public void buttonClicked() {
        int totalBuildings = estateGroupInfo.TotalBuildings;
        int buildingSellPrice = estateGroupInfo.BuildingSellCost;
        int amountRaised = totalBuildings * buildingSellPrice;
        DataEventHub.Instance.call_EstateGroupRemovedAllBuildings(estateGroupInfo);
        DataUIPipelineEventHub.Instance.call_MoneyRaisedForDebt(debtor, amountRaised);
        ResolveDebtEventHub.Instance.call_ResolveDebtVisualRefresh();
    }
    public void adjustCorrectInteractability() {
        bool buildingsLeft = estateGroupInfo.TotalBuildings > 0;
        bool debtPaid = debtor.DebtInfo == null;
        button.interactable = buildingsLeft && !debtPaid;
    }
}
