using UnityEngine;

internal abstract class Property: ScriptableObject, PropertyInfo {
    [SerializeField] private string propertyName;
    [SerializeField] private PropertySpace space;
    [SerializeField] private int cost;
    [SerializeField] private int mortgageValue;
    private Player owner;
    private bool isMortgaged;



    #region virtual
    internal abstract int getRent();
    internal virtual int getWorth() {
        return cost;
    }
    #endregion



    #region internal
    internal void changeOwner(Player player) {
        owner = player;
    }
    internal void mortgage() {
        isMortgaged = true;
    }
    internal void unmortgage() {
        isMortgaged = false;
    }
    #endregion



    #region PropertyInfo
    public int Cost => cost;
    public string Name => propertyName;
    public int MortgageValue => mortgageValue;
    public int UnmortgageCost => Mathf.RoundToInt((1.1f * mortgageValue) + 0.001f);
    public int RetainMortgageCost => Mathf.RoundToInt((0.1f * mortgageValue) + 0.001f);
    public bool IsMortgaged => isMortgaged;
    public bool IsBought => owner != null;
    public PlayerInfo Owner => owner;
    public int Rent => getRent();
    #endregion
}

public interface PropertyInfo {
    public int Cost { get; }
    public string Name { get; }
    public int MortgageValue { get; }
    public int UnmortgageCost { get; }
    public int RetainMortgageCost { get; }
    public bool IsMortgaged { get; }
    public bool IsBought { get; }
    public PlayerInfo Owner { get; }
    public int Rent { get; }
}
