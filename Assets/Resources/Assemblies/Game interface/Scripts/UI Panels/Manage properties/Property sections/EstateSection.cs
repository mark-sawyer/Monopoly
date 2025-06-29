using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EstateSection : PropertySection {
    #region Setup references
    [SerializeField] private TextMeshProUGUI propertyTitleText;
    [SerializeField] private TextMeshProUGUI buyPriceText;
    [SerializeField] private TextMeshProUGUI sellPriceText;
    [SerializeField] private Image titleStripImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Transform squarePanelTransform;
    [SerializeField] private Transform[] buttonPanelTransforms;
    [SerializeField] private BuyBuildingButton buyBuildingButton;
    #endregion
    #region Refresh references
    [SerializeField] private Button sellBuildingButton;
    [SerializeField] private BuildingIcons buildingIcons;
    #endregion
    private EstateInfo estateInfo;
    private EstateGroupColours estateGroupColours;



    #region PropertySection
    public override void setup() {
        void setEstateReferences() {
            estateInfo = (EstateInfo)PropertyInfo;
            buyBuildingButton.setup(estateInfo);
            buildingIcons.setup(estateInfo);
            estateGroupColours = EstateGroupDictionary.Instance.lookupColour(estateInfo.EstateColour);
        }
        void setTexts() {
            propertyTitleText.text = estateInfo.Name.ToUpper();
            buyPriceText.text = "$" + estateInfo.BuildingCost.ToString();
            sellPriceText.text = "$" + (estateInfo.BuildingCost / 2).ToString();
        }
        void setRingBorderColour() {
            Color highlightColour = estateGroupColours.HighlightColour.Colour;
            PanelRecolourer panelRecolourer = new PanelRecolourer(squarePanelTransform);
            panelRecolourer.recolour(highlightColour);
        }
        void setTitleStripImageColour() {
            Color titleStripColour = estateGroupColours.EstateColour.Colour;
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
        buyBuildingButton.setInteractable(playerInfo);
        sellBuildingButton.interactable = estateInfo.CanRemoveBuilding;
    }
    public void updateBuildingIcons() {
        buildingIcons.updateIcons();
    }
    #endregion
}
