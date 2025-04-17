
using System;

public class EstateGroup {
    private Property[] properties;
    private EstateColour estateColour;

    public EstateGroup(EstateColour estateColour) {
        this.estateColour = estateColour;
    }

    public void setProperties(Property[] properties) {
        if (properties is null) this.properties = properties;
        else throw new InvalidOperationException("Properties already set.");
    }
}
