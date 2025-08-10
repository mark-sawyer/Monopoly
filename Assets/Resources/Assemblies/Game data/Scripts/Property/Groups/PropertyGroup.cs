
public interface PropertyGroupInfo {
    public int NumberOfPropertiesInGroup { get; }
    public bool MortgageExists { get; }
    public int MortgageCount { get; }
    public int propertiesOwnedByPlayer(PlayerInfo playerInfo);
    public bool playerHasMortgageInGroup(PlayerInfo playerInfo);
    public PropertyInfo getPropertyInfo(int index);
}
