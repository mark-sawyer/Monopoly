using System.Collections.Generic;
using UnityEngine;

internal abstract class Space : ScriptableObject, SpaceInfo {
    private Game game;
    private List<Player> playersVisiting;



    #region SpaceInfo
    public IEnumerable<PlayerInfo> getVisitingPlayers() {
        return playersVisiting;
    }
    public int getNumberOfPlayersOnSpace() {
        return playersVisiting.Count;
    }
    public int getIndex() {
        return game.getSpaceIndex(this);
    }
    public int getPlayerOrderIndex(PlayerInfo playerInfo) {
        return playersVisiting.IndexOf((Player)playerInfo);
    }
    #endregion


    #region public
    public void setupSpace(Game game) {
        this.game = game;
        playersVisiting = new List<Player>();
    }
    public void addPlayer(Player player) {
        playersVisiting.Add(player);
    }
    public void removePlayer(Player player) {
        playersVisiting.Remove(player);
    }
    public bool containsPlayer(Player player) {
        return playersVisiting.Contains(player);
    }
    #endregion
}
