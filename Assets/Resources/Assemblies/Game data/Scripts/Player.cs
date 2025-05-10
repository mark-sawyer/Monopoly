using System.Collections.Generic;

internal class Player : PlayerInfo {
    internal Space space { get; set; }
    private List<Property> properties = new List<Property>();
    private int money = 1500;
    private Token token;
    private PlayerColour colour;



    internal Player(Space space, Token token, PlayerColour colour) {
        this.space = space;
        this.token = token;
        this.colour = colour;
    }
    private void buyProperty(Property property) {
        money -= property.Cost;
        properties.Add(property);
        property.changeOwner(this);
    }



    #region PlayerInfo
    public int SpaceIndex { get => space.Index; }
    public SpaceInfo SpaceInfo { get => space; }
    public Token Token { get => token; }
    public PlayerColour Colour { get => colour; }
    #endregion
}
