using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour {
    [SerializeField] private PropertyGroupIconSection propertyGroupIconSection;
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private MoneyAdjuster moneyAdjuster;
    [SerializeField] private Image highlightImage;
    [SerializeField] private GOOJFIcon chanceGOOJFIcon;
    [SerializeField] private GOOJFIcon ccGOOJFIcon;
    [SerializeField] private Canvas thisCanvas;
    private PlayerInfo playerInfo;
    private Dictionary<CardType, GOOJFIcon> GOOJFIconDict;



    #region public
    public PlayerInfo PlayerInfo => playerInfo;
    public PropertyGroupIconSection PropertyGroupIconSection => propertyGroupIconSection;
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
        propertyGroupIconSection.setup(playerInfo);
    }
    public void adjustMoney(PlayerInfo player) {
        moneyAdjuster.adjustMoney(player);
    }
    public void adjustMoneyQuietly(PlayerInfo player) {
        moneyAdjuster.adjustMoneyQuietly(player);
    }
    public void toggleHighlightImage(bool toggle) {
        highlightImage.enabled = toggle;
    }
    public IEnumerator toggleGOOJFIcon(CardType cardType) {
        bool offButShouldBeOn = playerInfo.hasGOOJFCardOfType(cardType) && !GOOJFIconDict[cardType].IsOn;

        if (offButShouldBeOn) {
            SoundPlayer.Instance.play_Pop();
            yield return GOOJFIconDict[cardType].enable(true);
        }
        else {
            SoundPlayer.Instance.play_Pop();
            yield return GOOJFIconDict[cardType].enable(false);
        }
    }
    public bool needsGOOJFIconAdjusted(CardType cardType) {
        return playerInfo.hasGOOJFCardOfType(cardType)
            ^ GOOJFIconDict[cardType].IsOn;
    }
    public void toggleOverScreenCover(bool toggle) {
        thisCanvas.sortingOrder = toggle ? 2 : 0;
    }
    #endregion
}
