using System;
using System.Collections.Generic;

public class Space {
    private List<Player> playersVisiting = new List<Player>();
    private Property property;

    public void addPlayer(Player player) {
        playersVisiting.Add(player);
    }
    public void removePlayer(Player player) {
        playersVisiting.Remove(player);
    }
    public bool containsPlayer(Player player) {
        return playersVisiting.Contains(player);
    }
    public void setProperty(Property property) {
        if (property is null) this.property = property;
        else throw new InvalidOperationException("Property already set.");
    }
}
