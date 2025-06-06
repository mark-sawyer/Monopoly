using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoneyDifferenceCard", menuName = "Card/Mechanic/PlayerMoneyDifferenceCard")]
internal class PlayerMoneyDifferenceCard : CardMechanic {
    [SerializeField] private int subtractedFromOtherPlayers;

    internal override void execute() {
        
    }
}
