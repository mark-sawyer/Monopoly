using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New RailroadGroup", menuName = "PropertyGroups/RailroadGroup")]
internal class RailroadGroup : ScriptableObject, RailroadGroupInfo {
    [SerializeField] private Railroad[] railroads;



    #region PropertyGroupInfo
    public int NumberOfPropertiesInGroup => 4;
    public bool MortgageExists => railroads.Any(x => x.IsMortgaged);
    public int MortgageCount => railroads.Count(x => x.IsMortgaged);
    public int propertiesOwnedByPlayer(PlayerInfo playerInfo) {
        return railroads.Count(x => x.Owner == playerInfo);
    }
    public int propertiesMortgagedByPlayer(PlayerInfo playerInfo) {
        return railroads.Count(x => x.Owner == playerInfo && x.IsMortgaged);
    }
    public bool playerHasMortgageInGroup(PlayerInfo playerInfo) {
        return railroads.Any(x => x.Owner == playerInfo && x.IsMortgaged);
    }
    public PropertyInfo getPropertyInfo(int index) {
        return railroads[index];
    }
    #endregion



    #region RailroadGroupInfo
    public RailroadInfo getRailroadInfo(int index) {
        return railroads[index];
    }
    public bool playerOwnsRailroad(PlayerInfo player, int index) {
        return railroads[index].Owner == player;
    }
    #endregion
}

public interface RailroadGroupInfo : PropertyGroupInfo {
    public RailroadInfo getRailroadInfo(int index);
    public bool playerOwnsRailroad(PlayerInfo player, int index);
}
