
internal interface Debt : DebtInfo {
    public Player Debtor { get; }
    public void pay(int paid);
}

public interface DebtInfo {
    public PlayerInfo DebtorInfo { get; }
    public int TotalOwed { get; }
}
