
public interface DiceInfo {
    public int getDieValue(int i);
    public int TotalValue { get; }
    public bool RolledDoubles { get; }  // This should depend on the last three rolls tracker, so it's false after leaving jail through doubles.
    public bool ThreeDoublesInARow { get; }
}
