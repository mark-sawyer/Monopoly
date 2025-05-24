using UnityEngine;

public interface UtilityGroupInfo {
    public int propertiesOwnedByPlayer(PlayerInfo player);
    public bool playerOwnsUtility(PlayerInfo player, UtilityType utilityType);
}
