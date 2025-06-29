using UnityEngine;

public class UtilityIcon : PropertyGroupIcon {
    [SerializeField] private ScriptableObject utilityGroupSO;
    [SerializeField] private GameColour propertyGroupPanelColour;
    [SerializeField] private UtilityIconColourSetter iconColourSetter;
    [SerializeField] private GameObject goldRing;
    private UtilityGroupInfo utilityGroupInfo;
    private UtilityIconState utilityIconState;



    #region MonoBehaviour
    private void Start() {
        utilityGroupInfo = (UtilityGroupInfo)utilityGroupSO;
        iconColourSetter.setColour(UtilityType.ELECTRICITY, false, ZeroPropertiesAlpha);
        iconColourSetter.setColour(UtilityType.WATER, false, ZeroPropertiesAlpha);
        Color panelColour = propertyGroupPanelColour.Colour;
        panelColour.a = ZeroPropertiesAlpha;
        updatePanelColour(panelColour);
        utilityIconState = new();
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
        void setIconColours(int propertiesOwned) {
            float alpha = propertiesOwned == 0 ? ZeroPropertiesAlpha : NonZeroPropertiesAlpha;

            iconColourSetter.setColour(
                UtilityType.ELECTRICITY,
                utilityGroupInfo.playerOwnsUtility(PlayerInfo, UtilityType.ELECTRICITY),
                alpha
            );
            iconColourSetter.setColour(
                UtilityType.WATER,
                utilityGroupInfo.playerOwnsUtility(PlayerInfo, UtilityType.WATER),
                alpha
            );
        }
        void setGoldRing(int propertiesOwned) {
            goldRing.SetActive(propertiesOwned == 2);
        }

        int propertiesOwned = utilityGroupInfo.utilitiesOwnedByPlayer(PlayerInfo);

        setPanelColour(propertiesOwned);
        setIconColours(propertiesOwned);
        setGoldRing(propertiesOwned);
    }
    public override void setNewState() {
        utilityIconState = new UtilityIconState();
    }
    #endregion
}
