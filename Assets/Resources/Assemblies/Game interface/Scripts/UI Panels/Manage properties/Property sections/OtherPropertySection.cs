using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OtherPropertySection : PropertySection {
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
    public override void refreshVisual(PlayerInfo playerInfo) {
        if (propertyInfo.IsMortgaged) {
            buttonText.text = "UNMORTGAGE " + propertyTypeString;
            moneyText.text = "$" + propertyInfo.UnmortgageCost.ToString();
            mortgagedGameObject.SetActive(true);
            PlayerInfo selectedPlayer = ManagePropertiesPanel.Instance.SelectedPlayer;
            int unmortgageCost = propertyInfo.UnmortgageCost;
            int money = selectedPlayer.Money;
            bool canAfford = money >= unmortgageCost;
            button.interactable = canAfford;
        }
        else {
            buttonText.text = "MORTGAGE " + propertyTypeString;
            moneyText.text = "$" + propertyInfo.MortgageValue.ToString();
            mortgagedGameObject.SetActive(false);
            button.interactable = true;
        }
    }
    #endregion



    #region
    public void buttonClicked() {
        PlayerInfo selectedPlayer = ManagePropertiesPanel.Instance.SelectedPlayer;
        DataEventHub dataEvents = DataEventHub.Instance;
        if (propertyInfo.IsMortgaged) {
            dataEvents.call_PropertyUnmortgaged(propertyInfo);
            dataEvents.call_MoneyAdjustment(selectedPlayer, -propertyInfo.UnmortgageCost);
        }
        else {
            dataEvents.call_PropertyMortgaged(propertyInfo);
            dataEvents.call_MoneyAdjustment(selectedPlayer, propertyInfo.MortgageValue);
        }
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualRefresh(selectedPlayer);
    }
    #endregion
}
