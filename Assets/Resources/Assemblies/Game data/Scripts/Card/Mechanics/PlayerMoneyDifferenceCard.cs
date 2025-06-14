using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoneyDifferenceCard", menuName = "Card/Mechanic/PlayerMoneyDifferenceCard")]
internal class PlayerMoneyDifferenceCard : CardMechanic, PlayerMoneyDifferenceCardInfo {
    [SerializeField] private int subtractedFromOtherPlayers;
    public int SubtractedFromOtherPlayers => subtractedFromOtherPlayers;
}
public interface PlayerMoneyDifferenceCardInfo : CardMechanicInfo {
    public int SubtractedFromOtherPlayers { get; }
}
