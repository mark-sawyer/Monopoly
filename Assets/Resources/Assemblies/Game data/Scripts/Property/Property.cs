using UnityEngine;

internal abstract class Property : ScriptableObject, PropertyInfo, Tradable {
    [SerializeField] private string propertyName;
    [SerializeField] private PropertySpace space;
    [SerializeField] private int cost;
    [SerializeField] private int mortgageValue;
    [SerializeField] private string abbreviation;
    [SerializeField] private int orderID;
    private Player owner;
    private bool isMortgaged;



    #region internal
    internal abstract bool IsCurrentlyTradable { get; }
    internal virtual int Worth => cost;

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
    public abstract int Rent { get; }
    #endregion



    #region Tradable
    public int TradableOrderID => orderID;
    public string Abbreviation => abbreviation;
    public void giveFromOneToTwo(Player playerOne, Player playerTwo) {
        playerOne.removeProperty(this);
        playerTwo.obtainProperty(this);
    }
    #endregion
}

public interface PropertyInfo : TradableInfo {
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
