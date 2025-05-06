using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New estate", menuName = "Estate")]
internal class Estate : Property, EstateInfo {
    private EstateGroup estateGroup;
    [SerializeField] private int[] rent;
    [SerializeField] private int mortgage;
    [SerializeField] private int buildingCost;



    #region EstateInfo
    public EstateColour EstateColour { get => estateGroup.estateColour; }
    #endregion



    #region internal
    internal void assignEstateGroup(EstateGroup estateGroupBeingAssigned) {
        if (estateGroup is null) estateGroup = estateGroupBeingAssigned;
        else throw new InvalidOperationException("EstateGroup already assigned");
    }
    #endregion
}
