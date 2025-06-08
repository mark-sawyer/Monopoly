using UnityEngine;

[CreateAssetMenu(fileName = "GetOutOfJailFreeCard", menuName = "Card/Mechanic/GetOutOfJailFreeCard")]
internal class GetOutOfJailFreeCard : CardMechanic, GetOutOfJailFreeCardInfo {
    internal override void execute() {

    }
}

public interface GetOutOfJailFreeCardInfo : CardMechanicInfo { }
