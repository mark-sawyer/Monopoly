using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoneyDifferenceCard", menuName = "Card/Mechanic/PlayerMoneyDifferenceCard")]
internal class PlayerMoneyDifferenceCard : CardMechanic, PlayerMoneyDifferenceCardInfo {
    [SerializeField] private int subtractedFromOtherPlayers;

    internal override void execute() {
        
    }
}
public interface PlayerMoneyDifferenceCardInfo : CardMechanicInfo { }
