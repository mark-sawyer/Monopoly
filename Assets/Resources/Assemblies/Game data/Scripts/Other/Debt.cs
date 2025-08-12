
internal class Debt : DebtInfo {
    private Player debtor;
    private Creditor creditor;
    private int owed;



    #region internal
    internal Debt(Player debtor, Creditor creditor, int owed) {
        this.debtor = debtor;
        this.creditor = creditor;
        this.owed = owed;
    }
    internal void pay(int paid) {
        owed = owed - paid;
        if (creditor is Player player) {
            player.adjustMoney(paid);
        }
    }
    #endregion



    #region DebtInfo
    public PlayerInfo Debtor => debtor;
    public Creditor Creditor => creditor;
    public int Owed => owed;
    #endregion
}

public interface DebtInfo {
    public PlayerInfo Debtor { get; }
    public Creditor Creditor { get; }
    public int Owed { get; }
}
