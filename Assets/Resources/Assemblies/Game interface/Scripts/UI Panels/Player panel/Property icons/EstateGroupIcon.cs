using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class EstateGroupIcon : PropertyGroupIcon {
    #region Internal references
    [SerializeField] private EstateColour estateColourEnum;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameObject goldRing;
    [SerializeField] private Image[] highlightImages;
    [SerializeField] private HotelIconToggle hotelIconToggle;
    #endregion
    #region External references
    [SerializeField] private GameColour houseColour;
    [SerializeField] private GameColour hotelColour;
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
    }
    #endregion



    #region PropertyGroupIcon
    public override bool iconNeedsToUpdate() {
        EstateGroupIconState newState = new EstateGroupIconState(estateGroupInfo, PlayerInfo);
        return estateGroupIconState.stateHasChanged(newState);
    }
    public override void updateVisual() {
        void transparentIfZeroProperties(int propertiesOwned) {
            EstateColour estateColourEnum = estateGroupInfo.EstateColour;
            EstateGroupDictionary estateGroupDictionary = EstateGroupDictionary.Instance;
            EstateGroupColours estateGroupColours = estateGroupDictionary.lookupColour(estateColourEnum);
            Color estateColour = estateGroupColours.EstateColour.Colour;
            estateColour.a = propertiesOwned == 0 ? ZeroPropertiesAlpha : NonZeroPropertiesAlpha;
            updatePanelColour(estateColour);
        }
        void setText(int propertiesOwned, bool hasMonopoly, bool hotelExists, int minBuildings, int maxBuildings) {
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
        void setHighlights(bool hasMonopoly, bool hotelExists, int minBuildings, int maxBuildings) {
            Color highlightColour(int i) {
                Color noColour = new Color(0f, 0f, 0f, 0f);
                EstateInfo estateInfo = estateGroupInfo.getEstateInfo(i);
                int buildings = estateInfo.BuildingCount;
                if (estateInfo.Owner != PlayerInfo) return noColour;
                if (!hasMonopoly) return estateGroupColours.HighlightColour.Colour;  // Owns the property
                if (maxBuildings == 0) return noColour;                              // Has a monopoly
                if (maxBuildings == minBuildings && hotelExists) return noColour;    // Has at least one building
                if (maxBuildings == minBuildings) return noColour;                   // Does not have all hotels
                if (hotelExists && buildings == 1) return hotelColour.Colour;        // Has an uneven number of buildings
                if (hotelExists) return noColour;                                    // Property does not have a hotel
                if (buildings == maxBuildings) return houseColour.Colour;            // No properties have hotels
                return noColour;                                                     // Can have another house added
            }

            for (int i = 0; i < estateGroupInfo.NumberOfEstatesInGroup; i++) {
                highlightImages[i].color = highlightColour(i);
            }
        }
        void setGoldRing(bool hasMonopoly) {
            goldRing.SetActive(hasMonopoly);
        }
        void setHotelIcon(bool hotelExists, int minBuildings, int maxBuildings) {
            hotelIconToggle.toggle(hotelExists && minBuildings == maxBuildings);
        }

        int propertiesOwned = estateGroupInfo.propertiesOwnedByPlayer(PlayerInfo);
        bool hasMonopoly = propertiesOwned == estateGroupInfo.NumberOfEstatesInGroup;
        bool hotelExists = estateGroupInfo.HotelExists;
        int minBuildings = estateGroupInfo.MinBuildingCount;
        int maxBuildings = estateGroupInfo.MaxBuildingCount;

        transparentIfZeroProperties(propertiesOwned);
        setText(propertiesOwned, hasMonopoly, hotelExists, minBuildings, maxBuildings);
        setHighlights(hasMonopoly, hotelExists, minBuildings, maxBuildings);
        setGoldRing(hasMonopoly);
        setHotelIcon(hotelExists, minBuildings, maxBuildings);
    }
    public override void setNewState() {
        estateGroupIconState = new EstateGroupIconState(estateGroupInfo, PlayerInfo);
    }
    #endregion
}
