
public interface GameStateInfo {
    public PlayerInfo[] getPlayers();
    public DieValueReader getDie(int index);
    public PlayerInfo getTurnPlayer();
    public int getPlayerIndex(PlayerInfo player);
}
