using UnityEngine;

[CreateAssetMenu(fileName = "GetOutOfJailFreeCard", menuName = "Card/Mechanic/GetOutOfJailFreeCard")]
internal class GetOutOfJailFreeCard : CardMechanic, GetOutOfJailFreeCardInfo { }
public interface GetOutOfJailFreeCardInfo : CardMechanicInfo { }
