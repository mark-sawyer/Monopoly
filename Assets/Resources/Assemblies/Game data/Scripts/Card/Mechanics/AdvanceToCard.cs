using UnityEngine;

[CreateAssetMenu(fileName = "AdvanceToCard", menuName = "Card/Mechanic/AdvanceToCard")]
internal class AdvanceToCard : CardMechanic, AdvanceToCardInfo {
    [SerializeField] private Space destination;
    public SpaceInfo Destination => destination;
}

public interface AdvanceToCardInfo : CardMechanicInfo {
    public SpaceInfo Destination { get; }
}
