using System.Collections.Generic;
using UnityEngine;

internal class Estate : Property, EstateInfo {
    [SerializeField] private EstateGroup estateGroup;
    [SerializeField] private int[] rent;
    [SerializeField] private int buildingCost;
    private Queue<Building> buildings = new Queue<Building>(4);
    private BankInfo bankInfo;



    #region internal
    internal void setup(BankInfo bankInfo) {
        this.bankInfo = bankInfo;
    }
    internal void addBuilding(Building building) {
        buildings.Enqueue(building);
    }
    internal Building removeBuilding() {
        return buildings.Dequeue();
    }
    #endregion



    #region EstateInfo
    public EstateGroupInfo EstateGroupInfo => estateGroup;
    public EstateColour EstateColour => estateGroup.EstateColour;
    public int BuildingCost => buildingCost;
    public int BuildingSellCost => buildingCost / 2;
    public int BuildingCount => buildings.Count;
    public int EstateOrder => estateGroup.getPropertyOrder(this);
    public bool CanAddBuilding {
        get {
            if (!ownerHasMonopoly()) return false;

            // Owner has a monopoly.
            bool mortgageExists = estateGroup.MortgageExists;
            if (mortgageExists) return false;

            // None of the estates are mortgaged.
            int buildingCount = buildings.Count;
            bool hasHotel = buildingCount == 1 && buildings.Peek() is Hotel;
            if (hasHotel) return false;

            // This estate does not have a hotel.
            bool groupHasHotel = estateGroup.HotelExists;
            bool bankHasHotel = bankInfo.HotelsRemaining > 0;
            if (groupHasHotel) return bankHasHotel;

            // No estate in the group has a hotel.
            int minBuildingCount = estateGroup.MinBuildingCount;
            bool canBuildingSomething = buildingCount <= minBuildingCount;
            if (!canBuildingSomething) return false;

            // The estate could build a house or hotel, if they are available.
            bool housesComplete = minBuildingCount == 4;
            if (housesComplete) return bankHasHotel;

            // The estate can build a house, if available.
            bool bankHasHouse = bankInfo.HousesRemaining > 0;
            return bankHasHouse;
        }
    }
    public bool CanRemoveBuilding {
        get {
            int buildingCount = buildings.Count;
            if (buildingCount == 0) return false;

            // There is a building on the estate.
            bool isHotel = buildings.Peek() is Hotel;
            int housesLeft = bankInfo.HousesRemaining;
            if (isHotel) return housesLeft >= 4;

            // It's not a hotel.
            bool hotelExists = estateGroup.HotelExists;
            if (hotelExists) return false;

            // There are no hotels in the group.
            int maxBuildingCount = estateGroup.MaxBuildingCount;
            return buildingCount == maxBuildingCount;
        }
    }
    public bool HasHotel => buildings.Count == 1 && buildings.Peek() is Hotel;
    public override int Rent {
        get {
            if (Owner == null) throw new System.Exception("Property is unowned.");

            if (buildings.Count == 0 && ownerHasMonopoly()) return 2 * rent[0];
            if (buildings.Count == 0) return rent[0];
            if (buildings.Peek() is Hotel) return rent[5];
            return rent[buildings.Count];
        }
    }
    public int getSpecificRent(int index) {
        return rent[index];
    }
    #endregion



    #region Property
    internal override int Worth => Cost + (buildingCost * buildings.Count);
    internal override bool IsCurrentlyTradable {
        get {
            bool noBuildings = !estateGroup.BuildingExists;
            return noBuildings;
        }
    }
    #endregion



    #region private
    private bool ownerHasMonopoly() {
        bool hasMonopoly = true;
        for (int i = 0; i < estateGroup.NumberOfPropertiesInGroup; i++) {
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
    public int BuildingSellCost { get; }
    public int BuildingCount { get; }
    public int EstateOrder { get; }
    public bool CanAddBuilding { get; }
    public bool CanRemoveBuilding { get; }
    public bool HasHotel { get; }
    public int getSpecificRent(int index);    
}
