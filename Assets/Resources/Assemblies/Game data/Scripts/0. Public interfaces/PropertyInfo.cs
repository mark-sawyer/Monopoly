
public interface PropertyInfo {
    public int Cost { get; }
    public string Name { get; }
    public int MortgageValue { get; }
    public bool IsBought { get; }
    public PlayerInfo Owner { get; }
}
