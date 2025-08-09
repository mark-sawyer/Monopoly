using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RDEstateSection : MonoBehaviour {
    #region Setup references
    [SerializeField] private ScriptableObject estateSO;
    [SerializeField] private TextMeshProUGUI propertyTitleText;
    [SerializeField] private TextMeshProUGUI sellPriceText;
    [SerializeField] private TextMeshProUGUI mortgagePriceText;
    [SerializeField] private Image titleStripImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Transform squarePanelTransform;
    [SerializeField] private Transform buttonColourPanelTransform;
    #endregion
    #region Updating visuals
    [SerializeField] private SellOrMortgageBuildingButton sellOrMortgageBuildingMono;
    [SerializeField] private BuildingIcons buildingIcons;
    #endregion



    public EstateInfo EstateInfo => (EstateInfo)estateSO;
    public void setup(EstateGroupColours estateGroupColours) {
        void setEstateReferences() {
            sellOrMortgageBuildingMono.setup(EstateInfo);
            buildingIcons.setup(EstateInfo);
        }
        void setTexts() {
            propertyTitleText.text = EstateInfo.Name.ToUpper();
            sellPriceText.text = "$" + EstateInfo.BuildingSellCost.ToString();
            mortgagePriceText.text = "$" + EstateInfo.MortgageValue.ToString();
        }
        void setTitleStripImageColour() {
            Color titleStripColour = estateGroupColours.MainColour.Colour;
            titleStripImage.color = titleStripColour;
        }
        void setBackgroundImageColour() {
            Color backgroundColour = estateGroupColours.BackgroundColour.Colour;
            backgroundImage.color = backgroundColour;
        }
        void setRingBorderColour() {
            Color highlightColour = estateGroupColours.HighlightColour.Colour;
            PanelRecolourer panelRecolourer = new PanelRecolourer(squarePanelTransform);
            panelRecolourer.recolour(highlightColour);
        }
        void setButtonColour() {
            Color buttonColour = estateGroupColours.HighlightColour.Colour;
            PanelRecolourer panelRecolourer = new PanelRecolourer(buttonColourPanelTransform);
            panelRecolourer.recolour(buttonColour);
        }



        setEstateReferences();
        setTexts();
        setTitleStripImageColour();
        setBackgroundImageColour();
        setRingBorderColour();
        setButtonColour();
        refreshVisual();
    }


    public void refreshVisual() {
        sellOrMortgageBuildingMono.adjustToAppropriateOption();
        buildingIcons.updateIcons();
    }
}
