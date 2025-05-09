using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : GameStateInfo, GamePlayer {
    private Dice dice = new Dice();
    private Space[] spaces;
    private Player[] players;
    private Player turnPlayer;



    #region public
    public Game(int playerNum) {
        spaces = initialiseSpaces();
        players = initialisePlayers(playerNum);
        turnPlayer = players[0];
    }
    public PropertyInfo DELETE_THIS_LATER() {
        return ((PropertySpace)spaces[11]).Property;
    }
    #endregion



    #region GameStateInfo
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
    public IEnumerable<PlayerInfo> PlayerInfos => players;
    public DiceInfo DiceInfo => dice;
    public PlayerInfo TurnPlayer => turnPlayer;
    public int NumberOfPlayers => players.Length;
    public int IndexOfTurnPlayer => getPlayerIndex(turnPlayer);
    public int SpaceIndexOfTurnPlayer => turnPlayer.SpaceIndex;
    #endregion



    #region GamePlayer
    public void rollDice() {
        dice.roll();
    }
    public void moveTurnPlayerDiceValues() {
        movePlayer(turnPlayer, dice.getTotalValue());
    }
    public void updateTurnPlayer() {
        int turnPlayerIndex = Array.IndexOf(players, turnPlayer);
        int nextTurnPlayer = (turnPlayerIndex + 1) % players.Length;
        turnPlayer = players[nextTurnPlayer];
    }
    #endregion



    #region private
    private void movePlayer(Player player, int spacesMoved) {
        int currentIndex = Array.FindIndex(spaces, x => x.containsPlayer(player));
        int newIndex = (currentIndex + spacesMoved) % GameConstants.TOTAL_SPACES;
        spaces[currentIndex].removePlayer(player);
        spaces[newIndex].addPlayer(player);
        player.space = spaces[newIndex];
    }
    private Space[] initialiseSpaces() {
        Space[] spaces = new Space[] {
            Resources.Load<Space>("ScriptableObjects/Spaces/00_cornerspace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/01_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/02_cardSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/03_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/04_taxSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/05_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/06_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/07_cardSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/08_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/09_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/10_cornerspace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/11_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/12_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/13_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/14_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/15_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/16_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/17_cardSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/18_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/19_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/20_cornerspace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/21_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/22_cardSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/23_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/24_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/25_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/26_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/27_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/28_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/29_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/30_cornerspace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/31_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/32_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/33_cardSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/34_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/35_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/36_cardSpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/37_propertySpace"),
            Resources.Load<Space>("ScriptableObjects/Spaces/38_taxSpace"),
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
        return players;
    }
    #endregion
}
