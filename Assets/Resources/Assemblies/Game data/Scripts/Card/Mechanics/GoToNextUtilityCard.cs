using UnityEngine;

[CreateAssetMenu(fileName = "GoToNextUtilityCard", menuName = "Card/Mechanic/GoToNextUtilityCard")]
internal class GoToNextUtilityCard : CardMechanic, GoToNextUtilityCardInfo { }
public interface GoToNextUtilityCardInfo : CardMechanicInfo { }
