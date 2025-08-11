using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New UtilityGroup", menuName = "PropertyGroups/UtilityGroup")]
internal class UtilityGroup : ScriptableObject, UtilityGroupInfo {
    [SerializeField] private Utility[] utilities;
    private Utility ElectricCompany => utilities[0];
    private Utility WaterWorks => utilities[1];



    #region PropertyGroupInfo
    public int NumberOfPropertiesInGroup => 2;
    public bool MortgageExists => utilities.Any(x => x.IsMortgaged);
    public int propertiesOwnedByPlayer(PlayerInfo playerInfo) {
        return utilities.Count(x => x.Owner == playerInfo);
    }
    public int propertiesMortgagedByPlayer(PlayerInfo playerInfo) {
        return utilities.Count(x => x.Owner == playerInfo && x.IsMortgaged);
    }
    public bool playerHasMortgageInGroup(PlayerInfo playerInfo) {
        return utilities.Any(x => x.Owner == playerInfo && x.IsMortgaged);
    }
    public PropertyInfo getPropertyInfo(int index) {
        return utilities[index];
    }
    #endregion



    #region UtilityGroupInfo
    public UtilityInfo getUtilityInfo(int index) {
        return utilities[index];
    }
    public bool playerOwnsUtility(PlayerInfo player, UtilityType utilityType) {
        Utility utility = utilityType == UtilityType.ELECTRICITY ? ElectricCompany : WaterWorks;
        return utility.Owner == player;
    }
    #endregion
}

public interface UtilityGroupInfo : PropertyGroupInfo {
    public UtilityInfo getUtilityInfo(int index);
    public bool playerOwnsUtility(PlayerInfo player, UtilityType utilityType);
}
