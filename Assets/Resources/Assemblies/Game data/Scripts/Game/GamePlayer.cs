
public interface GamePlayer {
    public void rollDice();
    public void moveTurnPlayerDiceValues();
    public void moveTurnPlayerToSpace(SpaceInfo spaceInfo);
    public void updateTurnPlayer();
    public void obtainProperty(PlayerInfo playerInfo, PropertyInfo propertyInfo);
    public void addBuilding(EstateInfo estateInfo);
    public void incurDebt(PlayerInfo debtor, Creditor creditor, int owed);
    public void removeDebt(PlayerInfo debtor);
    public void adjustPlayerMoney(PlayerInfo playerInfo, int difference);
    public void sendPlayerToJail(PlayerInfo playerInfo);
    public void removeTurnPlayerFromJail();
    public void resetDoublesCount();
    public void drawCard(CardType cardType);
    public void undrawCard();
    public void playerGetsGOOJFCard(PlayerInfo playerInfo, CardInfo cardInfo);
    public void playerUsesGOOJFCard(CardType cardType);
    public void incrementJailTurn();
}
