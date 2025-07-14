using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyOrUnmortgageBuildingButton : MonoBehaviour {
    private enum ButtonMode {
        BUY,
        UNMORTGAGE
    }
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buyText;
    [SerializeField] private GameObject buyGameObject;
    [SerializeField] private GameObject unmortgageGameObject;
    private EstateInfo estateInfo;
    private ButtonMode buttonMode;



    #region public
    public void setup(EstateInfo estateInfo) {
        this.estateInfo = estateInfo;
    }
    public void buttonClicked() {
        void buyClicked(PlayerInfo selectedPlayer) {
            DataEventHub.Instance.call_EstateAddedBuilding(estateInfo);
            DataEventHub.Instance.call_MoneyAdjustment(selectedPlayer, -estateInfo.BuildingCost);
        }
        void unmortgageClicked(PlayerInfo selectedPlayer) {
            DataEventHub.Instance.call_PropertyUnmortgaged(estateInfo);
            DataEventHub.Instance.call_MoneyAdjustment(selectedPlayer, -estateInfo.UnmortgageCost);
        }

        PlayerInfo selectedPlayer = ManagePropertiesPanel.Instance.SelectedPlayer;
        if (buttonMode == ButtonMode.BUY) buyClicked(selectedPlayer);
        else unmortgageClicked(selectedPlayer);
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualRefresh(selectedPlayer);
    }
    public void adjustToAppropriateOption(PlayerInfo playerInfo) {
        int money = playerInfo.Money;
        if (estateInfo.IsMortgaged) {
            toggleMode(ButtonMode.UNMORTGAGE);
            int unmortgageCost = estateInfo.UnmortgageCost;
            bool canAfford = unmortgageCost <= money;
            button.interactable = canAfford;
        }
        else {
            toggleMode(ButtonMode.BUY);
            int buildingCost = estateInfo.BuildingCost;
            bool canAfford = buildingCost <= money;
            button.interactable = canAfford && estateInfo.CanAddBuilding;
        }
    }
    #endregion



    #region private
    private void toggleMode(ButtonMode mode) {
        buttonMode = mode;
        if (mode == ButtonMode.BUY) {
            buyGameObject.SetActive(true);
            unmortgageGameObject.SetActive(false);
            if (estateInfo.BuildingCount == 4 || estateInfo.HasHotel) buyText.text = "BUY HOTEL";
            else buyText.text = "BUY HOUSE";
        }
        else {
            buyGameObject.SetActive(false);
            unmortgageGameObject.SetActive(true);
        }
    }
    #endregion
}
