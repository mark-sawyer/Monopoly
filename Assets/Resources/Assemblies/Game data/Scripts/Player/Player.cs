using UnityEngine;
using System.Collections.Generic;
using System.Linq;

internal class Player : PlayerInfo {
    private Game game;
    private Space space;
    private List<Property> properties;
    private Debt debt;
    private int money;
    private bool inJail;
    private bool isActive;
    private int turnInJail;
    private List<Card> getOutOfJailFreeCards;
    private Token token;
    private PlayerColour colour;



    #region internal
    internal Player(Space space, Token token, PlayerColour colour, int startingMoney, Game game) {
        this.space = space;
        this.token = token;
        this.colour = colour;
        this.game = game;
        money = startingMoney;


        properties = new List<Property>();
        inJail = false;
        isActive = true;
        turnInJail = 0;
        getOutOfJailFreeCards = new(2);
    }
    internal Space Space => space;
    internal Debt Debt => debt;
    internal void obtainProperty(Property property) {
        properties.Add(property);
        property.changeOwner(this);
    }
    internal void removeProperty(Property property) {
        properties.Remove(property);
        property.changeOwner(null);
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
        space = newSpace;
    }
    internal void incurDebt(Creditor creditor, int owed) {
        debt = new Debt(this, creditor, owed);
    }
    internal void payDebt(int paid) {
        debt.pay(paid);
        money -= paid;
        if (debt.Owed == 0) {
            debt = null;
        }
    }
    internal void getGOOJFCard(Card getOutOfJailFreeCard) {
        getOutOfJailFreeCards.Add(getOutOfJailFreeCard);
    }
    internal void incrementJailTurn() {
        turnInJail++;
    }
    internal Card handBackGOOJFCard(CardType cardType) {
        Card getOutOfJailFreeCard = getOutOfJailFreeCards.First(x => x.CardType == cardType);
        getOutOfJailFreeCards.Remove(getOutOfJailFreeCard);
        return getOutOfJailFreeCard;
    }
    internal void eliminateSelf() {
        isActive = false;
        space.removePlayer(this);
        space = null;
        inJail = false;
    }
    #endregion



    #region PlayerInfo
    public int Index => game.getPlayerIndex(this);  
    public int SpaceIndex { get => Space.Index; }
    public SpaceInfo SpaceInfo { get => Space; }
    public Token Token { get => token; }
    public PlayerColour Colour { get => colour; }
    public int Money => money;
    public DebtInfo DebtInfo => debt;
    public int TotalWorth => money + properties.Sum(x => x.Worth);
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
    public int HousesOwned {
        get {
            IEnumerable<Estate> estates = properties.OfType<Estate>();
            int houseCount = estates.Sum(x => {
                if (x.HasHotel) return 0;
                else return x.BuildingCount;
            });
            return houseCount;
        }
    }
    public int HotelsOwned {
        get {
            IEnumerable<Estate> estates = properties.OfType<Estate>();
            int hotelCount = estates.Count(x => x.HasHotel);
            return hotelCount;
        }
    }
    public IEnumerable<TradableInfo> TradableInfos {
        get {
            List<Tradable> tradables = new();
            foreach (Property property in properties) {
                if (property.IsCurrentlyTradable) {
                    tradables.Add(property);
                }
            }
            foreach (Card cardInstance in getOutOfJailFreeCards) {
                tradables.Add(cardInstance);
            }
            tradables.Sort((a, b) => a.TradableOrderID.CompareTo(b.TradableOrderID));
            return tradables;
        }
    }
    public bool CanRaiseMoney => properties.Any(x => !x.IsMortgaged);
    public bool hasGOOJFCardOfType(CardType cardType) {
        return getOutOfJailFreeCards.Any(x => x.CardType == cardType);
    }
    public bool ownsProperty(PropertyInfo propertyInfo) {
        Property property = (Property)propertyInfo;
        return properties.Contains(property);
    }
    #endregion
}
