using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New EstateGroup", menuName = "PropertyGroups/EstateGroup")]
internal class EstateGroup : ScriptableObject, EstateGroupInfo {
    [SerializeField] private int groupID;
    [SerializeField] private Estate[] estates;
    [SerializeField] private Color estateColour;
    [SerializeField] private Color highlightColour;



    #region EstateGroupInfo
    public int GroupID => groupID;
    public Color EstateColour => estateColour;
    public Color HighlightColour => highlightColour;
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
