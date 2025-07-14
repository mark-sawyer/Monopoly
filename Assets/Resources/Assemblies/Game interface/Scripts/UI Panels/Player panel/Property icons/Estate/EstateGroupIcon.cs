using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class EstateGroupIcon : PropertyGroupIcon {
    #region Internal references
    [SerializeField] private EstateColour estateColourEnum;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameObject goldRing;
    [SerializeField] private GameObject hotelIcon;
    [SerializeField] private GameObject mortgageM;
    [SerializeField] private EstateHighlight[] estateHighlights;
    #endregion
    #region External references
    [SerializeField] private GameColour houseColour;
    [SerializeField] private GameColour mortgageColour;
    #endregion
    #region Private attributes
    private EstateGroupIconState estateGroupIconState;
    private EstateGroupInfo estateGroupInfo;
    private EstateGroupColours estateGroupColours;
    #endregion



    #region MonoBehaviour
    private void Start() {
        estateGroupInfo = EstateGroupDictionary.Instance.lookupInfo(estateColourEnum);
        estateGroupColours = EstateGroupDictionary.Instance.lookupColour(estateColourEnum);
        Color estateColour = estateGroupColours.EstateColour.Colour;
        estateColour.a = ZeroPropertiesAlpha;
        updatePanelColour(estateColour);
        estateGroupIconState = new EstateGroupIconState(estateGroupInfo);
        for (int i = 0; i < estateHighlights.Length; i++) {
            estateHighlights[i].setup(estateGroupInfo.getEstateInfo(i), PlayerInfo);
        }
    }
    #endregion



    #region PropertyGroupIcon
    public override bool iconNeedsToUpdate() {
        EstateGroupIconState newState = new EstateGroupIconState(estateGroupInfo, PlayerInfo);
        return estateGroupIconState.stateHasChanged(newState);
    }
    public override void updateVisual() {
        int estatesInGroup = estateGroupInfo.NumberOfPropertiesInGroup;
        int propertiesOwned = estateGroupInfo.propertiesOwnedByPlayer(PlayerInfo);
        bool hasMonopoly = propertiesOwned == estatesInGroup;
        int mortgageCount = estateGroupInfo.MortgageCount;
        bool hotelExists = estateGroupInfo.HotelExists;
        int minBuildings = estateGroupInfo.MinBuildingCount;
        int maxBuildings = estateGroupInfo.MaxBuildingCount;



        void setText() {
            string text = "";
            Color colour = Color.white;
            Vector3 position = new Vector3();

            if (propertiesOwned == 0) { }
            else if (!hasMonopoly) text = propertiesOwned.ToString();  // Owns at least one property
            else if (minBuildings == 0) { }                            // Has a monopoly
            else if (maxBuildings == 1 && hotelExists) { }             // Has at least one building on each property
            else if (hotelExists) {
                text = "4";
                colour = houseColour.Colour;
            }                                   // Does not have hotels on each property
            else {
                text = minBuildings.ToString();
                colour = houseColour.Colour;
            }                                                    // Does not have any hotels

            if (text == "1") position = new Vector3(-0.4f, 0f);

            countText.text = text;
            countText.color = colour;
            countText.transform.localPosition = position;
        }
        void transparentIfZeroProperties() {
            EstateColour estateColourEnum = estateGroupInfo.EstateColour;
            EstateGroupDictionary estateGroupDictionary = EstateGroupDictionary.Instance;
            EstateGroupColours estateGroupColours = estateGroupDictionary.lookupColour(estateColourEnum);
            Color estateColour = estateGroupColours.EstateColour.Colour;
            estateColour.a = propertiesOwned == 0 ? ZeroPropertiesAlpha : NonZeroPropertiesAlpha;
            updatePanelColour(estateColour);
        }



        transparentIfZeroProperties();
        setText();
        foreach (EstateHighlight estateHighlight in estateHighlights) estateHighlight.setHighlight();
        goldRing.SetActive(hasMonopoly);
        hotelIcon.SetActive(hotelExists && minBuildings == maxBuildings);
        mortgageM.SetActive(mortgageCount == estatesInGroup);
    }
    public override void setNewState() {
        estateGroupIconState = new EstateGroupIconState(estateGroupInfo, PlayerInfo);
    }
    #endregion
}
