using System.Collections.Generic;

public interface GameStateInfo {
    public IEnumerable<PlayerInfo> getPlayers();
    public PlayerInfo getTurnPlayer();
    public DiceInfo getDiceInfo();
    public int getPlayerIndex(PlayerInfo player);
    public int getIndexOfTurnPlayer();
    public int getNumberOfPlayers();
    public IEnumerable<PlayerInfo> getPlayersOnSpace(int spaceIndex);
    public int getSpaceIndexOfTurnPlayer();
}
