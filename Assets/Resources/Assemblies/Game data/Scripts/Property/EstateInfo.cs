using UnityEngine;

public interface EstateInfo : PropertyInfo {
    public Color EstateColour { get; }
    public int BuildingCost { get; }
    public int getRent(int index);
}
