using UnityEngine;
using System.Collections.Generic;
using System.Linq;

internal class Player : PlayerInfo {
    private Game game;
    private List<Property> properties = new List<Property>();
    private Debt debt;
    private int money;
    private bool inJail = false;
    private bool isActive = true;
    private int turnInJail = 0;
    private List<Card> getOutOfJailFreeCards = new(2);
    private Token token;
    private PlayerColour colour;



    #region internal
    internal Player(Space space, Token token, PlayerColour colour, int startingMoney, Game game) {
        Space = space;
        money = startingMoney;
        this.token = token;
        this.colour = colour;
        this.game = game;
    }
    internal Space Space { get; set; }
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
        Space = newSpace;
    }
    internal void incurDebt(Creditor creditor, int owed) {
        debt = new Debt(this, creditor, owed);
    }
    internal void removeDebt() {
        debt = null;
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
    #endregion



    #region PlayerInfo
    public int Index => game.getPlayerIndex(this);  
    public int SpaceIndex { get => Space.Index; }
    public SpaceInfo SpaceInfo { get => Space; }
    public Token Token { get => token; }
    public PlayerColour Colour { get => colour; }
    public int Money => money;
    public DebtInfo Debt => debt;
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
    public bool hasGOOJFCardOfType(CardType cardType) {
        return getOutOfJailFreeCards.Any(x => x.CardType == cardType);
    }
    public bool ownsProperty(PropertyInfo propertyInfo) {
        Property property = (Property)propertyInfo;
        return properties.Contains(property);
    }
    #endregion
}
