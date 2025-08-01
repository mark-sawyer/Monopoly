using UnityEngine;

[CreateAssetMenu(fileName = "New Utility", menuName = "Utility")]
internal class Utility : Property, UtilityInfo {
    [SerializeField] private UtilityType utilityType;
    [SerializeField] private UtilityGroup utilityGroup;
    private DiceInterface dice;



    #region internal
    internal void setup(DiceInterface dice) {
        this.dice = dice;
    }
    internal override bool IsCurrentlyTradable => true;
    #endregion



    #region UtilityInfo
    public UtilityType UtilityType => utilityType;
    public override int Rent {
        get {
            PlayerInfo owner = Owner;
            int utilitiesOwnedByPlayer = utilityGroup.propertiesOwnedByPlayer(owner);
            if (utilitiesOwnedByPlayer == 1) return dice.TotalValue * 4;
            else return dice.TotalValue * 10;
        }
    }
    #endregion
}

public interface UtilityInfo : PropertyInfo {
    UtilityType UtilityType { get; }
}
