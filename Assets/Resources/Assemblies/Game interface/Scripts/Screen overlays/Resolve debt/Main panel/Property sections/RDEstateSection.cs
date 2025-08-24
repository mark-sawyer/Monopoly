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
    [SerializeField] private RDSellOrMortgageBuildingButton sellOrMortgageBuildingMono;
    [SerializeField] private BuildingIcons buildingIcons;
    #endregion



    public EstateInfo EstateInfo => (EstateInfo)estateSO;
    public void setup(PlayerInfo debtor) {
        void setEstateReferences() {
            sellOrMortgageBuildingMono.setup(debtor, EstateInfo);
            buildingIcons.setup(EstateInfo);
        }
        void setTexts() {
            propertyTitleText.text = EstateInfo.Name.ToUpper();
            sellPriceText.text = "$" + EstateInfo.BuildingSellCost.ToString();
            mortgagePriceText.text = "$" + EstateInfo.MortgageValue.ToString();
        }
        void setTitleStripImageColour(EstateGroupColours estateGroupColours) {
            Color titleStripColour = estateGroupColours.MainColour.Colour;
            titleStripImage.color = titleStripColour;
        }
        void setBackgroundImageColour(EstateGroupColours estateGroupColours) {
            Color backgroundColour = estateGroupColours.BackgroundColour.Colour;
            backgroundImage.color = backgroundColour;
        }
        void setRingBorderColour(EstateGroupColours estateGroupColours) {
            Color highlightColour = estateGroupColours.HighlightColour.Colour;
            PanelRecolourer panelRecolourer = new PanelRecolourer(squarePanelTransform);
            panelRecolourer.recolour(highlightColour);
        }
        void setButtonColour(EstateGroupColours estateGroupColours) {
            Color buttonColour = estateGroupColours.HighlightColour.Colour;
            PanelRecolourer panelRecolourer = new PanelRecolourer(buttonColourPanelTransform);
            panelRecolourer.recolour(buttonColour);
        }


        EstateGroupColours estateGroupColours = EstateGroupDictionary.Instance.lookupColour(EstateInfo.EstateColour);
        setEstateReferences();
        setTexts();
        setTitleStripImageColour(estateGroupColours);
        setBackgroundImageColour(estateGroupColours);
        setRingBorderColour(estateGroupColours);
        setButtonColour(estateGroupColours);
    }
    public void refreshVisual() {
        sellOrMortgageBuildingMono.adjustToAppropriateOption();
        buildingIcons.updateIcons();
    }
}
