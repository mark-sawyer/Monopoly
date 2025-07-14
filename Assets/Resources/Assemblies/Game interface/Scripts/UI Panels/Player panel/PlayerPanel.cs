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



    #region public
    public void setup(PlayerInfo playerInfo) {
        this.playerInfo = playerInfo;
        tokenIcon.setup(playerInfo.Token, playerInfo.Colour);
        moneyAdjuster.setStartingMoney(playerInfo.Money);
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
    public void updatePropertyIconVisual(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        PropertyGroupIcon getPropertyGroupIcon() {
            if (propertyInfo is EstateInfo estateInfo) {
                int groupID = (int)estateInfo.EstateColour;
                return propertyGroupIcons[groupID];
            }
            else if (propertyInfo is RailroadInfo) return propertyGroupIcons[8];
            else return propertyGroupIcons[9];
        }

        PropertyGroupIcon propertyGroupIcon = getPropertyGroupIcon();
        StartCoroutine(propertyGroupIcon.pulseAndUpdate());
        propertyGroupIcon.setNewState();
    }
    public void toggleHighlightImage(bool toggle) {
        highlightImage.enabled = toggle;
    }
    public void toggleGOOJFIcon(CardType cardType, bool toggle) {
        if (cardType == CardType.CHANCE) chanceGOOJFIcon.enable(toggle);
        else ccGOOJFIcon.enable(toggle);
    }
    public List<PropertyGroupIcon> propertyGroupIconsNeedingAnUpdate() {
        List<PropertyGroupIcon> needsUpdate = new();
        foreach (PropertyGroupIcon propertyGroupIcon in propertyGroupIcons) {
            if (propertyGroupIcon.iconNeedsToUpdate()) {
                needsUpdate.Add(propertyGroupIcon);
            }
        }
        return needsUpdate;
    }
    #endregion
}
