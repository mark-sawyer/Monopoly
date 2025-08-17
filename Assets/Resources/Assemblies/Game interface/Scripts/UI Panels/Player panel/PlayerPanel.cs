using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour {
    [SerializeField] private PropertyGroupIcon[] propertyGroupIcons;
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private MoneyAdjuster moneyAdjuster;
    [SerializeField] private Image highlightImage;
    [SerializeField] private GOOJFIcon chanceGOOJFIcon;
    [SerializeField] private GOOJFIcon ccGOOJFIcon;
    private PlayerInfo playerInfo;
    private Dictionary<CardType, GOOJFIcon> GOOJFIconDict;
    private Dictionary<int, int> estateGroupToPropertyGroupIcon;
    private const int RAILROAD_INDEX = 4;
    private const int UTILITY_INDEX = 9;



    #region public
    public PlayerInfo PlayerInfo => playerInfo;
    public PropertyGroupIcon[] PropertyGroupIcons => propertyGroupIcons;
    public bool NeedsMoneyUpdate {
        get {
            int dataMoney = playerInfo.Money;
            int uiMoney = moneyAdjuster.DisplayedMoney;
            return dataMoney != uiMoney;
        }
    }
    public void setup(PlayerInfo playerInfo) {
        this.playerInfo = playerInfo;
        tokenIcon.setup(playerInfo.Token, playerInfo.Colour);
        moneyAdjuster.setStartingMoney(playerInfo.Money);
        GOOJFIconDict = new Dictionary<CardType, GOOJFIcon>() {
            { CardType.CHANCE, chanceGOOJFIcon },
            { CardType.COMMUNITY_CHEST, ccGOOJFIcon },
        };
        estateGroupToPropertyGroupIcon = new Dictionary<int, int>() {
            { 0, 0 },
            { 1, 1 },
            { 2, 2 },
            { 3, 3 },
            { 4, 5 },
            { 5, 6 },
            { 6, 7 },
            { 7, 8 },
        };
        foreach (PropertyGroupIcon propertyGroupIcon in propertyGroupIcons) {
            propertyGroupIcon.setup(playerInfo);
        }
    }
    public void adjustMoney(PlayerInfo player) {
        moneyAdjuster.adjustMoney(player);
    }
    public void adjustMoneyQuietly(PlayerInfo player) {
        moneyAdjuster.adjustMoneyQuietly(player);
    }
    public IEnumerator updatePropertyIconVisual(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        PropertyGroupIcon getPropertyGroupIcon() {
            if (propertyInfo is EstateInfo estateInfo) {
                int groupID = (int)estateInfo.EstateColour;
                int propertyGroupIndex = estateGroupToPropertyGroupIcon[groupID];
                return propertyGroupIcons[propertyGroupIndex];
            }
            else if (propertyInfo is RailroadInfo) return propertyGroupIcons[RAILROAD_INDEX];
            else return propertyGroupIcons[UTILITY_INDEX];
        }

        PropertyGroupIcon propertyGroupIcon = getPropertyGroupIcon();
        SoundOnlyEventHub.Instance.call_AppearingPop();
        yield return propertyGroupIcon.pulseAndUpdate();
    }
    public void toggleHighlightImage(bool toggle) {
        highlightImage.enabled = toggle;
    }
    public IEnumerator toggleGOOJFIcon(CardType cardType) {
        bool offButShouldBeOn = playerInfo.hasGOOJFCardOfType(cardType) && !GOOJFIconDict[cardType].IsOn;

        if (offButShouldBeOn) {
            SoundOnlyEventHub.Instance.call_AppearingPop();
            yield return GOOJFIconDict[cardType].enable(true);
        }
        else {
            SoundOnlyEventHub.Instance.call_AppearingPop();
            yield return GOOJFIconDict[cardType].enable(false);
        }
    }
    public bool needsGOOJFIconAdjusted(CardType cardType) {
        return playerInfo.hasGOOJFCardOfType(cardType)
            ^ GOOJFIconDict[cardType].IsOn;
    }
    public List<PropertyGroupIcon> propertyGroupIconsNeedingAnUpdate() {
        List<PropertyGroupIcon> needsUpdate = new();
        foreach (PropertyGroupIcon propertyGroupIcon in propertyGroupIcons) {
            if (propertyGroupIcon.NeedsToUpdate) {
                needsUpdate.Add(propertyGroupIcon);
            }
        }
        return needsUpdate;
    }
    #endregion
}
