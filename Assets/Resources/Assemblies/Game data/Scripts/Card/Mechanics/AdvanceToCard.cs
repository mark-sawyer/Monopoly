using UnityEngine;

[CreateAssetMenu(fileName = "AdvanceToCard", menuName = "Card/Mechanic/AdvanceToCard")]
internal class AdvanceToCard : CardMechanic, AdvanceToCardInfo {
    [SerializeField] private Space destination;

    internal override void execute() {
        Player player = (Player)Game.TurnPlayer;
        Space oldSpace = player.Space;
        player.changeSpace(destination);
        int oldIndex = oldSpace.Index;
        int newIndex = destination.Index;
        bool passedGo = newIndex < oldIndex;
        if (passedGo) player.adjustMoney(GameConstants.MONEY_FOR_PASSING_GO);
    }
    public SpaceInfo Destination => destination;
}

public interface AdvanceToCardInfo : CardMechanicInfo {
    public SpaceInfo Destination { get; }
}
