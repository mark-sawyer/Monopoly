using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EstateSection : PropertySection {
    #region Setup references
    [SerializeField] private TextMeshProUGUI propertyTitleText;
    [SerializeField] private TextMeshProUGUI buyPriceText;
    [SerializeField] private TextMeshProUGUI sellPriceText;
    [SerializeField] private TextMeshProUGUI mortgagePriceText;
    [SerializeField] private TextMeshProUGUI unmortgagePriceText;
    [SerializeField] private Image titleStripImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Transform squarePanelTransform;
    [SerializeField] private Transform[] buttonPanelTransforms;
    #endregion
    #region Updating visuals
    [SerializeField] private BuyOrUnmortgageBuildingButton buyOrUnmortgageBuildingMono;
    [SerializeField] private SellOrMortgageBuildingButton sellOrMortgageBuildingMono;
    [SerializeField] private BuildingIcons buildingIcons;
    #endregion
    private EstateInfo estateInfo;
    private EstateGroupColours estateGroupColours;



    #region PropertySection
    public override void setup() {
        void setEstateReferences() {
            estateInfo = (EstateInfo)PropertyInfo;
            buyOrUnmortgageBuildingMono.setup(estateInfo);
            sellOrMortgageBuildingMono.setup(estateInfo);
            buildingIcons.setup(estateInfo);
            estateGroupColours = EstateGroupDictionary.Instance.lookupColour(estateInfo.EstateColour);
        }
        void setTexts() {
            propertyTitleText.text = estateInfo.Name.ToUpper();
            buyPriceText.text = "$" + estateInfo.BuildingCost.ToString();
            sellPriceText.text = "$" + estateInfo.BuildingSellCost.ToString();
            mortgagePriceText.text = "$" + estateInfo.MortgageValue.ToString();
            unmortgagePriceText.text = "$" + estateInfo.UnmortgageCost.ToString();
        }
        void setRingBorderColour() {
            Color highlightColour = estateGroupColours.HighlightColour.Colour;
            PanelRecolourer panelRecolourer = new PanelRecolourer(squarePanelTransform);
            panelRecolourer.recolour(highlightColour);
        }
        void setTitleStripImageColour() {
            Color titleStripColour = estateGroupColours.MainColour.Colour;
            titleStripImage.color = titleStripColour;
        }
        void setBackgroundImageColour() {
            Color backgroundColour = estateGroupColours.BackgroundColour.Colour;
            backgroundImage.color = backgroundColour;
        }
        void setButtonColours() {
            Color buttonColour = estateGroupColours.HighlightColour.Colour;
            foreach (Transform t in buttonPanelTransforms) {
                PanelRecolourer panelRecolourer = new PanelRecolourer(t);
                panelRecolourer.recolour(buttonColour);
            }
        }



        setEstateReferences();
        setTexts();
        setTitleStripImageColour();
        setRingBorderColour();
        setBackgroundImageColour();
        setButtonColours();
    }
    public override void refreshVisual(PlayerInfo playerInfo) {
        buyOrUnmortgageBuildingMono.adjustToAppropriateOption(playerInfo);
        sellOrMortgageBuildingMono.adjustToAppropriateOption();
        buildingIcons.updateIcons();
    }
    #endregion
}
