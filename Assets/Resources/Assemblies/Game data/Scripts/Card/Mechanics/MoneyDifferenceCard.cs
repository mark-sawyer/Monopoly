using UnityEngine;

[CreateAssetMenu(fileName = "MoneyDifferenceCard", menuName = "Card/Mechanic/MoneyDifferenceCard")]
internal class MoneyDifferenceCard : CardMechanic, MoneyDifferenceCardInfo {
    [SerializeField] private int addedToPlayer;

    internal override void execute() {
        
    }
}

public interface MoneyDifferenceCardInfo : CardMechanicInfo { }
