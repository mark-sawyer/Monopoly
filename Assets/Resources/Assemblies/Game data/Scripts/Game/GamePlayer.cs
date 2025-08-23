
using System.Collections.Generic;

public interface GamePlayer {
    public void addBuilding(EstateInfo estateInfo);
    public void adjustPlayerMoney(PlayerInfo playerInfo, int difference);
    public void createNewTrade(PlayerInfo playerOne, PlayerInfo playerTwo);
    public void drawCard(CardType cardType);
    public void eliminatePlayer(PlayerInfo playerInfo);
    public void eliminatedPlayerGOOJFCardReturned(CardInfo cardInfo);
    public void incrementJailTurn();
    public void incurDebt(PlayerInfo debtor, Creditor creditor, int owed);
    public void incurMultiCreditorDebt(PlayerInfo debtor, int owedToEach);
    public void makeProposedTrade();
    public void markTurnPlayerForLosingTurn();
    public void mortgageProperty(PropertyInfo propertyInfo);
    public void moveTurnPlayerAlongBoard(int spacesMoved);
    public void moveTurnPlayerToSpace(SpaceInfo spaceInfo);
    public void obtainProperty(PlayerInfo playerInfo, PropertyInfo propertyInfo);
    public void payDebtFromMoneyRaised(PlayerInfo debtor, int amount);
    public void payDebtWithTradedMoney(PlayerInfo debtor);
    public void playerGetsGOOJFCard(PlayerInfo playerInfo, CardInfo cardInfo);
    public void playerUsesGOOJFCard(CardType cardType);
    public void reduceDebt(PlayerInfo debtor, int paid);
    public void removeAllBuildingsFromEstateGroup(EstateGroupInfo estateGroupInfo);
    public void removeBuilding(EstateInfo estateInfo);
    public void removedTerminatedTrade();
    public void removeTurnPlayerFromJail();
    public void resetDoublesCount();
    public void rollDice();
    public void sendTurnPlayerToJail();
    public void setJailDebtBool(PlayerInfo playerInfo, bool b);
    public void setMortgageResolved(PlayerInfo playerInfo, PropertyInfo propertyInfo);
    public void tradePlayerMoney(PlayerInfo losingPlayer, PlayerInfo gainingPlayer, int amount);
    public void undrawCard();
    public void unmortgageProperty(PropertyInfo propertyInfo);
    public void updateProposedTrade(List<TradableInfo> t1, List<TradableInfo> t2, PlayerInfo moneyGiver, int money);
    public void updateTurnPlayer();
}
