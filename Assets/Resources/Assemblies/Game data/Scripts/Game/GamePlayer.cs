
public interface GamePlayer {
    public void rollDice();
    public void moveTurnPlayerDiceValues();
    public void updateTurnPlayer();
    public void obtainProperty(PlayerInfo playerInfo, PropertyInfo propertyInfo);
    public void addBuilding(EstateInfo estateInfo);
    public void incurDebt(PlayerInfo debtor, Creditor creditor, int owed);
    public void removeDebt(PlayerInfo debtor);
    public void adjustPlayerMoney(PlayerInfo playerInfo, int difference);
    public void sendPlayerToJail(PlayerInfo playerInfo);
    public void resetDoublesCount();
    public CardInfo drawCard(CardType cardType);
    public void bottomDeckCard(CardInfo cardInfo);
    public void resolveCard(CardInfo cardInfo);
}
