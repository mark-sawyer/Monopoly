using UnityEngine;

public class ResolveDebtTopRow : MonoBehaviour {
    [SerializeField] private TokenIcon debtorIcon;
    [SerializeField] private TokenIcon creditorIcon;
    [SerializeField] private GameObject playerSection;
    [SerializeField] private GameObject bankSection;
    [SerializeField] private MoneyAdjuster debtRemainingPlayer;
    [SerializeField] private MoneyAdjuster debtRemainingBank;


    public void setup(DebtInfo debtInfo) {
        PlayerInfo debtorInfo = debtInfo.Debtor;
        debtorIcon.setup(debtorInfo.Token, debtorInfo.Colour);
        Creditor creditor = debtInfo.Creditor;
        if (creditor is PlayerInfo creditorInfo) {
            playerSection.SetActive(true);
            bankSection.SetActive(false);
            creditorIcon.setup(creditorInfo.Token, creditorInfo.Colour);
            debtRemainingPlayer.setStartingMoney(debtInfo.Owed);
        }
        else {
            bankSection.SetActive(true);
            playerSection.SetActive(false);
            debtRemainingBank.setStartingMoney(debtInfo.Owed);
        }
    }
}
