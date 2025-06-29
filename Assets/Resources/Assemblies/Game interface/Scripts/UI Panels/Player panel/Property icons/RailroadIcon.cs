using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RailroadIcon : PropertyGroupIcon {
    [SerializeField] private ScriptableObject railroadGroupSO;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameColour propertyGroupPanelColour;
    [SerializeField] private GameColour propertyGroupIconColour;
    [SerializeField] private GameColour railroadHighlightColour;
    [SerializeField] private GameObject goldRing;
    [SerializeField] private Image[] highlightImages;
    [SerializeField] private Image trainImage;
    private RailroadGroupInfo railroadGroupInfo;
    private RailroadIconState railroadIconState;



    #region MonoBehaviour
    private void Start() {
        railroadGroupInfo = (RailroadGroupInfo)railroadGroupSO;

        Color iconColour = propertyGroupIconColour.Colour;
        iconColour.a = ZeroPropertiesAlpha;
        trainImage.color = iconColour;

        Color panelColour = propertyGroupPanelColour.Colour;
        panelColour.a = ZeroPropertiesAlpha;
        updatePanelColour(panelColour);

        railroadIconState = new();
    }
    #endregion



    #region PropertyGroupIcon
    public override bool iconNeedsToUpdate() {
        return false;
    }
    public override void updateVisual() {
        void setPanelColour(int propertiesOwned) {
            float alpha = propertiesOwned == 0 ? ZeroPropertiesAlpha : NonZeroPropertiesAlpha;
            Color panelColour = propertyGroupPanelColour.Colour;
            panelColour.a = alpha;
            updatePanelColour(panelColour);
        }
        void setIconColour(int propertiesOwned) {
            float alpha = propertiesOwned == 0 ? ZeroPropertiesAlpha : NonZeroPropertiesAlpha;
            Color iconColour = propertyGroupIconColour.Colour;
            iconColour.a = alpha;
            trainImage.color = iconColour;
        }
        void setText(int propertiesOwned) {
            string text = propertiesOwned == 0 || propertiesOwned == 4 ? "" : propertiesOwned.ToString();
            Vector3 position = text == "1" ? new Vector3(-0.4f, 0f) : new Vector3();            
            countText.text = text;
            countText.transform.localPosition = position;
        }
        void setHighlights(int propertiesOwned) {
            Color noColour = new Color(0f, 0f, 0f, 0f);
            for (int i = 0; i < 4; i++) {
                if (propertiesOwned == 0 || propertiesOwned == 4) {
                    highlightImages[i].color = noColour;
                }
                else {
                    bool playerOwnsRailroad = railroadGroupInfo.playerOwnsRailroad(PlayerInfo, i);
                    highlightImages[i].color = playerOwnsRailroad ? railroadHighlightColour.Colour : noColour;
                }
            }
        }
        void setGoldRing(int propertiesOwned) {
            goldRing.SetActive(propertiesOwned == 4);
        }

        int propertiesOwned = railroadGroupInfo.railroadsOwnedByPlayer(PlayerInfo);

        setPanelColour(propertiesOwned);
        setIconColour(propertiesOwned);
        setText(propertiesOwned);
        setHighlights(propertiesOwned);
        setGoldRing(propertiesOwned);
    }
    public override void setNewState() {
        railroadIconState = new RailroadIconState();
    }
    #endregion
}
