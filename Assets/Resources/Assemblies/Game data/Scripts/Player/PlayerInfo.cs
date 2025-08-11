
using System.Collections.Generic;

public interface PlayerInfo : Creditor {
    public int Index { get; }
    public int SpaceIndex { get; }
    public SpaceInfo SpaceInfo { get; }
    public Token Token { get; }
    public PlayerColour Colour { get; }
    public int Money { get; }
    public DebtInfo DebtInfo { get; }
    public int TotalWorth { get; }
    public int IncomeTaxAmount { get; }
    public bool IsActive { get; }
    public bool InJail { get; }
    public int TurnInJail { get; }
    public bool HasGOOJFCard { get; }
    public int HousesOwned { get; }
    public int HotelsOwned { get; }
    public IEnumerable<TradableInfo> TradableInfos { get; }
    public bool CanRaiseMoney { get; }
    public bool HasAnUnresolvedMortgage { get; }
    public PropertyInfo UnresolvedMortgageProperty { get; }
    public bool hasGOOJFCardOfType(CardType cardType);
    public bool ownsProperty(PropertyInfo propertyInfo);
    public bool HasLostTurn { get; }
}
