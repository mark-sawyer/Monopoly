using UnityEngine;

[CreateAssetMenu(fileName = "AdvanceToCard", menuName = "Card/Mechanic/AdvanceToCard")]
internal class AdvanceToCard : CardMechanic, AdvanceToCardInfo {
    [SerializeField] private Space destination;

    internal override void execute() {
        Player player = (Player)Game.TurnPlayer;
        Space oldSpace = player.Space;
        player.changeSpace(destination);
    }
    public SpaceInfo Destination => destination;
}

public interface AdvanceToCardInfo : CardMechanicInfo {
    public SpaceInfo Destination { get; }
}
