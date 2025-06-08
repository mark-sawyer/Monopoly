using UnityEngine;

[CreateAssetMenu(fileName = "GoToNextUtilityCard", menuName = "Card/Mechanic/GoToNextUtilityCard")]
internal class GoToNextUtilityCard : CardMechanic, GoToNextUtilityCardInfo {
    internal override void execute() {

    }
}

public interface GoToNextUtilityCardInfo : CardMechanicInfo { }
