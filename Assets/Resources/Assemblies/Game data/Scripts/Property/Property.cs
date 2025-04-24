using UnityEngine;

public abstract class Property: ScriptableObject {
    [SerializeField] private string propertyName;
    [SerializeField] private PropertySpace space;
    [SerializeField] public readonly int cost;
    private Player owner;

    internal void changeOwner(Player player) {
        owner = player;
    }
}
