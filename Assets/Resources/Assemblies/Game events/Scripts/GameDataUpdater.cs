using System.Collections.Generic;
using UnityEngine;

public class GameDataUpdater : MonoBehaviour {
    private GamePlayer gamePlayer;



    #region MonoBehaviour
    private void Start() {
        DataEventHub dataHub = DataEventHub.Instance;
        DataUIPipelineEventHub pipelineHub = DataUIPipelineEventHub.Instance;

        dataHub.sub_CardDrawn(drawCard);
        dataHub.sub_CardResolved(undrawCard);
        dataHub.sub_PlayerIncurredDebt(incurDebt);
        dataHub.sub_DoublesCountReset(resetDoublesCount);
        dataHub.sub_IncrementJailTurn(incrementJailTurn);
        dataHub.sub_EstateAddedBuilding(addBuildingToEstate);
        dataHub.sub_EstateRemovedBuilding(removeBuildingFromEstate);
        dataHub.sub_EstateGroupRemovedAllBuildings(sellAllBuildingsFromEstateGroup);
        dataHub.sub_PropertyMortgaged(mortgageProperty);
        dataHub.sub_PropertyUnmortgaged(unmortgageProperty);
        dataHub.sub_TradeCommenced(createNewTrade);
        dataHub.sub_MortgageIsResolved(setMortgageResolved);
        dataHub.sub_TurnPlayerWillLoseTurn(markTurnPlayerForLosingTurn);
        dataHub.sub_CardReturned(returnGOOJFCard);
        pipelineHub.sub_RollButtonClicked(rollDice);
        pipelineHub.sub_TurnPlayerMovedAlongBoard(moveTurnPlayerAlongBoard);
        pipelineHub.sub_TurnPlayerMovedToSpace(moveTurnPlayerToSpace);
        pipelineHub.sub_TurnPlayerSentToJail(sendTurnPlayerToJail);
        pipelineHub.sub_PlayerObtainedProperty(purchasedProperty);
        pipelineHub.sub_MoneyAdjustment(adjustMoney);
        pipelineHub.sub_MoneyBetweenPlayers(tradeMoney);
        pipelineHub.sub_NextPlayerTurn(updateTurnPlayer);
        pipelineHub.sub_PlayerGetsGOOJFCard(givePlayerGOOJFCard);
        pipelineHub.sub_LeaveJail(removeTurnPlayerFromJail);
        pipelineHub.sub_UseGOOJFCardButtonClicked(useGOOJFCard);
        pipelineHub.sub_TradeTerminated(removedTerminatedTrade);
        pipelineHub.sub_TradeUpdated(updateProposedTrade);
        pipelineHub.sub_TradeLockedIn(makeProposedTrade);
        pipelineHub.sub_DebtReduced(reduceDebt);
        pipelineHub.sub_MoneyRaisedForDebt(raiseMoneyForDebt);
        pipelineHub.sub_PlayerEliminated(eliminatePlayer);
    }
    #endregion



    #region public
    public void setup(GamePlayer gamePlayer) {
        this.gamePlayer = gamePlayer;
    }
    #endregion



    #region Listeners
    private void drawCard(CardType cardType) {
        gamePlayer.drawCard(cardType);
    }
    private void undrawCard() {
        gamePlayer.undrawCard();
    }
    private void incurDebt(PlayerInfo debtor, Creditor creditor, int owed) {
        gamePlayer.incurDebt(debtor, creditor, owed);
    }
    private void reduceDebt(PlayerInfo debtor, int paid) {
        gamePlayer.reduceDebt(debtor, paid);
    }
    private void raiseMoneyForDebt(PlayerInfo debtor, int moneyRaised) {
        gamePlayer.raiseMoneyForDebt(debtor, moneyRaised);
    }
    private void rollDice() {
        gamePlayer.rollDice();
    }
    private void moveTurnPlayerAlongBoard(int spacesMoved) {
        gamePlayer.moveTurnPlayerAlongBoard(spacesMoved);
    }
    private void moveTurnPlayerToSpace(SpaceInfo spaceInfo) {
        gamePlayer.moveTurnPlayerToSpace(spaceInfo);
    }
    private void sendTurnPlayerToJail() {
        gamePlayer.sendTurnPlayerToJail();
    }
    private void purchasedProperty(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        gamePlayer.obtainProperty(playerInfo, propertyInfo);
    }
    private void adjustMoney(PlayerInfo playerInfo, int difference) {
        gamePlayer.adjustPlayerMoney(playerInfo, difference);
    }
    private void tradeMoney(PlayerInfo losingPlayer, PlayerInfo gainingPlayer, int amount) {
        gamePlayer.tradePlayerMoney(losingPlayer, gainingPlayer, amount);
    }
    private void updateTurnPlayer() {
        gamePlayer.updateTurnPlayer();
        gamePlayer.resetDoublesCount();
    }
    private void givePlayerGOOJFCard(PlayerInfo playerInfo, CardInfo cardInfo) {
        gamePlayer.playerGetsGOOJFCard(playerInfo, cardInfo);
    }
    private void incrementJailTurn() {
        gamePlayer.incrementJailTurn();
    }
    private void removeTurnPlayerFromJail() {
        gamePlayer.removeTurnPlayerFromJail();
    }
    private void useGOOJFCard(CardType cardType) {
        gamePlayer.playerUsesGOOJFCard(cardType);
    }
    private void resetDoublesCount() {
        gamePlayer.resetDoublesCount();
    }
    private void addBuildingToEstate(EstateInfo estateInfo) {
        gamePlayer.addBuilding(estateInfo);
    }
    private void removeBuildingFromEstate(EstateInfo estateInfo) {
        gamePlayer.removeBuilding(estateInfo);
    }
    private void sellAllBuildingsFromEstateGroup(EstateGroupInfo estateGroupInfo) {
        gamePlayer.removeAllBuildingsFromEstateGroup(estateGroupInfo);
    }
    private void mortgageProperty(PropertyInfo propertyInfo) {
        gamePlayer.mortgageProperty(propertyInfo);
    }
    private void unmortgageProperty(PropertyInfo propertyInfo) {
        gamePlayer.unmortgageProperty(propertyInfo);
    }
    private void createNewTrade(PlayerInfo playerOne, PlayerInfo playerTwo) {
        gamePlayer.createNewTrade(playerOne, playerTwo);
    }
    private void removedTerminatedTrade() {
        gamePlayer.removedTerminatedTrade();
    }
    private void updateProposedTrade(List<TradableInfo> t1, List<TradableInfo> t2, PlayerInfo moneyGiver, int money) {
        gamePlayer.updateProposedTrade(t1, t2, moneyGiver, money);
    }
    private void makeProposedTrade() {
        gamePlayer.makeProposedTrade();
    }
    private void eliminatePlayer(PlayerInfo playerInfo) {
        gamePlayer.eliminatePlayer(playerInfo);
    }
    private void setMortgageResolved(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        gamePlayer.setMortgageResolved(playerInfo, propertyInfo);
    }
    private void markTurnPlayerForLosingTurn() {
        gamePlayer.markTurnPlayerForLosingTurn();
    }
    private void returnGOOJFCard(CardInfo cardInfo) {
        gamePlayer.eliminatedPlayerGOOJFCardReturned(cardInfo);
    }
    #endregion
}
