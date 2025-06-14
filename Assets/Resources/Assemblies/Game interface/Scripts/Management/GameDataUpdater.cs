using UnityEngine;

public class GameDataUpdater : MonoBehaviour {
    private GamePlayer gamePlayer;
    #region Listening GameEvents
    [SerializeField] private GameEvent rollButtonClicked;
    [SerializeField] private GameEvent turnPlayerSpaceUpdate;
    [SerializeField] private GameEvent turnPlayerSentToJail;
    [SerializeField] private PlayerPropertyEvent playerPurchasedProperty;
    [SerializeField] private PlayerIntEvent moneyAdjustment;
    [SerializeField] private GameEvent nextPlayerTurn;
    [SerializeField] private CardTypeEvent cardDrawn;
    [SerializeField] private GameEvent cardResolved;
    [SerializeField] private PlayerCreditorIntEvent playerIncurredDebt;
    [SerializeField] private PlayerEvent debtResolved;
    [SerializeField] private SpaceEvent turnPlayerMovedToSpace;
    #endregion
    [SerializeField] private CardInfoEvent cardRevealed;
    [SerializeField] private GameEvent cardUnrevealed;



    #region MonoBehaviour
    private void Start() {
        rollButtonClicked.Listeners += rollDice;
        turnPlayerSpaceUpdate.Listeners += moveTurnPlayerDiceValues;
        turnPlayerMovedToSpace.Listeners += moveTurnPlayerToSpace;
        turnPlayerSentToJail.Listeners += sendTurnPlayerToJail;
        playerPurchasedProperty.Listeners += purchasedProperty;
        moneyAdjustment.Listeners += adjustMoney;
        nextPlayerTurn.Listeners += updateTurnPlayer;
        cardDrawn.Listeners += drawCard;
        cardResolved.Listeners += replaceCard;
        playerIncurredDebt.Listeners += incurDebt;
        debtResolved.Listeners += setDebtToNull;
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
        CardInfo revealedCard = gamePlayer.drawCard(cardType);
        cardRevealed.invoke(revealedCard);
    }
    private void replaceCard() {
        if (RevealedCard.card is not GetOutOfJailFreeCardInfo) {
            gamePlayer.bottomDeckCard(RevealedCard.card);
        }
        cardUnrevealed.invoke();
    }
    private void incurDebt(PlayerInfo debtor, Creditor creditor, int owed) {
        gamePlayer.incurDebt(debtor, creditor, owed);
    }
    private void setDebtToNull(PlayerInfo debtor) {
        gamePlayer.removeDebt(debtor);
    }
    #endregion
}
