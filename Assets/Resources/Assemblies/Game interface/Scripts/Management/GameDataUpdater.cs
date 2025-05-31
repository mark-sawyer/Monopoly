using UnityEngine;

public class GameDataUpdater : MonoBehaviour {
    [SerializeField] ReferencePasser rp;
    private GamePlayer gamePlayer;
    #region GameEvents
    [SerializeField] private GameEvent rollButtonClicked;
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> playerPurchasedProperty;
    [SerializeField] private GameEvent<PlayerInfo, int> moneyAdjustment;
    [SerializeField] private GameEvent turnOver;
    #endregion



    #region MonoBehaviour
    private void Start() {
        gamePlayer = rp.GamePlayer;
        rollButtonClicked.Listeners += rollDiceAndMovePlayer;
        playerPurchasedProperty.Listeners += purchasedProperty;
        moneyAdjustment.Listeners += adjustMoney;
        turnOver.Listeners += maybeUpdateTurnPlayer;
    }
    #endregion



    #region listeners
    private void rollDiceAndMovePlayer() {
        gamePlayer.rollDice();
        gamePlayer.moveTurnPlayerDiceValues();
    }
    private void purchasedProperty(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        gamePlayer.obtainProperty(playerInfo, propertyInfo);
    }
    private void adjustMoney(PlayerInfo playerInfo, int difference) {
        gamePlayer.adjustPlayerMoney(playerInfo, difference);
    }
    private void maybeUpdateTurnPlayer() {
        if (!GameState.game.DiceInfo.RolledDoubles) {
            gamePlayer.updateTurnPlayer();
        }
    }
    #endregion
}
