using UnityEngine;

[CreateAssetMenu(fileName = "GoToNextRailroadCard", menuName = "Card/Mechanic/GoToNextRailroadCard")]
internal class GoToNextRailroadCard : CardMechanic, GoToNextRailroadCardInfo {
    internal override void execute() {

    }
}

public interface GoToNextRailroadCardInfo : CardMechanicInfo { }
