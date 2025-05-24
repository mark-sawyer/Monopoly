using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class EstateGroupIcon : PropertyGroupIcon {
    [SerializeField] private ScriptableObject estateGroupSO;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GoldRingToggle goldRingToggle;
    [SerializeField] private Image[] highlightImages;
    [SerializeField] private GameColour houseColour;
    [SerializeField] private GameColour hotelColour;
    private EstateGroupInfo estateGroupInfo;
    private const float ZERO_PROPERTIES_ALPHA = 0.125f;
    private const float NON_ZERO_PROPERTIES_ALPHA = 1f;



    #region MonoBehaviour
    private void Start() {
        estateGroupInfo = (EstateGroupInfo)estateGroupSO;
        Color estateColour = estateGroupInfo.EstateColour;
        estateColour.a = ZERO_PROPERTIES_ALPHA;
        updatePanelColour(estateColour);
    }
    #endregion



    #region PropertyGroupIcon
    public override void updateVisual(PlayerInfo playerInfo) {
        void transparentIfZeroProperties(int propertiesOwned) {
            Color estateColour = estateGroupInfo.EstateColour;
            estateColour.a = propertiesOwned == 0 ? ZERO_PROPERTIES_ALPHA : NON_ZERO_PROPERTIES_ALPHA;
            updatePanelColour(estateColour);
        }
        void setText(int propertiesOwned, bool hasMonopoly, bool hotelExists, int minBuildings, int maxBuildings) {
            string text = "";
            Color colour = Color.white;
            Vector3 position = new Vector3();

            if (propertiesOwned == 0) { }
            else if (!hasMonopoly) text = propertiesOwned.ToString();  // Player owns at least one property
            else if (minBuildings == 0) { }                            // Player has a monopoly
            else if (maxBuildings == 1 && hotelExists) { }             // Player has at least one building on each property
            else if (hotelExists) {
                text = "4";
                colour = houseColour.Colour;
            }                                   // Player does not have hotels on each property
            else {
                text = minBuildings.ToString();
                colour = houseColour.Colour;
            }                                                    // Player does not have any hotels

            if (text == "1") position = new Vector3(-0.4f, 0f);

            countText.text = text;
            countText.color = colour;
            countText.transform.localPosition = position;
        }
        void setHighlights(bool hasMonopoly) {
            if (hasMonopoly) {
                for (int i = 0; i < estateGroupInfo.NumberOfEstatesInGroup; i++) {
                    highlightImages[i].color = new Color(0f, 0f, 0f, 0f);
                }
            }
            else {
                for (int i = 0; i < estateGroupInfo.NumberOfEstatesInGroup; i++) {
                    EstateInfo estateInfo = estateGroupInfo.getEstateInfo(i);
                    PlayerInfo owner = estateInfo.Owner;
                    if (owner == playerInfo) {
                        Color highlightColour = estateGroupInfo.HighlightColour;
                        highlightColour.a = 1;
                        highlightImages[i].color = highlightColour;
                    }
                    else {
                        highlightImages[i].color = new Color(0f, 0f, 0f, 0f);
                    }
                }
            }

        }
        void setGoldRing(bool hasMonopoly) {
            goldRingToggle.toggle(hasMonopoly);
        }

        int propertiesOwned = estateGroupInfo.propertiesOwnedByPlayer(playerInfo);
        bool hasMonopoly = propertiesOwned == estateGroupInfo.NumberOfEstatesInGroup;
        bool hotelExists = estateGroupInfo.HotelExists;
        int minBuildings = estateGroupInfo.MinBuildingCount;
        int maxBuildings = estateGroupInfo.MaxBuildingCount;

        transparentIfZeroProperties(propertiesOwned);
        setText(propertiesOwned, hasMonopoly, hotelExists, minBuildings, maxBuildings);
        setHighlights(hasMonopoly);
        setGoldRing(hasMonopoly);
    }
    #endregion
}
