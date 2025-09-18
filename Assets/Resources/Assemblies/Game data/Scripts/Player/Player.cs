using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class Player : PlayerInfo {
    private Game game;
    private bool isActive;
    private Token token;
    private PlayerColour colour;
    private int money;
    private Space space;
    private List<Property> properties;
    private List<Card> getOutOfJailFreeCards;
    private bool inJail;
    private int turnInJail;
    private Debt debt;
    private List<Property> unresolvedMortgages;



    #region internal
    internal Player(Space space, Token token, PlayerColour colour, int startingMoney, Game game) {
        this.game = game;
        isActive = true;
        this.token = token;
        this.colour = colour;
        money = startingMoney;
        this.space = space;
        properties = new List<Property>();
        getOutOfJailFreeCards = new List<Card>(2);
        inJail = false;
        turnInJail = 0;
        unresolvedMortgages = new List<Property>();
        HasLostTurn = false;
        ToMoveAfterJailDebtResolving = false;
    }
    internal Space Space => space;
    internal Debt Debt => debt;
    internal void obtainProperty(Property property) {
        if (properties.Contains(property)) {
            throw new InvalidOperationException($"Player {colour} {token} already owns {property.Name}.");
        }
        properties.Add(property);
        property.changeOwner(this);
        if (property.IsMortgaged) {
            if (unresolvedMortgages.Contains(property)) {
                throw new InvalidOperationException(
                    $"Player {colour} {token} already has {property.Name} in their unresolved mortgages."
                );
            }
            unresolvedMortgages.Add(property);
        }
    }
    internal void removeProperty(Property property) {
        properties.Remove(property);
        unresolvedMortgages.Remove(property);
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
        debt = new SingleCreditorDebt(this, creditor, owed);
    }
    internal void incurDebt(Player[] creditors, int owedToEach) {
        debt = new MultiCreditorDebt(this, creditors, owedToEach);
    }
    internal void payDebt(int paid) {
        debt.pay(paid);
        money -= paid;
        if (debt.TotalOwed == 0) {
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
        Card getOutOfJailFreeCard = getOutOfJailFreeCards.FirstOrDefault(x => x.CardType == cardType);
        if (getOutOfJailFreeCard != null) {  
            getOutOfJailFreeCards.Remove(getOutOfJailFreeCard);
        }  // May already be cleared if player is eliminated.
        return getOutOfJailFreeCard;
    }
    internal void eliminateSelf() {
        isActive = false;
        space.removePlayer(this);
        game.Bank.takeEliminatedPlayerDebts(
            getTradablesList(),
            debt
        );
        debt = null;
        space = null;
        inJail = false;
        turnInJail = 0;
        properties.Clear();
        getOutOfJailFreeCards.Clear();
    }
    internal void removeUnresolvedMortgage(PropertyInfo propertyInfo) {
        Property property = (Property)propertyInfo;
        unresolvedMortgages.Remove(property);
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
    public IEnumerable<TradableInfo> TradableInfos => getTradablesList();
    public bool HasATradable => getTradablesList().Count > 0;
    public bool HasAnUnmortgagedProperty => properties.Any(x => !x.IsMortgaged);
    public bool HasAnUnresolvedMortgage => unresolvedMortgages.Count > 0;
    public PropertyInfo UnresolvedMortgageProperty => unresolvedMortgages[0];
    public bool hasGOOJFCardOfType(CardType cardType) {
        return getOutOfJailFreeCards.Any(x => x.CardType == cardType);
    }
    public bool ownsProperty(PropertyInfo propertyInfo) {
        Property property = (Property)propertyInfo;
        return properties.Contains(property);
    }
    public bool HasLostTurn { get; internal set; }
    public bool ToMoveAfterJailDebtResolving { get; internal set; }
    public int buildingsCanAdd(BuildingType buildingType) {
        List<EstateGroup> sortedEstateGroupsWithMonopolies = sortedListOfEstateGroupsWithMonopolies();
        int canAdd = 0;
        int tempMoney = money;
        foreach (EstateGroup estateGroup in sortedEstateGroupsWithMonopolies) {
            int couldPlace = buildingType == BuildingType.HOUSE
                ? estateGroup.housesPlayerCanAdd(this)
                : estateGroup.hotelsPlayerCanAdd(this);
            int canAfford = tempMoney / estateGroup.BuildingCost;
            int willAdd = couldPlace < canAfford ? couldPlace : canAfford;
            int willCost = willAdd * estateGroup.BuildingCost;
            canAdd += willAdd;
            tempMoney -= willCost;
            if (tempMoney < estateGroup.BuildingCost) break;
        }

        return canAdd;
    }
    #endregion



    #region private
    private List<Tradable> getTradablesList() {
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
    private List<EstateGroup> sortedListOfEstateGroupsWithMonopolies() {
        return properties
            .OfType<Estate>()
            .Select(x => x.EstateGroupInfo)
            .OfType<EstateGroup>()
            .Distinct()
            .Where(eg => eg.propertiesOwnedByPlayer(this) == eg.NumberOfPropertiesInGroup)
            .OrderBy(eg => (int)eg.EstateColour)
            .ToList();
    }
    #endregion
}
