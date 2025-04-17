using System;
using System.Collections.Generic;
using System.Linq;

public class Game {
    private Dice dice = new Dice();
    private Space[] spaces;
    private Player[] players;
    private Player turnPlayer;

    public Game(int playerNum) {
        spaces = initialiseSpaces();
        players = initialisePlayers(playerNum);
        turnPlayer = players[0];
    }

    
    
    private void turn() {
        rollDice();
        movePlayer(turnPlayer, dice.getValue());
        updateTurnPlayer();
    }
    private void updateTurnPlayer() {
        int turnPlayerIndex = Array.IndexOf(players, turnPlayer);
        int nextTurnPlayer = (turnPlayerIndex + 1) % players.Length;
        turnPlayer = players[nextTurnPlayer];
    }
    private void movePlayer(Player player, int spacesMoved) {
        int currentIndex = Array.FindIndex<Space>(spaces, x => x.containsPlayer(player));
        int newIndex = (currentIndex + spacesMoved) % Constants.GAME_SPACES;
        spaces[currentIndex].removePlayer(player);
        spaces[newIndex].addPlayer(player);
        player.space = spaces[newIndex];
    }
    private void rollDice() {
        dice.roll();
    }



    private Space[] initialiseSpaces() {
        Space[] spaces = new Space[Constants.GAME_SPACES];
        for (int i = 0; i < Constants.GAME_SPACES; i++) {
            spaces[i] = new Space();
        }
        return spaces;
    }
    private Player[] initialisePlayers(int playerNum) {
        Player[] players = new Player[playerNum];
        for (int i = 0; i < playerNum; i++) {
            players[i] = new Player(spaces[0]);
        }
        return players;
    }
}
