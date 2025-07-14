using System.Reflection;
using UnityEngine;

public class UtilityIcon : PropertyGroupIcon {
    [SerializeField] private ScriptableObject utilityGroupSO;
    [SerializeField] private GameColour propertyGroupPanelColour;
    [SerializeField] private UtilityIconColourSetter iconColourSetter;
    [SerializeField] private GameObject goldRing;
    private UtilityGroupInfo utilityGroupInfo;
    private PropertyInfo[] propertyInfos;
    private OtherPropertyGroupIconState utilityIconState;



    #region MonoBehaviour
    private void Start() {
        utilityGroupInfo = (UtilityGroupInfo)utilityGroupSO;
        propertyInfos = new PropertyInfo[2] {
            utilityGroupInfo.getUtilityInfo(0),
            utilityGroupInfo.getUtilityInfo(1)
        };
        iconColourSetter.setup(ZeroPropertiesAlpha, PlayerInfo);
        iconColourSetter.setColour(UtilityType.ELECTRICITY);
        iconColourSetter.setColour(UtilityType.WATER);
        Color panelColour = propertyGroupPanelColour.Colour;
        panelColour.a = ZeroPropertiesAlpha;
        updatePanelColour(panelColour);
        utilityIconState = new OtherPropertyGroupIconState(propertyInfos);
    }
    #endregion



    #region PropertyGroupIcon
    public override bool iconNeedsToUpdate() {
        OtherPropertyGroupIconState newState = new OtherPropertyGroupIconState(propertyInfos, PlayerInfo);
        return utilityIconState.stateHasChanged(newState);
    }
    public override void updateVisual() {
        void setPanelColour(int propertiesOwned) {
            float alpha = propertiesOwned == 0 ? ZeroPropertiesAlpha : NonZeroPropertiesAlpha;
            Color panelColour = propertyGroupPanelColour.Colour;
            panelColour.a = alpha;
            updatePanelColour(panelColour);
        }
        void setIconColours(int propertiesOwned) {
            iconColourSetter.setColour(PlayerInfo, utilityGroupInfo.getUtilityInfo(0));
            iconColourSetter.setColour(PlayerInfo, utilityGroupInfo.getUtilityInfo(1));
        }
        void setGoldRing(int propertiesOwned) {
            goldRing.SetActive(propertiesOwned == 2);
        }

        int propertiesOwned = utilityGroupInfo.propertiesOwnedByPlayer(PlayerInfo);

        setPanelColour(propertiesOwned);
        setIconColours(propertiesOwned);
        setGoldRing(propertiesOwned);
    }
    public override void setNewState() {
        utilityIconState = new OtherPropertyGroupIconState(propertyInfos, PlayerInfo);
    }
    #endregion
}
