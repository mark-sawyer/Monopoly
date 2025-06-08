using UnityEngine;

[CreateAssetMenu(fileName = "GoToJailCard", menuName = "Card/Mechanic/GoToJailCard")]
internal class GoToJailCard : CardMechanic, GoToJailCardInfo {
    internal override void execute() {

    }
}

public interface GoToJailCardInfo : CardMechanicInfo { }
