using System.Collections.Generic;
using UnityEngine;

internal class Estate : Property, EstateInfo {
    [SerializeField] private EstateGroup estateGroup;
    [SerializeField] private int[] rent;
    [SerializeField] private int buildingCost;
    private Queue<Building> buildings = new Queue<Building>(4);



    #region EstateInfo
    public EstateGroupInfo EstateGroupInfo => estateGroup;
    public EstateColour EstateColour => estateGroup.EstateColour;
    public int BuildingCost => buildingCost;
    public int BuildingCount => buildings.Count;
    public int EstateOrder => estateGroup.getPropertyOrder(this);
    public bool canAddBuilding() {
        if (!ownerHasMonopoly()) return false;

        int buildingCount = buildings.Count;
        bool hasHotel = buildingCount == 1 && buildings.Peek() is Hotel;
        if (hasHotel) return false;

        bool groupHasHotel = estateGroup.HotelExists;
        if (groupHasHotel) return true;

        int minBuildingCount = estateGroup.MinBuildingCount;
        return buildingCount <= minBuildingCount;
    }
    public int getSpecificRent(int index) {
        return rent[index];
    }
    #endregion



    #region Property
    internal override int getRent() {
        if (Owner == null) throw new System.Exception("Property is unowned.");

        if (buildings.Count == 0 && ownerHasMonopoly()) return 2 * rent[0];
        if (buildings.Count == 0) return rent[0];
        if (buildings.Peek() is Hotel) return rent[5];
        return rent[buildings.Count];        
    }
    internal override int getWorth() {
        return Cost + (buildingCost * buildings.Count);
    }
    #endregion



    #region internal
    internal void addBuilding(Building building) {
        buildings.Enqueue(building);
    }
    internal Building removeBuilding() {
        return buildings.Dequeue();
    }
    internal bool hasHotel() {
        return buildings.Count == 1 && buildings.Peek() is Hotel;
    }
    #endregion



    #region private
    private bool ownerHasMonopoly() {
        bool hasMonopoly = true;
        for (int i = 0; i < estateGroup.NumberOfEstatesInGroup; i++) {
            EstateInfo estate = estateGroup.getEstateInfo(i);
            if (estate.Owner != Owner) {
                hasMonopoly = false;
                break;
            }
        }
        return hasMonopoly;
    }
    #endregion
}

public interface EstateInfo : PropertyInfo {
    public EstateGroupInfo EstateGroupInfo { get; }
    public EstateColour EstateColour { get; }
    public int BuildingCost { get; }
    public int BuildingCount { get; }
    public int EstateOrder { get; }
    public bool canAddBuilding();
    public int getSpecificRent(int index);
}
