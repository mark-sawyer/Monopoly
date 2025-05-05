using System.Collections.Generic;

public interface GameStateInfo {
    public IEnumerable<PlayerInfo> getPlayers();
    public PlayerInfo getTurnPlayer();
    public DiceInfo getDiceInfo();
    public SpaceInfo getSpaceInfo(int index);
    public int getSpaceIndex(SpaceInfo space);
    public int getPlayerIndex(PlayerInfo player);
    public int getIndexOfTurnPlayer();
    public int getNumberOfPlayers();
    public int getSpaceIndexOfTurnPlayer();
}
