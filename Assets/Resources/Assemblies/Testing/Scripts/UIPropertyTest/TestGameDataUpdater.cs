using UnityEngine;

public class TestGameDataUpdater : MonoBehaviour {
    [SerializeField] GameEvent<PlayerInfo, PropertyInfo> playerObtainedProperty;
    [SerializeField] GameEvent<EstateInfo> estateAddedBuilding;
    [SerializeField] GameEvent<PlayerInfo, int> moneyAdjustment;
    private GamePlayer gamePlayer;



    #region MonoBehaviour
    private void Start() {
        playerObtainedProperty.Listeners += buyProperty;
        estateAddedBuilding.Listeners += addBuilding;
        moneyAdjustment.Listeners += adjustMoney;
    }
    #endregion



    #region public
    public void setGamePlayer(GamePlayer gamePlayer) {
        this.gamePlayer = gamePlayer;
    }
    #endregion



    #region private
    private void buyProperty(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        gamePlayer.obtainProperty(playerInfo, propertyInfo);
    }
    private void addBuilding(EstateInfo estateInfo) {
        gamePlayer.addBuilding(estateInfo);
    }
    private void adjustMoney(PlayerInfo playerInfo, int difference) {
        gamePlayer.adjustPlayerMoney(playerInfo, difference);
    }
    #endregion
}
