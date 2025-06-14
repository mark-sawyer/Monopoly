
using UnityEngine;

[CreateAssetMenu(fileName = "GoToJailCard", menuName = "Card/Mechanic/GoToJailCard")]
internal class GoToJailCard : CardMechanic, GoToJailCardInfo { }
public interface GoToJailCardInfo : CardMechanicInfo { }
