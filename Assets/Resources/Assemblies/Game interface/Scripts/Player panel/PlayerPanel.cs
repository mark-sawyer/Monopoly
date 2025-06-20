using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour {
    [SerializeField] private PropertyGroupIcon[] propertyGroupIcons;
    [SerializeField] private Transform propertyIconContainer;
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private MoneyAdjuster moneyAdjuster;
    [SerializeField] private Image highlightImage;
    [SerializeField] private GOOJFIcon chanceGOOJFIcon;
    [SerializeField] private GOOJFIcon ccGOOJFIcon;
    private PlayerInfo player;



    #region public
    public void setup(PlayerInfo player) {
        this.player = player;
        tokenIcon.setup(player.Token, player.Colour);
        moneyAdjuster.setStartingMoney(player.Money);
    }
    public void adjustMoney(PlayerInfo player) {
        moneyAdjuster.adjustMoney(player);
    }
    public void updatePropertyIconVisual(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        PropertyGroupIcon getPropertyGroupIcon() {
            if (propertyInfo is EstateInfo estateInfo) {
                int groupID = estateInfo.EstateGroupInfo.GroupID;
                if (groupID <= 4) return propertyIconContainer.GetChild(groupID - 1).GetComponent<PropertyGroupIcon>();
                else return propertyIconContainer.GetChild(groupID).GetComponent<PropertyGroupIcon>();
            }
            else if (propertyInfo is RailroadInfo) return propertyIconContainer.GetChild(4).GetComponent<PropertyGroupIcon>();
            else return propertyIconContainer.GetChild(9).GetComponent<PropertyGroupIcon>();
        }

        PropertyGroupIcon propertyGroupIcon = getPropertyGroupIcon();
        StartCoroutine(propertyGroupIcon.pulseAndUpdate(player));
    }
    public void toggleHighlightImage(bool toggle) {
        highlightImage.enabled = toggle;
    }
    public void toggleGOOJFIcon(CardType cardType, bool toggle) {
        if (cardType == CardType.CHANCE) chanceGOOJFIcon.enable(toggle);
        else ccGOOJFIcon.enable(toggle);
    }
    #endregion
}
