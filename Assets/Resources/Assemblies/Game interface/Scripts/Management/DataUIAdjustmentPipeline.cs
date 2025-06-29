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
    #region Leaving jail
    [SerializeField] private GameEvent leaveJail;
    [SerializeField] private GameEvent leaveJailData;
    [SerializeField] private GameEvent leaveJailUI;
    #endregion
    #region Jail turn begin
    [SerializeField] private GameEvent jailTurnBegin;
    [SerializeField] private GameEvent jailTurnBeginData;
    [SerializeField] private GameEvent jailTurnBeginUI;
    #endregion
    #region Using GOOJF card
    [SerializeField] private CardTypeEvent useCardButtonClicked;
    [SerializeField] private CardTypeEvent useGOOJFCardData;
    [SerializeField] private CardTypeEvent useGOOJFCardUI;
    #endregion
    #region Estate added building
    [SerializeField] private EstateEvent estateAddedBuilding;
    [SerializeField] private EstateEvent estateAddedBuildingData;
    [SerializeField] private EstateEvent estateAddedBuildingUI;
    #endregion



    #region Monobehaviour
    private void Start() {
        moneyAdjustment.Listeners += moneyPipeline;
        playerObtainedProperty.Listeners += propertyBoughtPipeline;
        turnPlayer.Listeners += turnPlayerPipeline;
        playerGetsGOOJFCard.Listeners += playerGetsGOOJFCardPipeline;
        leaveJail.Listeners += leaveJailPipeline;
        jailTurnBegin.Listeners += jailTurnBeginPipeline;
        useCardButtonClicked.Listeners += useGOOJFCardPipeline;
        estateAddedBuilding.Listeners += estateAddedBuildingPipeline;
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
    private void leaveJailPipeline() {
        leaveJailData.invoke();
        leaveJailUI.invoke();
    }
    private void jailTurnBeginPipeline() {
        jailTurnBeginData.invoke();
        jailTurnBeginUI.invoke();
    }
    private void useGOOJFCardPipeline(CardType cardType) {
        useGOOJFCardData.invoke(cardType);
        useGOOJFCardUI.invoke(cardType);
    }
    private void estateAddedBuildingPipeline(EstateInfo estateInfo) {
        estateAddedBuildingData.invoke(estateInfo);
        estateAddedBuildingUI.invoke(estateInfo);
    }
    #endregion
}
