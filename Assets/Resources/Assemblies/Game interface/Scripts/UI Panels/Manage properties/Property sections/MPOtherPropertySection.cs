using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MPOtherPropertySection : MPPropertySection {
    [SerializeField] private string propertyTypeString;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Button button;
    [SerializeField] private GameObject mortgagedGameObject;
    private PropertyInfo propertyInfo;



    #region PropertySection
    public override void setup() {
        propertyInfo = PropertyInfo;
    }
    public override void refreshRegularVisual(PlayerInfo playerInfo) {
        bool buttonStatus;


        if (propertyInfo.IsMortgaged) {
            buttonText.text = "UNMORTGAGE " + propertyTypeString;
            moneyText.text = "$" + propertyInfo.UnmortgageCost.ToString();
            mortgagedGameObject.SetActive(true);
            PlayerInfo selectedPlayer = ManagePropertiesPanel.Instance.SelectedPlayer;
            int unmortgageCost = propertyInfo.UnmortgageCost;
            int money = selectedPlayer.Money;
            bool canAfford = money >= unmortgageCost;
            buttonStatus = canAfford;
        }
        else {
            buttonText.text = "MORTGAGE " + propertyTypeString;
            moneyText.text = "$" + propertyInfo.MortgageValue.ToString();
            mortgagedGameObject.SetActive(false);
            buttonStatus = true;
        }


        if (ManagePropertiesWipe.Instance.WipeInProgress && buttonStatus == true) {
            button.interactable = false;
            ManagePropertiesEventHub.Instance.sub_PanelUnpaused(correctStatusAfterWipe);
        }
        else {
            button.interactable = buttonStatus;
        }
    }
    public override void refreshBuildingPlacementVisual(PlayerInfo playerInfo) {
        if (propertyInfo.IsMortgaged) {
            buttonText.text = "UNMORTGAGE " + propertyTypeString;
            moneyText.text = "$" + propertyInfo.UnmortgageCost.ToString();
            mortgagedGameObject.SetActive(true);
            button.interactable = false;
        }
        else {
            buttonText.text = "MORTGAGE " + propertyTypeString;
            moneyText.text = "$" + propertyInfo.MortgageValue.ToString();
            mortgagedGameObject.SetActive(false);
            button.interactable = false;
        }
    }
    #endregion



    #region public
    public void buttonClicked() {
        SoundPlayer.Instance.play_MoneyChing();
        PlayerInfo selectedPlayer = ManagePropertiesPanel.Instance.SelectedPlayer;
        if (propertyInfo.IsMortgaged) {
            DataEventHub.Instance.call_PropertyUnmortgaged(propertyInfo);
            DataUIPipelineEventHub.Instance.call_MoneyAdjustment(selectedPlayer, -propertyInfo.UnmortgageCost);
        }
        else {
            DataEventHub.Instance.call_PropertyMortgaged(propertyInfo);
            DataUIPipelineEventHub.Instance.call_MoneyAdjustment(selectedPlayer, propertyInfo.MortgageValue);
        }

        bool regularRefresh = ManagePropertiesPanel.Instance.IsRegularRefreshMode;
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualRefresh(selectedPlayer, regularRefresh);
    }
    #endregion



    #region private
    private void correctStatusAfterWipe() {
        button.interactable = true;
        ManagePropertiesEventHub.Instance.unsub_PanelUnpaused(correctStatusAfterWipe);
    }
    #endregion
}
