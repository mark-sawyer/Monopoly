using UnityEngine;

public abstract class Property: ScriptableObject {
    [SerializeField] private string propertyName;
    [SerializeField] private PropertySpace space;
    [SerializeField] private int cost;
    private Player owner;


    internal int Cost { get => cost; }



    internal void changeOwner(Player player) {
        owner = player;
    }
}
