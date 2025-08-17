using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RailroadIcon : PropertyGroupIcon {
    [SerializeField] private ScriptableObject railroadGroupSO;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameColour propertyGroupPanelColour;
    [SerializeField] private GameColour propertyGroupIconColour;
    [SerializeField] private GameColour mortgageColour;
    [SerializeField] private GameObject goldRing;
    [SerializeField] private Image trainImage;
    [SerializeField] private RailroadHighlight[] railroadHighlights;
    private RailroadGroupInfo railroadGroupInfo;
    private OtherPropertyGroupIconState railroadIconState;
    private PropertyInfo[] propertyInfos;



    #region MonoBehaviour
    //private void Start() {
    //    railroadGroupInfo = (RailroadGroupInfo)railroadGroupSO;
    //    propertyInfos = new PropertyInfo[4] {
    //        railroadGroupInfo.getRailroadInfo(0),
    //        railroadGroupInfo.getRailroadInfo(1),
    //        railroadGroupInfo.getRailroadInfo(2),
    //        railroadGroupInfo.getRailroadInfo(3)
    //    };
    //
    //    Color iconColour = propertyGroupIconColour.Colour;
    //    iconColour.a = ZeroPropertiesAlpha;
    //    trainImage.color = iconColour;
    //
    //    Color panelColour = propertyGroupPanelColour.Colour;
    //    panelColour.a = ZeroPropertiesAlpha;
    //    updatePanelColour(panelColour);
    //
    //    railroadIconState = new OtherPropertyGroupIconState(propertyInfos);
    //    for (int i = 0; i < 4; i++) {
    //        railroadHighlights[i].setup(railroadGroupInfo.getRailroadInfo(i), PlayerInfo);
    //    }
    //}
    #endregion



    #region PropertyGroupIcon
    public override void setup(PlayerInfo playerInfo) {
        PlayerInfo = playerInfo;
        railroadGroupInfo = (RailroadGroupInfo)railroadGroupSO;
        propertyInfos = new PropertyInfo[4] {
            railroadGroupInfo.getRailroadInfo(0),
            railroadGroupInfo.getRailroadInfo(1),
            railroadGroupInfo.getRailroadInfo(2),
            railroadGroupInfo.getRailroadInfo(3)
        };

        Color iconColour = propertyGroupIconColour.Colour;
        iconColour.a = ZeroPropertiesAlpha;
        trainImage.color = iconColour;

        Color panelColour = propertyGroupPanelColour.Colour;
        panelColour.a = ZeroPropertiesAlpha;
        updatePanelColour(panelColour);

        railroadIconState = new OtherPropertyGroupIconState(propertyInfos);
        for (int i = 0; i < 4; i++) {
            railroadHighlights[i].setup(railroadGroupInfo.getRailroadInfo(i), PlayerInfo);
        }
        updateIcon();
    }
    public override IEnumerator fadeAway() {
        Color panelColour = transform.GetChild(0).GetChild(0).GetComponent<Image>().color;
        Color trainColour = trainImage.color;
        Func<float, float> getAlpha = LinearValue.getFunc(ZeroPropertiesAlpha, 0, FrameConstants.DYING_PLAYER);
        PanelRecolourer panelRecolourer = new PanelRecolourer(transform);
        for (int i = 1; i <= FrameConstants.DYING_PLAYER; i++) {
            float alpha = getAlpha(i);
            panelColour.a = alpha;
            trainColour.a = alpha;
            panelRecolourer.recolour(panelColour);
            trainImage.color = trainColour;
            yield return null;
        }
    }
    public override bool NeedsToUpdate {
        get {
            OtherPropertyGroupIconState newState = new OtherPropertyGroupIconState(propertyInfos, PlayerInfo);
            return railroadIconState.stateHasChanged(newState);
        }
    }
    public override bool IsOn => !railroadIconState.NoOwnership;
    protected override void updateIcon() {
        void setPanelColour(int propertiesOwned) {
            float alpha = propertiesOwned == 0 ? ZeroPropertiesAlpha : NonZeroPropertiesAlpha;
            Color panelColour = propertyGroupPanelColour.Colour;
            panelColour.a = alpha;
            updatePanelColour(panelColour);
        }
        void setIconColour(int propertiesOwned) {
            float alpha = propertiesOwned == 0 ? ZeroPropertiesAlpha : NonZeroPropertiesAlpha;
            Color iconColour = railroadGroupInfo.propertiesMortgagedByPlayer(PlayerInfo) == 4
                ? mortgageColour.Colour
                : propertyGroupIconColour.Colour;
            iconColour.a = alpha;
            trainImage.color = iconColour;
        }
        void setText(int propertiesOwned) {
            string text = propertiesOwned == 0 || propertiesOwned == 4 ? "" : propertiesOwned.ToString();
            Vector3 position = text == "1" ? new Vector3(-0.4f, 0f) : new Vector3();            
            countText.text = text;
            countText.transform.localPosition = position;
        }
        void setGoldRing(int propertiesOwned) {
            goldRing.SetActive(propertiesOwned == 4);
        }

        int propertiesOwned = railroadGroupInfo.propertiesOwnedByPlayer(PlayerInfo);

        setPanelColour(propertiesOwned);
        setIconColour(propertiesOwned);
        setText(propertiesOwned);
        foreach (RailroadHighlight rh in railroadHighlights) rh.setHighlight();
        setGoldRing(propertiesOwned);
    }
    protected override void updateOff() {
        void setPanelColour() {
            Color panelColour = propertyGroupPanelColour.Colour;
            panelColour.a = ZeroPropertiesAlpha;
            updatePanelColour(panelColour);
        }
        void setIconColour() {
            Color iconColour = propertyGroupIconColour.Colour;
            iconColour.a = ZeroPropertiesAlpha;
            trainImage.color = iconColour;
        }


        setPanelColour();
        setIconColour();
        countText.text = "";
        foreach (RailroadHighlight rh in railroadHighlights) rh.turnOffHighlight();
        goldRing.SetActive(false);
    }
    protected override void setNewState() {
        railroadIconState = new OtherPropertyGroupIconState(propertyInfos, PlayerInfo);
    }
    #endregion
}
