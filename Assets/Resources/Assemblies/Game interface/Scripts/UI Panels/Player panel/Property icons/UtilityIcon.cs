using UnityEngine;

public class UtilityIcon : PropertyGroupIcon {
    [SerializeField] private ScriptableObject utilityGroupSO;
    [SerializeField] private GameColour propertyGroupPanelColour;
    [SerializeField] private UtilityIconColourSetter iconColourSetter;
    [SerializeField] private GameObject goldRing;
    private UtilityGroupInfo utilityGroupInfo;



    #region MonoBehaviour
    private void Start() {
        utilityGroupInfo = (UtilityGroupInfo)utilityGroupSO;
        iconColourSetter.setColour(UtilityType.ELECTRICITY, false, ZeroPropertiesAlpha);
        iconColourSetter.setColour(UtilityType.WATER, false, ZeroPropertiesAlpha);
        Color panelColour = propertyGroupPanelColour.Colour;
        panelColour.a = ZeroPropertiesAlpha;
        updatePanelColour(panelColour);
    }
    #endregion



    #region PropertyGroupIcon
    public override void updateVisual(PlayerInfo playerInfo) {
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
                utilityGroupInfo.playerOwnsUtility(playerInfo, UtilityType.ELECTRICITY),
                alpha
            );
            iconColourSetter.setColour(
                UtilityType.WATER,
                utilityGroupInfo.playerOwnsUtility(playerInfo, UtilityType.WATER),
                alpha
            );
        }
        void setGoldRing(int propertiesOwned) {
            goldRing.SetActive(propertiesOwned == 2);
        }

        int propertiesOwned = utilityGroupInfo.utilitiesOwnedByPlayer(playerInfo);

        setPanelColour(propertiesOwned);
        setIconColours(propertiesOwned);
        setGoldRing(propertiesOwned);
    }
    #endregion
}
