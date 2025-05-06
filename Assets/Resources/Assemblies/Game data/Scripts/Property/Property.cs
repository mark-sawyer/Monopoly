using UnityEngine;

internal abstract class Property: ScriptableObject, PropertyInfo {
    [SerializeField] private string propertyName;
    [SerializeField] private PropertySpace space;
    [SerializeField] private int cost;
    private Player owner;



    #region PropertyInfo
    public int Cost { get => cost; }
    public string Name { get => propertyName; }
    #endregion




    #region internal
    internal void changeOwner(Player player) {
        owner = player;
    }
    #endregion
}
