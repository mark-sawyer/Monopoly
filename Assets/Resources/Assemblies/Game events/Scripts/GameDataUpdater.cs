using UnityEngine;

public class GameDataUpdater : MonoBehaviour {
    private GamePlayer gamePlayer;



    #region MonoBehaviour
    private void Start() {
        DataEventHub dataHub = DataEventHub.Instance;

        dataHub.sub_RollButtonClicked(rollDice);
        dataHub.sub_TurnPlayerMovedAlongBoard(moveTurnPlayerAlongBoard);
        dataHub.sub_TurnPlayerMovedToSpace(moveTurnPlayerToSpace);
        dataHub.sub_TurnPlayerSentToJail(sendTurnPlayerToJail);
        dataHub.sub_CardDrawn(drawCard);
        dataHub.sub_CardResolved(undrawCard);
        dataHub.sub_PlayerIncurredDebt(incurDebt);
        dataHub.sub_DebtResolved(setDebtToNull);
        dataHub.sub_DoublesCountReset(resetDoublesCount);
        dataHub.sub_PlayerObtainedProperty(purchasedProperty);
        dataHub.sub_MoneyAdjustment(adjustMoney);
        dataHub.sub_MoneyBetweenPlayers(tradeMoney);
        dataHub.sub_NextPlayerTurn(updateTurnPlayer);
        dataHub.sub_PlayerGetsGOOJFCard(givePlayerGOOJFCard);
        dataHub.sub_IncrementJailTurn(incrementJailTurn);
        dataHub.sub_LeaveJail(removeTurnPlayerFromJail);
        dataHub.sub_UseGOOJFCardButtonClicked(useGOOJFCard);
        dataHub.sub_EstateAddedBuilding(addBuildingToEstate);
        dataHub.sub_EstateRemovedBuilding(removeBuildingFromEstate);
        dataHub.sub_PropertyMortgaged(mortgageProperty);
        dataHub.sub_PropertyUnmortgaged(unmortgageProperty);
    }
    #endregion



    #region public
    public void setup(GamePlayer gamePlayer) {
        this.gamePlayer = gamePlayer;
    }
    #endregion



    #region Listeners
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
    private void drawCard(CardType cardType) {
        gamePlayer.drawCard(cardType);
    }
    private void undrawCard() {
        gamePlayer.undrawCard();
    }
    private void incurDebt(PlayerInfo debtor, Creditor creditor, int owed) {
        gamePlayer.incurDebt(debtor, creditor, owed);
    }
    private void setDebtToNull(PlayerInfo debtor) {
        gamePlayer.removeDebt(debtor);
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
    private void mortgageProperty(PropertyInfo propertyInfo) {
        gamePlayer.mortgageProperty(propertyInfo);
    }
    private void unmortgageProperty(PropertyInfo propertyInfo) {
        gamePlayer.unmortgageProperty(propertyInfo);
    }
    #endregion
}
