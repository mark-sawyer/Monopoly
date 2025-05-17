
using System.Collections.Generic;

public interface SpaceInfo {
    public int getPlayerOrderIndex(PlayerInfo playerInfo);
    public int NumberOfPlayersOnSpace { get; }
    public IEnumerable<PlayerInfo> VisitingPlayers { get; }
    public int Index { get; }
}
