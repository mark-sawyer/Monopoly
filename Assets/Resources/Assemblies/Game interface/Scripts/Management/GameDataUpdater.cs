using UnityEngine;

public class GameDataUpdater : MonoBehaviour {
    [SerializeField] ReferencePasser rp;
    private GamePlayer gamePlayer;
    #region GameEvents
    [SerializeField] private GameEvent rollButtonClicked;
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> playerPurchasedProperty;
    [SerializeField] private GameEvent<PlayerInfo, int> moneyAdjustment;
    #endregion



    #region MonoBehaviour
    private void Start() {
        gamePlayer = rp.GamePlayer;
        rollButtonClicked.Listeners += rollDiceAndMovePlayer;
        playerPurchasedProperty.Listeners += purchasedProperty;
        moneyAdjustment.Listeners += adjustMoney;
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
    #endregion
}
