using UnityEngine;

public class GameDataUpdater : MonoBehaviour {
    private GamePlayer gamePlayer;
    #region GameEvents
    [SerializeField] private GameEvent rollButtonClicked;
    [SerializeField] private GameEvent turnPlayerSpaceUpdate;
    [SerializeField] private GameEvent turnPlayerSentToJail;
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> playerPurchasedProperty;
    [SerializeField] private GameEvent<PlayerInfo, int> moneyAdjustment;
    [SerializeField] private GameEvent nextPlayerTurn;
    #endregion



    #region MonoBehaviour
    private void Start() {
        rollButtonClicked.Listeners += rollDice;
        turnPlayerSpaceUpdate.Listeners += moveTurnPlayerDiceValues;
        playerPurchasedProperty.Listeners += purchasedProperty;
        moneyAdjustment.Listeners += adjustMoney;
        turnPlayerSentToJail.Listeners += sendTurnPlayerToJail;
        nextPlayerTurn.Listeners += updateTurnPlayer;
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
    #endregion
}
