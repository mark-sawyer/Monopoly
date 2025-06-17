using UnityEngine;

public class DataUIAdjustmentPipeline : MonoBehaviour {
    #region moneyAdjustment
    [SerializeField] private PlayerIntEvent moneyAdjustment;
    [SerializeField] private PlayerIntEvent moneyAdjustmentData;
    [SerializeField] private PlayerEvent moneyAdjustmentUI;
    #endregion
    #region playerObtainedProperty
    [SerializeField] private PlayerPropertyEvent playerObtainedProperty;
    [SerializeField] private PlayerPropertyEvent playerObtainedPropertyData;
    [SerializeField] private PlayerPropertyEvent playerPropertyAdjustmentUI;
    #endregion
    #region turnPlayer
    [SerializeField] private GameEvent turnPlayer;
    [SerializeField] private GameEvent turnPlayerData;
    [SerializeField] private GameEvent turnPlayerUI;
    #endregion
    #region GOOJF card
    [SerializeField] private PlayerCardEvent playerGetsGOOJFCard;
    [SerializeField] private PlayerCardEvent playerGetsGOOJFCardData;
    [SerializeField] private PlayerCardTypeEvent playerGetsGOOJFCardUI;
    #endregion



    #region Monobehaviour
    private void Start() {
        moneyAdjustment.Listeners += moneyPipeline;
        playerObtainedProperty.Listeners += propertyBoughtPipeline;
        turnPlayer.Listeners += turnPlayerPipeline;
        playerGetsGOOJFCard.Listeners += playerGetsGOOJFCardPipeline;
    }
    #endregion



    #region Pipelines
    private void moneyPipeline(PlayerInfo playerInfo, int adjustment) {
        moneyAdjustmentData.invoke(playerInfo, adjustment);
        moneyAdjustmentUI.invoke(playerInfo);
    }
    private void propertyBoughtPipeline(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        playerObtainedPropertyData.invoke(playerInfo, propertyInfo);
        playerPropertyAdjustmentUI.invoke(playerInfo, propertyInfo);
    }
    private void turnPlayerPipeline() {
        turnPlayerData.invoke();
        turnPlayerUI.invoke();
    }
    private void playerGetsGOOJFCardPipeline(PlayerInfo playerInfo, CardInfo cardInfo) {
        playerGetsGOOJFCardData.invoke(playerInfo, cardInfo);
        playerGetsGOOJFCardUI.invoke(playerInfo, cardInfo.CardType);
    }
    #endregion
}
