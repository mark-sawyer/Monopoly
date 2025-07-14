using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellOrMortgageBuildingButton : MonoBehaviour {
    private enum ButtonMode {
        SELL,
        MORTGAGE
    }
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private GameObject sellGameObject;
    [SerializeField] private GameObject mortgageGameObject;
    [SerializeField] private Button button;
    private EstateInfo estateInfo;
    private ButtonMode buttonMode;



    #region public
    public void setup(EstateInfo estateInfo) {
        this.estateInfo = estateInfo;
    }
    public void buttonClicked() {
        void sellClicked(PlayerInfo selectedPlayer) {
            DataEventHub.Instance.call_EstateRemovedBuilding(estateInfo);
            DataEventHub.Instance.call_MoneyAdjustment(selectedPlayer, estateInfo.BuildingSellCost);
        }
        void mortgageClicked(PlayerInfo selectedPlayer) {
            DataEventHub.Instance.call_PropertyMortgaged(estateInfo);
            DataEventHub.Instance.call_MoneyAdjustment(selectedPlayer, estateInfo.MortgageValue);
        }



        PlayerInfo selectedPlayer = ManagePropertiesPanel.Instance.SelectedPlayer;
        if (buttonMode == ButtonMode.SELL) sellClicked(selectedPlayer);
        else mortgageClicked(selectedPlayer);
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualRefresh(selectedPlayer);
    }
    public void adjustToAppropriateOption() {
        if (estateInfo.BuildingCount > 0) {
            toggleMode(ButtonMode.SELL);
            button.interactable = estateInfo.CanRemoveBuilding;
        }
        else {
            toggleMode(ButtonMode.MORTGAGE);
            bool noBuildingsInGroup = !estateInfo.EstateGroupInfo.BuildingExists;
            bool propertyNotMortgaged = !estateInfo.IsMortgaged;
            button.interactable = noBuildingsInGroup && propertyNotMortgaged;
        }
    }
    #endregion



    #region private
    private void toggleMode(ButtonMode mode) {
        buttonMode = mode;
        if (mode == ButtonMode.SELL) {
            sellGameObject.SetActive(true);
            mortgageGameObject.SetActive(false);
            if (estateInfo.HasHotel) sellText.text = "SELL HOTEL";
            else sellText.text = "SELL HOUSE";
        }
        else {
            sellGameObject.SetActive(false);
            mortgageGameObject.SetActive(true);
        }
    }
    #endregion
}
