using System.Collections.Generic;
using UnityEngine;

internal abstract class Space : ScriptableObject {
    private Game game;
    private List<Player> playersVisiting;

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
    public int getIndex() {
        return game.getSpaceIndex(this);
    }
}
