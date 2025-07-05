using UnityEngine;
using UnityEngine.UI;

public class BuyBuildingButton : MonoBehaviour {
    [SerializeField] private Button button;
    private EstateInfo estateInfo;



    public void setup(EstateInfo estateInfo) {
        this.estateInfo = estateInfo;
    }
    public void setInteractable(PlayerInfo playerInfo) {
        int buildingCost = estateInfo.BuildingCost;
        int money = playerInfo.Money;
        bool canAfford = buildingCost <= money;
        bool isInteractable = canAfford && estateInfo.CanAddBuilding;
        button.interactable = isInteractable;
    }
    public void buttonClicked() {
        PlayerInfo selectedPlayer = ManagePropertiesPanel.Instance.SelectedPlayer;
        DataEventHub.Instance.call_EstateAddedBuilding(estateInfo);
        DataEventHub.Instance.call_MoneyAdjustment(selectedPlayer, -estateInfo.BuildingCost);
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualRefresh(selectedPlayer);
    }
}
