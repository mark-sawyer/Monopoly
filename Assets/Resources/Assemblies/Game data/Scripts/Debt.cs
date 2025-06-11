
internal class Debt : DebtInfo {
    private Player debtor;
    private Creditor creditor;
    private int owed;



    internal Debt(Player debtor, Creditor creditor, int owed) {
        this.debtor = debtor;
        this.creditor = creditor;
        this.owed = owed;
    }



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
