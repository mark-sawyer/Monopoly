using System.Collections.Generic;

public interface GameStateInfo {
    public IEnumerable<PlayerInfo> PlayerInfos { get; }
    public IEnumerable<PlayerInfo> ActivePlayers { get; }
    public PlayerInfo TurnPlayer { get; }
    public int IndexOfTurnPlayer { get; }
    public int SpaceIndexOfTurnPlayer { get; }
    public SpaceInfo SpaceInfoOfTurnPlayer { get; }
    public DiceInfo DiceInfo { get; }
    public int NumberOfPlayers { get; }
    public Creditor BankCreditor { get; }
    public BankInfo BankInfo { get; }
    public CardInfo DrawnCard { get; }
    public bool TradeIsEmpty { get; }
    public TradeInfo CompletedTrade { get; }
    public SpaceInfo getSpaceInfo(int index);
    public int getSpaceIndex(SpaceInfo space);
    public PlayerInfo getPlayerInfo(int index);
}
