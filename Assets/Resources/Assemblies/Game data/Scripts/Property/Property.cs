using UnityEngine;

internal abstract class Property: ScriptableObject, PropertyInfo {
    [SerializeField] private string propertyName;
    [SerializeField] private PropertySpace space;
    [SerializeField] private int cost;
    [SerializeField] private int mortgageValue;
    private Player owner;



    #region PropertyInfo
    public int Cost => cost;
    public string Name => propertyName;
    public int MortgageValue => mortgageValue;
    public bool IsBought => owner != null;
    #endregion




    #region internal
    internal void changeOwner(Player player) {
        owner = player;
    }
    #endregion
}
