using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EstateSection : PropertySection {
    #region Internal references
    [SerializeField] private TextMeshProUGUI propertyTitleText;
    [SerializeField] private TextMeshProUGUI buyPriceText;
    [SerializeField] private TextMeshProUGUI sellPriceText;
    [SerializeField] private Image titleStripImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Transform squarePanelTransform;
    [SerializeField] private Transform[] buttonPanelTransforms;
    #endregion
    private EstateInfo estateInfo;
    private EstateGroupColours estateGroupColours;



    #region PropertySection
    public override void setup() {
        estateInfo = (EstateInfo)PropertyInfo;
        estateGroupColours = EstateGroupDictionary.Instance.lookupColour(estateInfo.EstateColour);
        propertyTitleText.text = estateInfo.Name.ToUpper();
        buyPriceText.text = "$" + estateInfo.BuildingCost.ToString();
        sellPriceText.text = "$" + (estateInfo.BuildingCost / 2).ToString();
        titleStripImage.color = EstateGroupDictionary.Instance.lookupColour(estateInfo.EstateColour).EstateColour.Colour;
        setRingBorderColour();
        setBackgroundImageColour();
        setButtonColours();
    }
    public override void refreshVisual(PlayerInfo playerInfo) {

    }
    #endregion



    #region private
    private void setRingBorderColour() {
        Color highlightColour = estateGroupColours.HighlightColour.Colour;
        PanelRecolourer panelRecolourer = new PanelRecolourer(squarePanelTransform);
        panelRecolourer.recolour(highlightColour);
    }
    private void setBackgroundImageColour() {
        Color backgroundColour = estateGroupColours.BackgroundColour.Colour;
        backgroundImage.color = backgroundColour;
    }
    private void setButtonColours() {
        Color buttonColour = estateGroupColours.HighlightColour.Colour;
        foreach (Transform t in buttonPanelTransforms) {
            PanelRecolourer panelRecolourer = new PanelRecolourer(t);
            panelRecolourer.recolour(buttonColour);
        }
    }
    #endregion
}
