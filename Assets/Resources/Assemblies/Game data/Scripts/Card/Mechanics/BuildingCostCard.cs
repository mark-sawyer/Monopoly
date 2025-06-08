using UnityEngine;

[CreateAssetMenu(fileName = "BuildingCostCard", menuName = "Card/Mechanic/BuildingCostCard")]
internal class BuildingCostCard : CardMechanic, BuildingCostCardInfo {
    [SerializeField] private int houseCost;
    [SerializeField] private int hotelCost;

    internal override void execute() {

    }
}

public interface BuildingCostCardInfo : CardMechanicInfo { }
