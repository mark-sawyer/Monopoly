using System.Collections.Generic;

public interface GameStateInfo {
    public IEnumerable<PlayerInfo> PlayerInfos { get; }
    public PlayerInfo TurnPlayer { get; }
    public int IndexOfTurnPlayer { get; }
    public int SpaceIndexOfTurnPlayer { get; }
    public SpaceInfo SpaceInfoOfTurnPlayer { get; }
    public DiceInfo DiceInfo { get; }
    public int NumberOfPlayers { get; }
    public SpaceInfo getSpaceInfo(int index);
    public int getSpaceIndex(SpaceInfo space);
    public PlayerInfo getPlayerInfo(int index);
    public int getPlayerIndex(PlayerInfo player);
}
