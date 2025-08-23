using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyOrUnmortgageBuildingButton : MonoBehaviour {
    private enum ButtonMode {
        BUY,
        UNMORTGAGE,
        PLACE_BUILDING
    }
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buyText;
    [SerializeField] private TextMeshProUGUI placeBuildingText;
    [SerializeField] private GameObject buyGameObject;
    [SerializeField] private GameObject unmortgageGameObject;
    [SerializeField] private GameObject placeBuildingGameObject;
    private EstateInfo estateInfo;
    private ButtonMode buttonMode;



    #region public
    public void setup(EstateInfo estateInfo) {
        this.estateInfo = estateInfo;
    }
    public void buttonClicked() {
        void buyClicked(PlayerInfo selectedPlayer) {
            DataEventHub.Instance.call_EstateAddedBuilding(estateInfo);
            DataUIPipelineEventHub.Instance.call_MoneyAdjustment(selectedPlayer, -estateInfo.BuildingCost);
            SoundPlayer.Instance.play_MoneyChing();
        }
        void unmortgageClicked(PlayerInfo selectedPlayer) {
            DataEventHub.Instance.call_PropertyUnmortgaged(estateInfo);
            DataUIPipelineEventHub.Instance.call_MoneyAdjustment(selectedPlayer, -estateInfo.UnmortgageCost);
            SoundPlayer.Instance.play_MoneyChing();
        }
        void placeBuildingClicked(PlayerInfo selectedPlayer) {
            DataEventHub.Instance.call_EstateAddedBuilding(estateInfo);
            SoundPlayer.Instance.play_BrickLaying();
        }


        PlayerInfo selectedPlayer = ManagePropertiesPanel.Instance.SelectedPlayer;
        if (buttonMode == ButtonMode.BUY) buyClicked(selectedPlayer);
        else if (buttonMode == ButtonMode.UNMORTGAGE) unmortgageClicked(selectedPlayer);
        else placeBuildingClicked(selectedPlayer);

        bool regularRefresh = ManagePropertiesPanel.Instance.IsRegularRefreshMode;
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualRefresh(selectedPlayer, regularRefresh);
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
    public void adjustForBuildingPlacementMode(PlayerInfo playerInfo, BuildingType buildingType) {
        void handleHousePlacement() {
            if (estateInfo.CanAddBuilding && estateInfo.BuildingCount < 4) {
                toggleMode(ButtonMode.PLACE_BUILDING);
                button.interactable = true;
            }
            else {
                toggleMode(ButtonMode.BUY);
                button.interactable = false;
            }
        }
        void handleHotelPlacement() {
            if (estateInfo.CanAddBuilding && estateInfo.BuildingCount == 4) {
                toggleMode(ButtonMode.PLACE_BUILDING);
                button.interactable = true;
            }
            else {
                toggleMode(ButtonMode.BUY);
                button.interactable = false;
            }
        }


        if (estateInfo.IsMortgaged) {
            toggleMode(ButtonMode.UNMORTGAGE);
            button.interactable = false;
            return;
        }
        
        if (buildingType == BuildingType.HOUSE) handleHousePlacement();
        else handleHotelPlacement();
    }
    #endregion



    #region private
    private void toggleMode(ButtonMode mode) {
        buttonMode = mode;
        if (mode == ButtonMode.BUY) {
            buyGameObject.SetActive(true);
            unmortgageGameObject.SetActive(false);
            placeBuildingGameObject.SetActive(false);
            if (estateInfo.BuildingCount == 4 || estateInfo.HasHotel) buyText.text = "BUY HOTEL";
            else buyText.text = "BUY HOUSE";
        }
        else if (mode == ButtonMode.UNMORTGAGE) {
            buyGameObject.SetActive(false);
            unmortgageGameObject.SetActive(true);
            placeBuildingGameObject.SetActive(false);
        }
        else {
            buyGameObject.SetActive(false);
            unmortgageGameObject.SetActive(false);
            placeBuildingGameObject.SetActive(true);
            if (estateInfo.BuildingCount == 4) placeBuildingText.text = "PLACE HOTEL";
            else placeBuildingText.text = "PLACE HOUSE";
        }
    }
    #endregion
}
