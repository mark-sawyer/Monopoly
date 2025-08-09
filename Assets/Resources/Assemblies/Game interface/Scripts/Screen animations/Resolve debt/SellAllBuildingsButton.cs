using UnityEngine;
using UnityEngine.UI;

public class SellAllBuildingsButton : MonoBehaviour {
    [SerializeField] private Button button;
    private EstateGroupInfo estateGroupInfo;



    public void setup(EstateGroupInfo estateGroupInfo) {
        this.estateGroupInfo = estateGroupInfo;
    }
    public void buttonClicked() {
        int totalBuildings = estateGroupInfo.TotalBuildings;
        int buildingSellPrice = estateGroupInfo.BuildingSellCost;
        int amountRaised = totalBuildings * buildingSellPrice;
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        DataEventHub.Instance.call_EstateGroupRemovedAllBuildings(estateGroupInfo);
        DataUIPipelineEventHub.Instance.call_MoneyRaisedForDebt(turnPlayer, amountRaised);
        ResolveDebtEventHub.Instance.call_ResolveDebtVisualRefresh();
    }
    public void adjustCorrectInteractability() {
        bool buildingsLeft = estateGroupInfo.TotalBuildings > 0;
        bool debtPaid = GameState.game.TurnPlayer.DebtInfo == null;
        button.interactable = buildingsLeft && !debtPaid;
    }
}
