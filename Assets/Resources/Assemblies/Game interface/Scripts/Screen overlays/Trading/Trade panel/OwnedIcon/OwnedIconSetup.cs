using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OwnedIconSetup {
    private TextMeshProUGUI textMesh;
    private Image backgroundColourImage;
    private OwnedIconIconManager ownedIconIconManager;
    private Transform squarePanelTransform;
    private NonEstateTradableColours netc;



    #region public
    public OwnedIconSetup(
        TextMeshProUGUI textMesh,
        Image backgroundColourImage,
        OwnedIconIconManager iconGetter,
        Transform squarePanelTransform
    ) {
        this.textMesh = textMesh;
        this.backgroundColourImage = backgroundColourImage;
        this.ownedIconIconManager = iconGetter;
        this.squarePanelTransform = squarePanelTransform;
        netc = NonEstateTradableColours.Instance;
    }
    public void setup(TradableInfo tradableInfo) {
        textMesh.text = tradableInfo.Abbreviation;

        if (tradableInfo is PropertyInfo propertyInfo) {
            if (propertyInfo is EstateInfo estateInfo) setupEstate(estateInfo);
            else if (propertyInfo is UtilityInfo utilityInfo) setupUtility(utilityInfo);
            else setupRailroad(propertyInfo.IsMortgaged);
        }
        else {
            setupCard((CardInfo)tradableInfo);
        }
    }
    #endregion



    #region private
    private void setupEstate(EstateInfo estateInfo) {
        EstateGroupDictionary estateGroupDictionary = EstateGroupDictionary.Instance;
        EstateColour estateColour = estateInfo.EstateColour;
        EstateGroupColours estateGroupColours = estateGroupDictionary.lookupColour(estateColour);
        Color backgroundColour = estateGroupColours.MainColour.Colour;
        Color textBorderColour = estateGroupColours.HighlightColour.Colour;

        ownedIconIconManager.setupHouse(textBorderColour, estateInfo.IsMortgaged);
        setColours(backgroundColour, textBorderColour);
    }
    private void setupRailroad(bool isMortgaged) {
        ownedIconIconManager.setupTrain(isMortgaged);
        setColours(
            netc.RailroadBackgroundColour,
            netc.RailroadTextBorderColour
        );
    }
    private void setupUtility(UtilityInfo utilityInfo) {
        UtilityType utilityType = utilityInfo.UtilityType;

        Color backgroundColour;
        Color textBorderColour;
        if (utilityType == UtilityType.WATER) {
            ownedIconIconManager.setupWater(utilityInfo.IsMortgaged);
            backgroundColour = netc.WaterBackgroundColour;
            textBorderColour = netc.WaterTextBorderColour;
        }
        else {
            ownedIconIconManager.setupElectricity(utilityInfo.IsMortgaged);
            backgroundColour = netc.ElectricityBackgroundColour;
            textBorderColour = netc.ElectricityTextBorderColour;
        }

        setColours(backgroundColour, textBorderColour);
    }
    private void setupCard(CardInfo cardInfo) {
        textMesh.fontSize = 19f;

        CardType cardType = cardInfo.CardType;

        Color backgroundColour;
        Color textBorderColour;
        if (cardType == CardType.CHANCE) {
            backgroundColour = netc.ChanceBackgroundColour;
            textBorderColour = netc.ChanceTextBorderColour;
        }
        else {
            backgroundColour = netc.CommunityChestBackgroundColour;
            textBorderColour = netc.CommunityChestTextBorderColour;
        }
        setColours(backgroundColour, textBorderColour);
        ownedIconIconManager.setupLock(textBorderColour);
    }


    private void setColours(Color backgroundColour, Color textBorderColour) {
        backgroundColourImage.color = backgroundColour;
        textMesh.color = textBorderColour;
        PanelRecolourer panelRecolourer = new PanelRecolourer(squarePanelTransform);
        panelRecolourer.recolour(textBorderColour);
    }
    #endregion
}
