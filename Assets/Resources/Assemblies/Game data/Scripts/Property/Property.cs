using UnityEngine;

internal abstract class Property: ScriptableObject, PropertyInfo {
    [SerializeField] private string propertyName;
    [SerializeField] private string abbreviatedName;
    [SerializeField] private PropertySpace space;
    [SerializeField] private int cost;
    [SerializeField] private int mortgageValue;
    private Player owner;



    #region PropertyInfo
    public int Cost => cost;
    public string Name => propertyName;
    public string AbbreviatedName => abbreviatedName;
    public int MortgageValue => mortgageValue;
    #endregion




    #region internal
    internal void changeOwner(Player player) {
        owner = player;
    }
    #endregion
}
