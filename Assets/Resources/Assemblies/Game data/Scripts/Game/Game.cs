using System;
using System.Collections.Generic;
using UnityEngine;

internal class Game : GameStateInfo, GamePlayer {
    private DiceInterface dice;
    private Space[] spaces;
    private Player[] players;
    private Player turnPlayer;
    private Queue<House> houses;
    private Queue<Hotel> hotels;
    private Queue<CardInstance> communityChestCards;
    private Queue<CardInstance> chanceCards;



    #region internal
    internal Game(int playerNum, DiceInterface dice) {
        this.dice = dice;
        spaces = initialiseSpaces();
        players = initialisePlayers(playerNum);
        houses = initialiseHouses();
        hotels = initialiseHotels();
        communityChestCards = initialiseCards(CardType.COMMUNITY_CHEST);
        chanceCards = initialiseCards(CardType.CHANCE);
        turnPlayer = players[0];
    }
    #endregion



    #region GameStateInfo
    public IEnumerable<PlayerInfo> PlayerInfos => players;
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
    #endregion



    #region GamePlayer
    public void rollDice() {
        dice.roll();
    }
    public void moveTurnPlayerDiceValues() {
        movePlayer(turnPlayer, dice.TotalValue);
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
            for (int i = 0; i < 4; i++) houses.Enqueue((House)estate.removeBuilding());
            estate.addBuilding(hotels.Dequeue());
        }
        else estate.addBuilding(houses.Dequeue());
    }
    public void adjustPlayerMoney(PlayerInfo playerInfo, int difference) {
        Player player = (Player)playerInfo;
        player.adjustMoney(difference);
    }
    public void sendPlayerToJail(PlayerInfo playerInfo) {
        Player player = (Player)playerInfo;
        player.goToJail();
    }
    public void resetDoublesCount() {
        dice.resetDoublesCount();
    }
    #endregion



    #region private
    private void movePlayer(Player player, int spacesMoved) {
        int currentIndex = Array.FindIndex(spaces, x => x.containsPlayer(player));
        int newIndex = (currentIndex + spacesMoved) % GameConstants.TOTAL_SPACES;
        spaces[currentIndex].removePlayer(player);
        spaces[newIndex].addPlayer(player);
        player.Space = spaces[newIndex];
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
            space.setupSpace(this);
        }
        return spaces;
    }
    private Player[] initialisePlayers(int playerNum) {
        Player[] players = new Player[playerNum];
        for (int i = 0; i < playerNum; i++) {
            players[i] = new Player(
                spaces[0],
                (Token)i,
                (PlayerColour)UnityEngine.Random.Range(0, 8)
            );
            spaces[0].addPlayer(players[i]);
        }
        Player.jailSpace = spaces[10];
        return players;
    }
    private Queue<House> initialiseHouses() {
        Queue<House> houseQueue = new Queue<House>(GameConstants.TOTAL_HOUSES);
        for (int i = 0; i < GameConstants.TOTAL_HOUSES; i++) {
            houseQueue.Enqueue(new House());
        }
        return houseQueue;
    }
    private Queue<Hotel> initialiseHotels() {
        Queue<Hotel> hotelQueue = new Queue<Hotel>(GameConstants.TOTAL_HOTELS);
        for (int i = 0; i < GameConstants.TOTAL_HOTELS; i++) {
            hotelQueue.Enqueue(new Hotel());
        }
        return hotelQueue;
    }
    private Queue<CardInstance> initialiseCards(CardType cardType) {
        Deck deck = cardType == CardType.COMMUNITY_CHEST
            ? Resources.Load<Deck>("ScriptableObjects/Cards/cc_deck")
            : Resources.Load<Deck>("ScriptableObjects/Cards/chance_deck");
        return deck.getAsQueue();
    }
    #endregion
}
