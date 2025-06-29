using UnityEngine;
using UnityEngine.UI;

public class BuyBuildingButton : MonoBehaviour {
    [SerializeField] private Button button;
    [SerializeField] private EstateEvent estateAddedBuilding;
    [SerializeField] private PlayerIntEvent moneyAdjustment;
    [SerializeField] private PlayerEvent managePropertiesVisualRefresh;
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
        estateAddedBuilding.invoke(estateInfo);
        moneyAdjustment.invoke(selectedPlayer, -estateInfo.BuildingCost);
        managePropertiesVisualRefresh.invoke(selectedPlayer);
    }
}
