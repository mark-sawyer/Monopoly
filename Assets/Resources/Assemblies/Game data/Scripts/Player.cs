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



    #region PlayerInfo
    public int getSpaceIndex() {
        return space.getIndex();
    }
    public SpaceInfo getSpaceInfo() {
        return space;
    }
    public Token getToken() {
        return token;
    }
    #endregion
}
