using UnityEngine;

public interface EstateInfo : PropertyInfo {
    public EstateGroupInfo EstateGroupInfo { get; }
    public Color EstateColour { get; }
    public int BuildingCost { get; }
    public int BuildingCount { get; }
    public int EstateOrder { get; }
    public int Rent { get; }
    public bool canAddBuilding();
    public int getSpecificRent(int index);
}
