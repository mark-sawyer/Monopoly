
using System.Collections.Generic;

public interface SpaceInfo {
    public int getNumberOfPlayersOnSpace();
    public IEnumerable<PlayerInfo> getVisitingPlayers();
    public int getIndex();
    public int getPlayerOrderIndex(PlayerInfo playerInfo);
}
