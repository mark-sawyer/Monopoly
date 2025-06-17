using UnityEngine;

public class GameDataUpdater : MonoBehaviour {
    private GamePlayer gamePlayer;
    #region Listening GameEvents
    [SerializeField] private GameEvent rollButtonClicked;
    [SerializeField] private GameEvent turnPlayerSpaceUpdate;
    [SerializeField] private GameEvent turnPlayerSentToJail;
    [SerializeField] private PlayerPropertyEvent playerPurchasedProperty;
    [SerializeField] private PlayerIntEvent moneyAdjustment;
    [SerializeField] private GameEvent nextPlayerTurnData;
    [SerializeField] private CardTypeEvent cardDrawn;
    [SerializeField] private GameEvent cardResolved;
    [SerializeField] private PlayerCreditorIntEvent playerIncurredDebt;
    [SerializeField] private PlayerEvent debtResolved;
    [SerializeField] private SpaceEvent turnPlayerMovedToSpace;
    [SerializeField] private PlayerCardEvent playerGetsGOOJFCardData;
    #endregion



    #region MonoBehaviour
    private void Start() {
        rollButtonClicked.Listeners += rollDice;
        turnPlayerSpaceUpdate.Listeners += moveTurnPlayerDiceValues;
        turnPlayerMovedToSpace.Listeners += moveTurnPlayerToSpace;
        turnPlayerSentToJail.Listeners += sendTurnPlayerToJail;
        playerPurchasedProperty.Listeners += purchasedProperty;
        moneyAdjustment.Listeners += adjustMoney;
        nextPlayerTurnData.Listeners += updateTurnPlayer;
        cardDrawn.Listeners += drawCard;
        cardResolved.Listeners += undrawCard;
        playerIncurredDebt.Listeners += incurDebt;
        debtResolved.Listeners += setDebtToNull;
        playerGetsGOOJFCardData.Listeners += givePlayerGOOJFCard;
    }
    #endregion



    #region public
    public void setup(GamePlayer gamePlayer) {
        this.gamePlayer = gamePlayer;
    }
    #endregion



    #region listeners
    private void rollDice() {
        gamePlayer.rollDice();
    }
    private void moveTurnPlayerDiceValues() {
        gamePlayer.moveTurnPlayerDiceValues();
    }
    private void moveTurnPlayerToSpace(SpaceInfo spaceInfo) {
        gamePlayer.moveTurnPlayerToSpace(spaceInfo);
    }
    private void sendTurnPlayerToJail() {
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        gamePlayer.sendPlayerToJail(turnPlayer);
    }
    private void purchasedProperty(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        gamePlayer.obtainProperty(playerInfo, propertyInfo);
    }
    private void adjustMoney(PlayerInfo playerInfo, int difference) {
        gamePlayer.adjustPlayerMoney(playerInfo, difference);
    }
    private void sendPlayerToJail(PlayerInfo playerInfo) {
        gamePlayer.sendPlayerToJail(playerInfo);
    }
    private void updateTurnPlayer() {
        gamePlayer.updateTurnPlayer();
        gamePlayer.resetDoublesCount();
    }
    private void drawCard(CardType cardType) {
        gamePlayer.drawCard(cardType);
    }
    private void undrawCard() {
        gamePlayer.undrawCard();
    }
    private void incurDebt(PlayerInfo debtor, Creditor creditor, int owed) {
        gamePlayer.incurDebt(debtor, creditor, owed);
    }
    private void setDebtToNull(PlayerInfo debtor) {
        gamePlayer.removeDebt(debtor);
    }
    private void givePlayerGOOJFCard(PlayerInfo playerInfo, CardInfo cardInfo) {
        gamePlayer.playerGetsGOOJFCard(playerInfo, cardInfo);
    }
    #endregion
}
