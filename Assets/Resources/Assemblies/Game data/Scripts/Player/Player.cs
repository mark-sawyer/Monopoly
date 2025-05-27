using UnityEngine;
using System.Collections.Generic;
using System.Linq;

internal class Player : PlayerInfo {
    internal Space space { get; set; }
    private List<Property> properties = new List<Property>();
    private int money = 1500;
    private Token token;
    private PlayerColour colour;



    #region internal
    internal Player(Space space, Token token, PlayerColour colour) {
        this.space = space;
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
    #endregion



    #region PlayerInfo
    public int SpaceIndex { get => space.Index; }
    public SpaceInfo SpaceInfo { get => space; }
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
    #endregion
}
