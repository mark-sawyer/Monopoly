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
        bool buttonStatus;

        if (estateInfo.IsMortgaged) {
            toggleMode(ButtonMode.UNMORTGAGE);
            int unmortgageCost = estateInfo.UnmortgageCost;
            bool canAfford = unmortgageCost <= money;
            buttonStatus = canAfford;
        }
        else {
            toggleMode(ButtonMode.BUY);
            int buildingCost = estateInfo.BuildingCost;
            bool canAfford = buildingCost <= money;
            buttonStatus = canAfford && estateInfo.CanAddBuilding;
        }

        if (ManagePropertiesWipe.Instance.WipeInProgress && buttonStatus == true) {
            button.interactable = false;
            ManagePropertiesEventHub.Instance.sub_PanelUnpaused(correctStatusAfterWipe);
        }
        else {
            button.interactable = buttonStatus;
        }
    }
    public void adjustForBuildingPlacementMode(PlayerInfo playerInfo, BuildingType buildingType) {
        if (estateInfo.IsMortgaged) {
            toggleMode(ButtonMode.UNMORTGAGE);
            button.interactable = false;
            return;
        }
        
        bool buttonStatus;
        bool canPlaceRelevantBuilding = buildingType == BuildingType.HOUSE
            ? estateInfo.CanAddBuilding && estateInfo.BuildingCount < 4
            : estateInfo.CanAddBuilding && estateInfo.BuildingCount == 4;
        if (canPlaceRelevantBuilding) {
            toggleMode(ButtonMode.PLACE_BUILDING);
            buttonStatus = true;
        }
        else {
            toggleMode(ButtonMode.BUY);
            buttonStatus = false;
        }


        if (ManagePropertiesWipe.Instance.WipeInProgress && buttonStatus == true) {
            button.interactable = false;
            ManagePropertiesEventHub.Instance.sub_PanelUnpaused(correctStatusAfterWipe);
        }
        else {
            button.interactable = buttonStatus;
        }
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
    private void correctStatusAfterWipe() {
        button.interactable = true;
        ManagePropertiesEventHub.Instance.unsub_PanelUnpaused(correctStatusAfterWipe);
    }
    #endregion
}
