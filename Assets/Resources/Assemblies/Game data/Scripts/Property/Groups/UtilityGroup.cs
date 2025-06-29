using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New UtilityGroup", menuName = "PropertyGroups/UtilityGroup")]
internal class UtilityGroup : ScriptableObject, UtilityGroupInfo {
    [SerializeField] private Utility[] utilities;
    private Utility ElectricCompany => utilities[0];
    private Utility WaterWorks => utilities[1];



    #region UtilityGroupInfo
    public UtilityInfo getUtilityInfo(int index) {
        return utilities[index];
    }
    public int utilitiesOwnedByPlayer(PlayerInfo player) {
        return utilities.Count(x => x.Owner == player);
    }
    public bool playerOwnsUtility(PlayerInfo player, UtilityType utilityType) {
        Utility utility = utilityType == UtilityType.ELECTRICITY ? ElectricCompany : WaterWorks;
        return utility.Owner == player;
    }
    #endregion
}

public interface UtilityGroupInfo {
    public UtilityInfo getUtilityInfo(int index);
    public int utilitiesOwnedByPlayer(PlayerInfo player);
    public bool playerOwnsUtility(PlayerInfo player, UtilityType utilityType);
}
