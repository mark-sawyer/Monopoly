using System.Security.Cryptography;
using UnityEngine;

public class ResolveDebtTopRow : MonoBehaviour {
    [SerializeField] private TokenIcon debtorIcon;
    [SerializeField] private TokenIcon creditorIcon;
    [SerializeField] private GameObject playerSection;
    [SerializeField] private GameObject bankSection;
    [SerializeField] private GameObject everyoneSection;
    [SerializeField] private MoneyAdjuster debtRemainingPlayer;
    [SerializeField] private MoneyAdjuster debtRemainingBank;
    [SerializeField] private MoneyAdjuster debtRemainingEveryone;
    private MoneyAdjuster moneyAdjusterInUse;
    private DebtInfo debtInfo;



    #region MonoBehaviour
    private void OnDestroy() {
        UIPipelineEventHub.Instance.unsub_MoneyRaisedForDebt(adjustPanelAfterPayment);
    }
    #endregion



    #region public
    public void setup(DebtInfo debtInfo) {
        this.debtInfo = debtInfo;
        PlayerInfo debtorInfo = debtInfo.DebtorInfo;
        debtorIcon.setup(debtorInfo.Token, debtorInfo.Colour);

        if (debtInfo is SingleCreditorDebtInfo singleCreditorDebtInfo) {
            setupSingleCreditor(singleCreditorDebtInfo);
        }
        else {
            MultiCreditorDebtInfo multiCreditorDebtInfo = (MultiCreditorDebtInfo)debtInfo;
            setupMultiCreditor(multiCreditorDebtInfo);
        }
        moneyAdjusterInUse.setStartingMoney(debtInfo.TotalOwed);

        UIPipelineEventHub.Instance.sub_MoneyRaisedForDebt(adjustPanelAfterPayment);
    }
    #endregion



    #region private
    private void setupSingleCreditor(SingleCreditorDebtInfo singleCreditorDebtInfo) {
        Creditor creditor = singleCreditorDebtInfo.Creditor;
        if (creditor is PlayerInfo creditorInfo) {
            playerSection.SetActive(true);
            bankSection.SetActive(false);
            everyoneSection.SetActive(false);
            creditorIcon.setup(creditorInfo.Token, creditorInfo.Colour);
            moneyAdjusterInUse = debtRemainingPlayer;
        }
        else {
            playerSection.SetActive(false);
            bankSection.SetActive(true);
            everyoneSection.SetActive(false);
            moneyAdjusterInUse = debtRemainingBank;
        }
    }
    private void setupMultiCreditor(MultiCreditorDebtInfo multiCreditorDebtInfo) {
        playerSection.SetActive(false);
        bankSection.SetActive(false);
        everyoneSection.SetActive(true);
        moneyAdjusterInUse = debtRemainingEveryone;
    }
    private void adjustPanelAfterPayment() {
        void adjust() {
            SoundPlayer.Instance.play_MoneyChing();
            moneyAdjusterInUse.adjustMoney(debtInfo);
        }


        if (!OffScreen) adjust();
        else {
            WaitFrames.Instance.beforeAction(
                FrameConstants.MANAGE_PROPERTIES_DROP
                + 20,
                adjust
            );
        }
    }
    private bool OffScreen => ((RectTransform)transform.parent.parent.parent.parent).anchoredPosition.y > 10;
    #endregion
}
