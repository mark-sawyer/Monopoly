using System.Collections.Generic;
using UnityEngine;

internal abstract class Space : ScriptableObject, SpaceInfo {
    private GameStateInfo gameInfo;
    private List<Player> playersVisiting;



    #region SpaceInfo
    public int getPlayerOrderIndex(PlayerInfo playerInfo) {
        return playersVisiting.IndexOf((Player)playerInfo);
    }
    public IEnumerable<PlayerInfo> VisitingPlayers => playersVisiting;
    public int NumberOfPlayersOnSpace => playersVisiting.Count;
    public int Index => gameInfo.getSpaceIndex(this);
    #endregion



    #region internal
    internal virtual void setupSpace(GameStateInfo gameInfo, BankInfo bankInfo) {
        this.gameInfo = gameInfo;
        playersVisiting = new List<Player>();
        if (this is PropertySpace propertySpace) {
            Property property = propertySpace.Property;
            if (property is Estate estate) {
                estate.setup(bankInfo);
            }
        }
    }
    internal void addPlayer(Player player) {
        playersVisiting.Add(player);
    }
    internal void removePlayer(Player player) {
        playersVisiting.Remove(player);
    }
    internal bool containsPlayer(Player player) {
        return playersVisiting.Contains(player);
    }
    #endregion
}

public interface SpaceInfo {
    public int getPlayerOrderIndex(PlayerInfo playerInfo);
    public int NumberOfPlayersOnSpace { get; }
    public IEnumerable<PlayerInfo> VisitingPlayers { get; }
    public int Index { get; }
}
