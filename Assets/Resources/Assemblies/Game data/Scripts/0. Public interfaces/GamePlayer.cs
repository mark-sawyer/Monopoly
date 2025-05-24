
public interface GamePlayer {
    public void rollDice();
    public void moveTurnPlayerDiceValues();
    public void updateTurnPlayer();
    public void obtainProperty(PlayerInfo playerInfo, PropertyInfo propertyInfo);
    public void addBuilding(EstateInfo estateInfo);
    public void adjustPlayerMoney(PlayerInfo playerInfo, int difference);
}
