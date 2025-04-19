using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New estate", menuName = "Estate")]
internal class Estate : Property, EstateVisualDataGetter {
    private EstateGroup estateGroup;

    public void assignEstateGroup(EstateGroup estateGroupBeingAssigned) {
        if (estateGroup is null) estateGroup = estateGroupBeingAssigned;
        else throw new InvalidOperationException("EstateGroup already assigned");
    }



    /* EstateVisualDataGetter */
    public EstateColour getEstateColour() {
        return estateGroup.estateColour;
    }
}
