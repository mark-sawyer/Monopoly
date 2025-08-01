using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class Game : GameStateInfo, GamePlayer {
    private DiceInterface dice;
    private Bank bank;
    private Space[] spaces;
    private Player[] players;
    private Player turnPlayer;
    private Queue<Card> communityChestCards;
    private Queue<Card> chanceCards;
    private Card drawnCard;



    #region internal
    internal Game(
        int playerNum,
        int startingMoney,
        DiceInterface dice,
        Queue<Card> communityChestCards,
        Queue<Card> chanceCards
    ) {
        this.dice = dice;
        this.communityChestCards = communityChestCards;
        this.chanceCards = chanceCards;
        bank = new Bank();
        spaces = initialiseSpaces();
        initialiseUtilities();
        players = initialisePlayers(playerNum, startingMoney);
        turnPlayer = players[0];
    }
    #endregion



    #region GameStateInfo
    public IEnumerable<PlayerInfo> PlayerInfos => players;
    public IEnumerable<PlayerInfo> ActivePlayers => players.Where(x => x.IsActive);
    public DiceInfo DiceInfo => dice;
    public PlayerInfo TurnPlayer => turnPlayer;
    public int IndexOfTurnPlayer => getPlayerIndex(turnPlayer);
    public int SpaceIndexOfTurnPlayer => turnPlayer.SpaceIndex;
    public SpaceInfo SpaceInfoOfTurnPlayer => turnPlayer.SpaceInfo;
    public int NumberOfPlayers => players.Length;
    public SpaceInfo getSpaceInfo(int index) {
        return spaces[index];
    }
    public int getSpaceIndex(SpaceInfo space) {
        return Array.IndexOf(spaces, space);
    }
    public PlayerInfo getPlayerInfo(int index) {
        return players[index];
    }
    public int getPlayerIndex(PlayerInfo player) {
        return Array.FindIndex(players, x => x == player);
    }
    public Creditor BankCreditor => bank;
    public BankInfo BankInfo => bank;
    public CardInfo DrawnCard => drawnCard;
    #endregion



    #region GamePlayer
    public void rollDice() {
        dice.roll();
    }
    public void moveTurnPlayerAlongBoard(int spacesMoved) {
        movePlayer(turnPlayer, spacesMoved);
    }
    public void moveTurnPlayerToSpace(SpaceInfo spaceInfo) {
        Space space = (Space)spaceInfo;
        turnPlayer.changeSpace(space);
    }
    public void updateTurnPlayer() {
        int turnPlayerIndex = Array.IndexOf(players, turnPlayer);
        int nextTurnPlayer = (turnPlayerIndex + 1) % players.Length;
        turnPlayer = players[nextTurnPlayer];
    }
    public void obtainProperty(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        Player player = (Player)playerInfo;
        Property property = (Property)propertyInfo;
        player.obtainProperty(property);
    }
    public void addBuilding(EstateInfo estateInfo) {
        Estate estate = (Estate)estateInfo;
        if (estate.BuildingCount == 4) {
            for (int i = 0; i < 4; i++) bank.returnHouse((House)estate.removeBuilding());
            estate.addBuilding(bank.getHotel());
        }
        else estate.addBuilding(bank.getHouse());
    }
    public void removeBuilding(EstateInfo estateInfo) {
        Estate estate = (Estate)estateInfo;
        Building removedBuilding = estate.removeBuilding();
        if (removedBuilding is Hotel hotel) {
            bank.returnHotel(hotel);
            for (int i = 0; i < 4; i++) estate.addBuilding(bank.getHouse());
        }
        else if (removedBuilding is House house) bank.returnHouse(house);
    }
    public void mortgageProperty(PropertyInfo propertyInfo) {
        Property property = (Property)propertyInfo;
        property.mortgage();
    }
    public void unmortgageProperty(PropertyInfo propertyInfo) {
        Property property = (Property)propertyInfo;
        property.unmortgage();
    }
    public void incurDebt(PlayerInfo debtor, Creditor creditor, int owed) {
        Player debtorPlayer = (Player)debtor;
        debtorPlayer.incurDebt(creditor, owed);
    }
    public void removeDebt(PlayerInfo debtor) {
        ((Player)debtor).removeDebt();
    }
    public void adjustPlayerMoney(PlayerInfo playerInfo, int difference) {
        Player player = (Player)playerInfo;
        player.adjustMoney(difference);
    }
    public void tradePlayerMoney(PlayerInfo losingPlayer, PlayerInfo gainingPlayer, int amount) {
        ((Player)losingPlayer).adjustMoney(-amount);
        ((Player)gainingPlayer).adjustMoney(amount);
    }
    public void sendTurnPlayerToJail() {
        turnPlayer.changeSpace(spaces[GameConstants.JAIL_SPACE_INDEX]);
        turnPlayer.InJail = true;
    }
    public void removeTurnPlayerFromJail() {
        turnPlayer.exitJail();
    }
    public void resetDoublesCount() {
        dice.resetDoublesCount();
    }
    public void drawCard(CardType cardType) {
        if (cardType == CardType.COMMUNITY_CHEST) drawnCard = communityChestCards.Dequeue();
        else drawnCard = chanceCards.Dequeue();
    }
    public void undrawCard() {
        if (drawnCard.CardMechanicInfo is not GetOutOfJailFreeCardInfo) {
            if (drawnCard.CardType == CardType.COMMUNITY_CHEST) communityChestCards.Enqueue(drawnCard);
            else chanceCards.Enqueue(drawnCard);
        }
        drawnCard = null;
    }
    public void playerGetsGOOJFCard(PlayerInfo playerInfo, CardInfo cardInfo) {
        Card getOutOfJailFreeCard = (Card)cardInfo;
        if (getOutOfJailFreeCard.CardMechanic is not GetOutOfJailFreeCard) {
            throw new System.Exception("Not a GetOutOfJailFreeCard");
        }
        Player player = (Player)playerInfo;
        player.getGOOJFCard(getOutOfJailFreeCard);
    }
    public void playerUsesGOOJFCard(CardType cardType) {
        turnPlayer.exitJail();
        Card getOutOfJailFreeCard = turnPlayer.handBackGOOJFCard(cardType);
        if (cardType == CardType.CHANCE) chanceCards.Enqueue(getOutOfJailFreeCard);
        else communityChestCards.Enqueue(getOutOfJailFreeCard);
    }
    public void incrementJailTurn() {
        turnPlayer.incrementJailTurn();
    }
    #endregion



    #region private
    private void movePlayer(Player player, int spacesMoved) {
        int oldIndex = player.SpaceIndex;
        int newIndex = (oldIndex + spacesMoved) % GameConstants.TOTAL_SPACES;
        player.changeSpace(spaces[newIndex]);
    }
    private Space[] initialiseSpaces() {
        Space[] spaces = new Space[] {
            Resources.Load<Space>("ScriptableObjects/Spaces/00_goSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/01_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/02_cardSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/03_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/04_incomeTaxSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/05_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/06_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/07_cardSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/08_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/09_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/10_jailspace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/11_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/12_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/13_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/14_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/15_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/16_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/17_cardSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/18_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/19_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/20_freeParkingspace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/21_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/22_cardSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/23_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/24_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/25_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/26_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/27_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/28_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/29_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/30_goToJailspace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/31_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/32_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/33_cardSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/34_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/35_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/36_cardSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/37_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/38_luxuryTaxSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/39_propertySpace")
        };
        foreach (Space space in spaces) {
            space.setupSpace(this, bank);
        }
        return spaces;
    }
    private void initialiseUtilities() {
        Utility electricCompany = Resources.Load<Utility>("ScriptableObjects/Properties/07_ElectricCompany");
        Utility waterWorks = Resources.Load<Utility>("ScriptableObjects/Properties/20_WaterWorks");
        electricCompany.setup(dice);
        waterWorks.setup(dice);
    }
    private Player[] initialisePlayers(int playerNum, int startingMoney) {
        Player[] players = new Player[playerNum];
        for (int i = 0; i < playerNum; i++) {
            players[i] = new Player(
                spaces[0],
                (Token)UnityEngine.Random.Range(0, 8),
                (PlayerColour)UnityEngine.Random.Range(0, 8),
                startingMoney
            );
            spaces[0].addPlayer(players[i]);
        }
        return players;
    }
    #endregion
}
