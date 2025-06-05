using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New JailSpace", menuName = "Space/JailSpace")]
internal class JailSpace : Space, JailSpaceInfo {
    public int NumberOfPlayersInJail => VisitingPlayers.Count(x => x.InJail);
    public int NumberOfVisitingPlayers => VisitingPlayers.Count(x => !x.InJail);
    public int getJailOrderIndex(PlayerInfo playerInfo) {
        int index = 0;
        foreach (PlayerInfo visitingPlayer in VisitingPlayers) {
            if (!visitingPlayer.InJail) continue;
            if (visitingPlayer != playerInfo) index++;
            else break;
        }
        return index;
    }
    public int getVisitingOrderIndex(PlayerInfo playerInfo) {
        int index = 0;
        foreach (PlayerInfo visitingPlayer in VisitingPlayers) {
            if (visitingPlayer.InJail) continue;
            if (visitingPlayer != playerInfo) index++;
            else break;
        }
        return index;
    }
}

public interface JailSpaceInfo : SpaceInfo {
    public int NumberOfPlayersInJail { get; }
    public int NumberOfVisitingPlayers { get; }
    public int getJailOrderIndex(PlayerInfo playerInfo);
    public int getVisitingOrderIndex(PlayerInfo playerInfo);
}
