using UnityEngine;

[CreateAssetMenu(fileName = "BuildingCostCard", menuName = "Card/Mechanic/BuildingCostCard")]
internal class BuildingCostCard : CardMechanic, BuildingCostCardInfo {
    [SerializeField] private int houseCost;
    [SerializeField] private int hotelCost;
    public int HouseCost => houseCost;
    public int HotelCost => hotelCost;
}

public interface BuildingCostCardInfo : CardMechanicInfo {
    public int HouseCost { get; }
    public int HotelCost { get; }
}
