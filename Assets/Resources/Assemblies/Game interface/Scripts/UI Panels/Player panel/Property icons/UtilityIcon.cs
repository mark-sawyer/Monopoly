using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
        iconColourSetter.setup(ZeroPropertiesAlpha);
        iconColourSetter.setColourOff(UtilityType.ELECTRICITY);
        iconColourSetter.setColourOff(UtilityType.WATER);
        Color panelColour = propertyGroupPanelColour.Colour;
        panelColour.a = ZeroPropertiesAlpha;
        updatePanelColour(panelColour);
        utilityIconState = new OtherPropertyGroupIconState(propertyInfos);
    }
    #endregion



    #region PropertyGroupIcon
    public override bool NeedsToUpdate {
        get {
            OtherPropertyGroupIconState newState = new OtherPropertyGroupIconState(propertyInfos, PlayerInfo);
            return utilityIconState.stateHasChanged(newState);
        }
    }
    public override bool IsOn => !utilityIconState.NoOwnership;
    public override IEnumerator fadeAway() {
        Color panelColour = transform.GetChild(0).GetChild(0).GetComponent<Image>().color;
        Func<float, float> getAlpha = LinearValue.getFunc(ZeroPropertiesAlpha, 0, FrameConstants.DYING_PLAYER);
        PanelRecolourer panelRecolourer = new PanelRecolourer(transform);
        for (int i = 1; i <= FrameConstants.DYING_PLAYER; i++) {
            float alpha = getAlpha(i);
            panelColour.a = alpha;
            panelRecolourer.recolour(panelColour);
            iconColourSetter.setIconsAlpha(alpha);
            yield return null;
        }
    }
    protected override void updateIcon() {
        void setPanelColour(int propertiesOwned) {
            float alpha = propertiesOwned == 0 ? ZeroPropertiesAlpha : NonZeroPropertiesAlpha;
            Color panelColour = propertyGroupPanelColour.Colour;
            panelColour.a = alpha;
            updatePanelColour(panelColour);
        }
        void setIconColours() {
            iconColourSetter.setColour(PlayerInfo, utilityGroupInfo.getUtilityInfo(0));
            iconColourSetter.setColour(PlayerInfo, utilityGroupInfo.getUtilityInfo(1));
        }
        void setGoldRing(int propertiesOwned) {
            goldRing.SetActive(propertiesOwned == 2);
        }

        int propertiesOwned = utilityGroupInfo.propertiesOwnedByPlayer(PlayerInfo);

        setPanelColour(propertiesOwned);
        setIconColours();
        setGoldRing(propertiesOwned);
    }
    protected override void updateOff() {
        void setPanelColour() {
            Color panelColour = propertyGroupPanelColour.Colour;
            panelColour.a = ZeroPropertiesAlpha;
            updatePanelColour(panelColour);
        }


        setPanelColour();
        iconColourSetter.setColourOff(UtilityType.ELECTRICITY);
        iconColourSetter.setColourOff(UtilityType.WATER);
        goldRing.SetActive(false);
    }
    protected override void setNewState() {
        utilityIconState = new OtherPropertyGroupIconState(propertyInfos, PlayerInfo);
    }
    #endregion
}
