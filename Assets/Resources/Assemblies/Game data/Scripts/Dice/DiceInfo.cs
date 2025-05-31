
public interface DiceInfo {
    public int getDieValue(int i);
    public int TotalValue { get; }
    public bool RolledDoubles { get; }
    public bool ThreeDoublesInARow { get; }
}
