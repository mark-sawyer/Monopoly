using UnityEngine;

public class ResolveDebtPanel : ScreenAnimation<DebtInfo> {
    [SerializeField] private RectTransform rt;
    [SerializeField] private ResolveDebtTopRow resolveDebtTopRow;
    private DroppingQuestionsFunctionality droppingQuestionsFunctionality;



    #region ScreenAnimation
    public override void setup(DebtInfo debtInfo) {
        droppingQuestionsFunctionality = new DroppingQuestionsFunctionality(rt);
        droppingQuestionsFunctionality.adjustSize();
        resolveDebtTopRow.setup(debtInfo);
    }
    public override void appear() {
        StartCoroutine(droppingQuestionsFunctionality.drop());
    }
    #endregion
}
