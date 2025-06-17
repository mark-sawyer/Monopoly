using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour {
    [SerializeField] private PropertyGroupIcon[] propertyGroupIcons;
    [SerializeField] private Transform propertyIconContainer;
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private MoneyAdjuster moneyAdjuster;
    [SerializeField] private Image highlightImage;
    [SerializeField] private Image chanceGOOJFImage;
    [SerializeField] private Image communityChestGOOJFImage;
    private PlayerInfo player;



    #region public
    public void setup(PlayerInfo player) {
        this.player = player;
        tokenIcon.setup(player.Token, player.Colour);
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
        IEnumerator pulse(Transform t) {
            float getScale(float x) {
                if (x <= 5) return 1f + 0.2f * x;
                else return 2f - (1f / 15f) * (x - 5f);
            }

            for (int i = 1; i <= 20; i++) {
                float scale = getScale(i);
                t.localScale = new Vector3(scale, scale, scale);
                yield return null;
            }
            t.localScale = new Vector3(1f, 1f, 1f);
        }

        if (cardType == CardType.COMMUNITY_CHEST) {
            communityChestGOOJFImage.enabled = toggle;
            if (toggle) StartCoroutine(pulse(communityChestGOOJFImage.transform));
        }
        else {
            chanceGOOJFImage.enabled = toggle;
            if (toggle) StartCoroutine(pulse(chanceGOOJFImage.transform));
        }
    }
    #endregion
}
