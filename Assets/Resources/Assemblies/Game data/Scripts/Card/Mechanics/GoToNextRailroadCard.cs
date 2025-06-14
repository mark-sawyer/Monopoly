using UnityEngine;

[CreateAssetMenu(fileName = "GoToNextRailroadCard", menuName = "Card/Mechanic/GoToNextRailroadCard")]
internal class GoToNextRailroadCard : CardMechanic, GoToNextRailroadCardInfo { }
public interface GoToNextRailroadCardInfo : CardMechanicInfo { }
