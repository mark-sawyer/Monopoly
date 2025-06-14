using UnityEngine;

[CreateAssetMenu(fileName = "MoneyDifferenceCard", menuName = "Card/Mechanic/MoneyDifferenceCard")]
internal class MoneyDifferenceCard : CardMechanic, MoneyDifferenceCardInfo {
    [SerializeField] private int addedToPlayer;
    public int AddedToPlayer => addedToPlayer;
}

public interface MoneyDifferenceCardInfo : CardMechanicInfo {
    public int AddedToPlayer { get; }
}
