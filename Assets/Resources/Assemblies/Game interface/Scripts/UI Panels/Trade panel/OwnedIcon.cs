using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OwnedIcon : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Image backgroundColourImage;
    [SerializeField] private GameObject[] iconGameObjects;
    [SerializeField] private Transform squarePanelTransform;
    #region GameColours
    [SerializeField] private GameColour railroadBackgroundColour;
    [SerializeField] private GameColour railroadTextBorderColour;
    [SerializeField] private GameColour electricityBackgroundColour;
    [SerializeField] private GameColour electricityTextBorderColour;
    [SerializeField] private GameColour waterBackgroundColour;
    [SerializeField] private GameColour waterTextBorderColour;
    [SerializeField] private GameColour chanceBackgroundColour;
    [SerializeField] private GameColour chanceTextBorderColour;
    [SerializeField] private GameColour communityChestBackgroundColour;
    [SerializeField] private GameColour communityChestTextBorderColour;
    #endregion
    #region Icon index consts
    private const int HOUSE_ICON_INDEX = 0;
    private const int TRAIN_ICON_INDEX = 1;
    private const int ELECTRICITY_ICON_INDEX = 2;
    private const int WATER_ICON_INDEX = 3;
    private const int LOCK_ICON_INDEX = 4;
    #endregion



    #region public
    public void setupOwnedIcon(TradableInfo tradableInfo) {
        textMesh.text = tradableInfo.Abbreviation;

        if (tradableInfo is PropertyInfo propertyInfo) {
            if (propertyInfo is EstateInfo estateInfo) setupEstate(estateInfo);
            else if (propertyInfo is UtilityInfo utilityInfo) setupUtility(utilityInfo);
            else setupRailroad();
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

        GameObject houseIcon = iconGameObjects[HOUSE_ICON_INDEX];
        houseIcon.SetActive(true);
        setColours(backgroundColour, textBorderColour);
        setIconColour(houseIcon, textBorderColour);
    }
    private void setupRailroad() {
        GameObject trainIcon = iconGameObjects[TRAIN_ICON_INDEX];
        trainIcon.SetActive(true);
        setColours(
            railroadBackgroundColour.Colour,
            railroadTextBorderColour.Colour
        );
    }
    private void setupUtility(UtilityInfo utilityInfo) {
        UtilityType utilityType = utilityInfo.UtilityType;

        GameObject utilityIcon;
        Color backgroundColour;
        Color textBorderColour;
        if (utilityType == UtilityType.WATER) {
            utilityIcon = iconGameObjects[WATER_ICON_INDEX];
            backgroundColour = waterBackgroundColour.Colour;
            textBorderColour = waterTextBorderColour.Colour;
        }
        else {
            utilityIcon = iconGameObjects[ELECTRICITY_ICON_INDEX];
            backgroundColour = electricityBackgroundColour.Colour;
            textBorderColour = electricityTextBorderColour.Colour;
        }

        utilityIcon.SetActive(true);
        setColours(backgroundColour, textBorderColour);
    }
    private void setupCard(CardInfo cardInfo) {
        textMesh.fontSize = 19f;

        CardType cardType = cardInfo.CardType;

        Color backgroundColour;
        Color textBorderColour;
        if (cardType == CardType.CHANCE) {
            backgroundColour = chanceBackgroundColour.Colour;
            textBorderColour = chanceTextBorderColour.Colour;
        }
        else {
            backgroundColour = communityChestBackgroundColour.Colour;
            textBorderColour = communityChestTextBorderColour.Colour;
        }
        setColours(backgroundColour, textBorderColour);
        GameObject lockIcon = iconGameObjects[LOCK_ICON_INDEX];
        lockIcon.SetActive(true);
        setIconColour(lockIcon, textBorderColour);
    }


    private void setColours(Color backgroundColour, Color textBorderColour) {
        backgroundColourImage.color = backgroundColour;
        textMesh.color = textBorderColour;
        PanelRecolourer panelRecolourer = new PanelRecolourer(squarePanelTransform);
        panelRecolourer.recolour(textBorderColour);
    }
    private void setIconColour(GameObject iconGameObject, Color colour) {
        Image image = iconGameObject.GetComponent<Image>();
        image.color = colour;
    }
    #endregion
}
