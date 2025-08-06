
using System.Collections.Generic;

public interface GamePlayer {
    public void rollDice();
    public void moveTurnPlayerAlongBoard(int spacesMoved);
    public void moveTurnPlayerToSpace(SpaceInfo spaceInfo);
    public void updateTurnPlayer();
    public void obtainProperty(PlayerInfo playerInfo, PropertyInfo propertyInfo);
    public void addBuilding(EstateInfo estateInfo);
    public void removeBuilding(EstateInfo estateInfo);
    public void mortgageProperty(PropertyInfo propertyInfo);
    public void unmortgageProperty(PropertyInfo propertyInfo);
    public void incurDebt(PlayerInfo debtor, Creditor creditor, int owed);
    public void removeDebt(PlayerInfo debtor);
    public void adjustPlayerMoney(PlayerInfo playerInfo, int difference);
    public void tradePlayerMoney(PlayerInfo losingPlayer, PlayerInfo gainingPlayer, int amount);
    public void sendTurnPlayerToJail();
    public void removeTurnPlayerFromJail();
    public void resetDoublesCount();
    public void drawCard(CardType cardType);
    public void undrawCard();
    public void playerGetsGOOJFCard(PlayerInfo playerInfo, CardInfo cardInfo);
    public void playerUsesGOOJFCard(CardType cardType);
    public void incrementJailTurn();
    public void createNewTrade(PlayerInfo playerOne, PlayerInfo playerTwo);
    public void removedTerminatedTrade();
    public void updateProposedTrade(List<TradableInfo> t1, List<TradableInfo> t2, PlayerInfo moneyGiver, int money);
    public void makeProposedTrade();
}
