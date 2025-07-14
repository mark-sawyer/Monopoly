using UnityEngine;

[CreateAssetMenu(fileName = "New Utility", menuName = "Utility")]
internal class Utility : Property, UtilityInfo {
    [SerializeField] private UtilityType utilityType;
    [SerializeField] private UtilityGroup utilityGroup;
    private DiceInterface dice;



    #region internal
    internal override int getRent() {
        PlayerInfo owner = Owner;
        int utilitiesOwnedByPlayer = utilityGroup.propertiesOwnedByPlayer(owner);
        if (utilitiesOwnedByPlayer == 1) return dice.TotalValue * 4;
        else return dice.TotalValue * 10;
    }
    internal void setup(DiceInterface dice) {
        this.dice = dice;
    }
    #endregion



    #region UtilityInfo
    public UtilityType UtilityType => utilityType;
    #endregion
}

public interface UtilityInfo : PropertyInfo {
    UtilityType UtilityType { get; }
}
