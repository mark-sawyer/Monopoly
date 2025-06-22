using UnityEngine;
using System.Collections.Generic;
using System.Linq;

internal class Player : PlayerInfo {
    private List<Property> properties = new List<Property>();
    private Debt debt;
    private int money;
    private bool inJail = false;
    private bool isActive = true;
    private int turnInJail = 0;
    private List<CardInstance> getOutOfJailFreeCards = new(2);
    private Token token;
    private PlayerColour colour;



    #region internal
    internal Player(Space space, Token token, PlayerColour colour, int startingMoney) {
        Space = space;
        money = startingMoney;
        this.token = token;
        this.colour = colour;
    }
    internal Space Space { get; set; }
    internal void obtainProperty(Property property) {
        properties.Add(property);
        property.changeOwner(this);
    }
    internal void adjustMoney(int difference) {
        money += difference;
    }
    internal void exitJail() {
        inJail = false;
        turnInJail = 0;
    }
    internal void changeSpace(Space newSpace) {
        Space.removePlayer(this);
        newSpace.addPlayer(this);
        Space = newSpace;
    }
    internal void incurDebt(Creditor creditor, int owed) {
        debt = new Debt(this, creditor, owed);
    }
    internal void removeDebt() {
        debt = null;
    }
    internal void getGOOJFCard(CardInstance getOutOfJailFreeCard) {
        getOutOfJailFreeCards.Add(getOutOfJailFreeCard);
    }
    internal void incrementJailTurn() {
        turnInJail++;
    }
    internal CardInstance handBackGOOJFCard(CardType cardType) {
        CardInstance getOutOfJailFreeCard = getOutOfJailFreeCards.First(x => x.CardType == cardType);
        getOutOfJailFreeCards.Remove(getOutOfJailFreeCard);
        return getOutOfJailFreeCard;
    }
    #endregion



    #region PlayerInfo
    public int SpaceIndex { get => Space.Index; }
    public SpaceInfo SpaceInfo { get => Space; }
    public Token Token { get => token; }
    public PlayerColour Colour { get => colour; }
    public int Money => money;
    public DebtInfo Debt => debt;
    public int TotalWorth => money + properties.Sum(x => x.getWorth());
    public int IncomeTaxAmount {
        get {
            float tenPercent = TotalWorth * 0.1f;
            int rounded = Mathf.RoundToInt(tenPercent + 0.001f);
            return rounded;
        }
    }
    public bool IsActive { get => isActive; internal set => isActive = value; }
    public bool InJail { get => inJail; internal set => inJail = value; }
    public int TurnInJail => turnInJail;
    public bool HasGOOJFCard => getOutOfJailFreeCards.Count > 0;
    public bool hasGOOJFCardOfType(CardType cardType) {
        return getOutOfJailFreeCards.Any(x => x.CardType == cardType);
    }
    #endregion
}
