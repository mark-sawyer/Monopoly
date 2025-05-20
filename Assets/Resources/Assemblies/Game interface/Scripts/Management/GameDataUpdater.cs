using UnityEngine;

public class GameDataUpdater : MonoBehaviour {
    [SerializeField] ReferencePasser rp;
    private GamePlayer gamePlayer;
    #region GameEvents
    [SerializeField] private GameEvent rollButtonClicked;
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> playerPurchasedProperty;
    #endregion



    #region MonoBehaviour
    private void Start() {
        gamePlayer = rp.GamePlayer;
        rollButtonClicked.Listeners += rollDiceAndMovePlayer;
        playerPurchasedProperty.Listeners += purchasedProperty;
    }
    #endregion



    #region listeners
    private void rollDiceAndMovePlayer() {
        gamePlayer.rollDice();
        gamePlayer.moveTurnPlayerDiceValues();
    }
    private void purchasedProperty(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        gamePlayer.purchaseProperty(playerInfo, propertyInfo);
    }
    #endregion
}
