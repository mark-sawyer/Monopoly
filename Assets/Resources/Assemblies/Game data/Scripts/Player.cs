using System.Collections.Generic;

internal class Player : PlayerInfo {
    internal Space space { get; set; }
    private List<Property> properties = new List<Property>();
    private int money = 1500;
    private Token token;

    internal Player(Space space, Token token) {
        this.space = space;
        this.token = token;
    }
    private void buyProperty(Property property) {
        money -= property.cost;
        properties.Add(property);
        property.changeOwner(this);
    }



    /* Public interface */
    public int getSpaceIndex() {
        return space.getIndex();
    }
    public Token getToken() {
        return token;
    }
}
