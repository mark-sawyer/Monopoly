
using System.Collections.Generic;

internal class Player : PlayerVisualDataGetter {
    public Space space { get; set; }
    private List<Property> properties = new List<Property>();
    private int money = 1500;

    public Player(Space space) {
        this.space = space;
    }
    private void buyProperty(Property property) {
        money -= property.cost;
        properties.Add(property);
        property.changeOwner(this);
    }



    /* PlayerVisualDataGetter */
    public float getPlayerPosition() {
        return space.getIndex();
    }
}
