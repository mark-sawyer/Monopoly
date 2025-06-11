using UnityEngine;

[CreateAssetMenu(fileName = "New Utility", menuName = "Utility")]
internal class Utility : Property, UtilityInfo {
    [SerializeField] private UtilityType utilityType;
    [SerializeField] private UtilityGroup utilityGroup;
    private DiceInterface dice;

    internal override int getRent() {
        PlayerInfo owner = Owner;
        int utilitiesOwnedByPlayer = utilityGroup.utilitiesOwnedByPlayer(owner);
        if (utilitiesOwnedByPlayer == 1) return dice.TotalValue * 4;
        else return dice.TotalValue * 10;
    }
    internal void setup(DiceInterface dice) {
        this.dice = dice;
    }
}

public interface UtilityInfo : PropertyInfo { }
