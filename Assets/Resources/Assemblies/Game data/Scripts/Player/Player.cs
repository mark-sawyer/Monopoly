using UnityEngine;
using System.Collections.Generic;
using System.Linq;

internal class Player : PlayerInfo {
    internal static Space jailSpace;
    private List<Property> properties = new List<Property>();
    private int money = 1500;
    private Token token;
    private PlayerColour colour;
    private bool inJail = false;



    #region internal
    internal Space Space { get; set; }
    internal Player(Space space, Token token, PlayerColour colour) {
        Space = space;
        this.token = token;
        this.colour = colour;
    }
    internal void obtainProperty(Property property) {
        properties.Add(property);
        property.changeOwner(this);
    }
    internal void adjustMoney(int difference) {
        money += difference;
    }
    internal void goToJail() {
        Space.removePlayer(this);
        Space = jailSpace;
        Space.addPlayer(this);
        inJail = true;
    }
    internal void exitJail() {
        inJail = false;
    }
    internal void changeSpace(Space newSpace) {
        Space.removePlayer(this);
        newSpace.addPlayer(this);
        Space = newSpace;
    }
    #endregion



    #region PlayerInfo
    public int SpaceIndex { get => Space.Index; }
    public SpaceInfo SpaceInfo { get => Space; }
    public Token Token { get => token; }
    public PlayerColour Colour { get => colour; }
    public int Money => money;
    public int IncomeTaxAmount {
        get {
            int totalWorth = money + properties.Sum(x => x.getWorth());
            float tenPercent = totalWorth * 0.1f;
            int rounded = Mathf.RoundToInt(tenPercent + 0.001f);
            return rounded;
        }
    }
    public bool InJail => inJail;
    #endregion
}
