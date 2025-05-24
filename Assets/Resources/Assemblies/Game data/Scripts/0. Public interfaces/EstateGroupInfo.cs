using UnityEngine;

public interface EstateGroupInfo {
    public int GroupID { get; }
    public Color EstateColour { get; }
    public Color HighlightColour { get; }
    public int NumberOfEstatesInGroup { get; }
    public int MinBuildingCount { get; }
    public int MaxBuildingCount { get; }
    public bool HotelExists { get; }
    public EstateInfo getEstateInfo(int index);
    public int propertiesOwnedByPlayer(PlayerInfo player);
}
