
public interface PlayerInfo {
    public int SpaceIndex { get; }
    public SpaceInfo SpaceInfo { get; }
    public Token Token { get; }
    public PlayerColour Colour { get; }
    public int Money { get; }
    public int IncomeTaxAmount { get; }
    public bool InJail { get; }
}
