using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New estate", menuName = "Estate")]
public class Estate : Property {
    private EstateGroup estateGroup;
    [SerializeField] private int[] rent;
    [SerializeField] private int mortgage;
    [SerializeField] private int buildingCost;

    internal void assignEstateGroup(EstateGroup estateGroupBeingAssigned) {
        if (estateGroup is null) estateGroup = estateGroupBeingAssigned;
        else throw new InvalidOperationException("EstateGroup already assigned");
    }



    /* Public interface */
    public EstateColour getEstateColour() {
        return estateGroup.estateColour;
    }
}
