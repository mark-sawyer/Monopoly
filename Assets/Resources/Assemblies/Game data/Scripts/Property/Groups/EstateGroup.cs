using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New EstateGroup", menuName = "PropertyGroups/EstateGroup")]
internal class EstateGroup : ScriptableObject, EstateGroupInfo {
    [SerializeField] private EstateColour estateColour;
    [SerializeField] private Estate[] estates;



    #region EstateGroupInfo
    public EstateColour EstateColour => estateColour;
    public int NumberOfEstatesInGroup => estates.Length;
    public int MinBuildingCount => estates.Min(x => x.BuildingCount);
    public int MaxBuildingCount => estates.Max(x => x.BuildingCount);
    public bool HotelExists => estates.Any(x => x.hasHotel());
    public EstateInfo getEstateInfo(int index) {
        return estates[index];
    }
    public int propertiesOwnedByPlayer(PlayerInfo player) {
        return estates.Count(x => x.Owner == player);
    }
    #endregion



    #region internal
    internal int getPropertyOrder(Estate estate) {
        return Array.IndexOf(estates, estate);
    }
    #endregion
}

public interface EstateGroupInfo {
    public EstateColour EstateColour { get; }
    public int NumberOfEstatesInGroup { get; }
    public int MinBuildingCount { get; }
    public int MaxBuildingCount { get; }
    public bool HotelExists { get; }
    public EstateInfo getEstateInfo(int index);
    public int propertiesOwnedByPlayer(PlayerInfo player);
}
