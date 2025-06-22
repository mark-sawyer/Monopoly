
public interface PlayerInfo : Creditor {
    public int SpaceIndex { get; }
    public SpaceInfo SpaceInfo { get; }
    public Token Token { get; }
    public PlayerColour Colour { get; }
    public int Money { get; }
    public DebtInfo Debt { get; }
    public int TotalWorth { get; }
    public int IncomeTaxAmount { get; }
    public bool IsActive { get; }
    public bool InJail { get; }
    public int TurnInJail { get; }
    public bool HasGOOJFCard { get; }
    public bool hasGOOJFCardOfType(CardType cardType);
}
