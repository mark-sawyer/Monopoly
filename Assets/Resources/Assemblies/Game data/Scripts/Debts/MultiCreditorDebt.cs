using System;
using System.Collections.Generic;
using System.Linq;

internal class MultiCreditorDebt : Debt, MultiCreditorDebtInfo {
    private Player debtor;
    private Player[] creditors;
    private int[] individuallyOwed;



    #region internal
    internal MultiCreditorDebt(Player debtor, Player[] creditors, int owedToEach) {
        this.debtor = debtor;
        this.creditors = creditors;
        individuallyOwed = new int[creditors.Length];
        for (int i = 0; i < creditors.Length; i++) {
            individuallyOwed[i] = owedToEach;
        }
    }
    public Player Debtor => debtor;
    public void pay(int paymentRemaining) {
        int uniqueCount = individuallyOwed.Distinct().Count();
        if (uniqueCount > 1) {
            int max = individuallyOwed.Max();
            int indexOfMax = Array.IndexOf(individuallyOwed, max);
            paymentRemaining = payThroughTheArray(indexOfMax, 1, paymentRemaining);
            if (paymentRemaining == 0) return;
            int newUniqueCount = individuallyOwed.Distinct().Count();
            if (newUniqueCount > 1) throw new Exception("My algorithm is wrong.");
        }

        int paidToEach = paymentRemaining / creditors.Length;
        paymentRemaining = payThroughTheArray(0, paidToEach, paymentRemaining);
        if (paymentRemaining == 0) return;
        payThroughTheArray(0, 1, paymentRemaining);
    }
    #endregion



    #region MultiCreditorDebtInfo
    public PlayerInfo DebtorInfo => debtor;
    public IEnumerable<PlayerInfo> Creditors => creditors;
    public int TotalOwed => individuallyOwed.Sum();
    public int[] IndividuallyOwed => individuallyOwed;
    #endregion



    #region private
    private int payThroughTheArray(int startingIndex, int paidToIndividual, int paymentRemaining) {
        for (int i = startingIndex; i < creditors.Length; i++) {
            individuallyOwed[i] -= paidToIndividual;
            creditors[i].adjustMoney(paidToIndividual);
            paymentRemaining -= paidToIndividual;
            if (paymentRemaining == 0) break;
        }
        return paymentRemaining;
    }
    #endregion
}

public interface MultiCreditorDebtInfo : DebtInfo {
    public IEnumerable<PlayerInfo> Creditors { get; }
    public int[] IndividuallyOwed { get; }
}
