
internal class SingleCreditorDebt : Debt, SingleCreditorDebtInfo {
    Player debtor;
    Creditor creditor;
    int owed;



    #region internal
    internal SingleCreditorDebt(Player debtor, Creditor creditor, int owed) {
        this.debtor = debtor;
        this.creditor = creditor;
        this.owed = owed;
    }
    public Player Debtor => debtor;
    public void pay(int paid) {
        owed = owed - paid;
        if (creditor is Player player) {
            player.adjustMoney(paid);
        }
    }
    #endregion



    #region SingleCreditorDebtInfo
    public PlayerInfo DebtorInfo => debtor;
    public Creditor Creditor => creditor;
    public int TotalOwed => owed;
    #endregion
}

public interface SingleCreditorDebtInfo : DebtInfo {
    public Creditor Creditor { get; }
}
