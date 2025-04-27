using System;
using System.Linq;
using UnityEngine;

public class Game {
    private Dice dice = new Dice();
    private Space[] spaces;
    private Player[] players;
    private Player turnPlayer;



    public Game(int playerNum) {
        spaces = initialiseSpaces();
        players = initialisePlayers(playerNum);
        initialiseProperties();
        turnPlayer = players[0];
    }
    public void turn() {
        rollDice();
        movePlayer(turnPlayer, dice.getValue());
        updateTurnPlayer();
    }
    public Player[] getPlayers() {
        return players;
    }
    public DieValueReader getDie(int index) {
        return dice.getDie(index);
    }
    public Player getTurnPlayer() {
        return turnPlayer;
    }
    public int getPlayerIndex(Player player) {
        return Array.FindIndex(players, x => x == player);
    }



    internal int getSpaceIndex(Space space) {
        return Array.IndexOf(spaces, space);
    }



    private void updateTurnPlayer() {
        int turnPlayerIndex = Array.IndexOf(players, turnPlayer);
        int nextTurnPlayer = (turnPlayerIndex + 1) % players.Length;
        turnPlayer = players[nextTurnPlayer];
    }
    private void movePlayer(Player player, int spacesMoved) {
        int currentIndex = Array.FindIndex<Space>(spaces, x => x.containsPlayer(player));
        int newIndex = (currentIndex + spacesMoved) % GameConstants.GAME_SPACES;
        spaces[currentIndex].removePlayer(player);
        spaces[newIndex].addPlayer(player);
        player.space = spaces[newIndex];
    }
    private void rollDice() {
        dice.roll();
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
            players[i] = new Player(spaces[0], (Token)i);
            spaces[0].addPlayer(players[i]);
        }
        return players;
    }
    private void initialiseProperties() {
        Property[] properties = new Property[] {
            Resources.Load<Estate>("ScriptableObjects/Properties/00_OldKentRoad"),
            Resources.Load<Estate>("ScriptableObjects/Properties/01_WhiteChapelRoad"),
            Resources.Load<Railroad>("ScriptableObjects/Properties/02_KingsCrossStation"),
            Resources.Load<Estate>("ScriptableObjects/Properties/03_TheAngelIslington"),
            Resources.Load<Estate>("ScriptableObjects/Properties/04_EustonRoad"),
            Resources.Load<Estate>("ScriptableObjects/Properties/05_PentonvilleRoad"),
            Resources.Load<Estate>("ScriptableObjects/Properties/06_PallMall"),
            Resources.Load<Utility>("ScriptableObjects/Properties/07_ElectricCompany"),
            Resources.Load<Estate>("ScriptableObjects/Properties/08_Whitehall"),
            Resources.Load<Estate>("ScriptableObjects/Properties/09_NorthumberlandAvenue"),
            Resources.Load<Railroad>("ScriptableObjects/Properties/10_MaryleboneStation"),
            Resources.Load<Estate>("ScriptableObjects/Properties/11_BowStreet"),
            Resources.Load<Estate>("ScriptableObjects/Properties/12_MarlboroughStreet"),
            Resources.Load<Estate>("ScriptableObjects/Properties/13_VineStreet"),
            Resources.Load<Estate>("ScriptableObjects/Properties/14_Strand"),
            Resources.Load<Estate>("ScriptableObjects/Properties/15_FleetStreet"),
            Resources.Load<Estate>("ScriptableObjects/Properties/16_TrafalgarSquare"),
            Resources.Load<Railroad>("ScriptableObjects/Properties/17_FenchurchStStation"),
            Resources.Load<Estate>("ScriptableObjects/Properties/18_LeicesterSquare"),
            Resources.Load<Estate>("ScriptableObjects/Properties/19_CoventryStreet"),
            Resources.Load<Utility>("ScriptableObjects/Properties/20_WaterWorks"),
            Resources.Load<Estate>("ScriptableObjects/Properties/21_Piccadilly"),
            Resources.Load<Estate>("ScriptableObjects/Properties/22_RegentStreet"),
            Resources.Load<Estate>("ScriptableObjects/Properties/23_OxfordStreet"),
            Resources.Load<Estate>("ScriptableObjects/Properties/24_BondStreet"),
            Resources.Load<Railroad>("ScriptableObjects/Properties/25_LiverpoolStreetStation"),
            Resources.Load<Estate>("ScriptableObjects/Properties/26_ParkLane"),
            Resources.Load<Estate>("ScriptableObjects/Properties/27_Mayfair")
        };
        EstateGroup[] estateGroups = new EstateGroup[] {
            new EstateGroup(
                EstateColour.BROWN,
                new Estate[] { (Estate)properties[0], (Estate)properties[1] }
            ),
            new EstateGroup(
                EstateColour.LIGHT_BLUE,
                new Estate[] { (Estate)properties[3], (Estate)properties[4], (Estate)properties[5] }
            ),
            new EstateGroup(
                EstateColour.PINK,
                new Estate[] { (Estate)properties[6], (Estate)properties[8], (Estate)properties[9] }
            ),
            new EstateGroup(
                EstateColour.ORANGE,
                new Estate[] { (Estate)properties[11], (Estate)properties[12], (Estate)properties[13] }
            ),
            new EstateGroup(
                EstateColour.RED,
                new Estate[] { (Estate)properties[14], (Estate)properties[15], (Estate)properties[16] }
            ),
            new EstateGroup(
                EstateColour.YELLOW,
                new Estate[] { (Estate)properties[18], (Estate)properties[19], (Estate)properties[21] }
            ),
            new EstateGroup(
                EstateColour.GREEN,
                new Estate[] { (Estate)properties[22], (Estate)properties[23], (Estate)properties[24] }
            ),
            new EstateGroup(
                EstateColour.DARK_BLUE,
                new Estate[] { (Estate)properties[26], (Estate)properties[27] }
            )
        };
        foreach (EstateGroup estateGroup in estateGroups) {
            estateGroup.setEstateReferencesToThis();
        }
    }
}
