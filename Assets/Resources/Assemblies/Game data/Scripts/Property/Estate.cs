using UnityEngine;

[CreateAssetMenu(fileName = "New estate", menuName = "Estate")]
internal class Estate : Property, EstateInfo {
    [SerializeField] private EstateGroup estateGroup;
    [SerializeField] private int[] rent;
    [SerializeField] private int mortgage;
    [SerializeField] private int buildingCost;



    #region EstateInfo
    public Color EstateColour { get => estateGroup.EstateColour; }
    public int BuildingCost { get => buildingCost; }
    public int getRent(int index) {
        return rent[index];
    }
    #endregion
}
