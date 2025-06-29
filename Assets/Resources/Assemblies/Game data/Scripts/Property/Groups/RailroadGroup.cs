using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New RailroadGroup", menuName = "PropertyGroups/RailroadGroup")]
internal class RailroadGroup : ScriptableObject, RailroadGroupInfo {
    [SerializeField] private Railroad[] railroads;

    #region RailroadGroupInfo
    public RailroadInfo getRailroadInfo(int index) {
        return railroads[index];
    }
    public int railroadsOwnedByPlayer(PlayerInfo player) {
        return railroads.Count(x => x.Owner == player);
    }
    public bool playerOwnsRailroad(PlayerInfo player, int index) {
        return railroads[index].Owner == player;
    }
    #endregion
}

public interface RailroadGroupInfo {
    public RailroadInfo getRailroadInfo(int index);
    public int railroadsOwnedByPlayer(PlayerInfo player);
    public bool playerOwnsRailroad(PlayerInfo player, int index);
}
