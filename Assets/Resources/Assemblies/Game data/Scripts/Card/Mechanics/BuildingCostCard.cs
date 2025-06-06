using UnityEngine;

[CreateAssetMenu(fileName = "BuildingCostCard", menuName = "Card/Mechanic/BuildingCostCard")]
internal class BuildingCostCard : CardMechanic {
    [SerializeField] private int houseCost;
    [SerializeField] private int hotelCost;

    internal override void execute() {

    }
}
