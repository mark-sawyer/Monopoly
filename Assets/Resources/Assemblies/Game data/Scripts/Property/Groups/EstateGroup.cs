using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New EstateGroup", menuName = "PropertyGroups/EstateGroup")]
internal class EstateGroup : ScriptableObject, EstateGroupInfo {
    [SerializeField] private EstateColour estateColour;
    [SerializeField] private Estate[] estates;



    #region PropertyGroupInfo
    public int NumberOfPropertiesInGroup => estates.Length;
    public bool MortgageExists => estates.Any(x => x.IsMortgaged);
    public int MortgageCount => estates.Count(x => x.IsMortgaged);
    public int propertiesOwnedByPlayer(PlayerInfo playerInfo) {
        return estates.Count(x => x.Owner == playerInfo);
    }
    public bool playerHasMortgageInGroup(PlayerInfo playerInfo) {
        return estates.Any(x => x.Owner == playerInfo && x.IsMortgaged);
    }
    #endregion



    #region EstateGroupInfo
    public EstateColour EstateColour => estateColour;
    public int MinBuildingCount => estates.Min(x => x.BuildingCount);
    public int MaxBuildingCount => estates.Max(x => x.BuildingCount);
    public bool BuildingExists => estates.Any(x => x.BuildingCount > 0);
    public bool HotelExists => estates.Any(x => x.HasHotel);
    public EstateInfo getEstateInfo(int index) {
        return estates[index];
    }
    #endregion



    #region internal
    internal int getPropertyOrder(Estate estate) {
        return Array.IndexOf(estates, estate);
    }
    #endregion
}

public interface EstateGroupInfo : PropertyGroupInfo {
    public EstateColour EstateColour { get; }
    public int MinBuildingCount { get; }
    public int MaxBuildingCount { get; }
    public bool BuildingExists { get; }
    public bool HotelExists { get; }
    public EstateInfo getEstateInfo(int index);
}
