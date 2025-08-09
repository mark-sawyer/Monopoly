using UnityEngine;

public class ResolveDebtPanel : ScreenAnimation<DebtInfo> {
    [SerializeField] private RectTransform rt;
    [SerializeField] private ResolveDebtTopRow resolveDebtTopRow;
    private DroppingQuestionsFunctionality droppingQuestionsFunctionality;



    #region MonoBehaviour
    private void Start() {
        ResolveDebtEventHub.Instance.sub_ResolveDebtVisualRefresh(checkIfRaisingMoneyOver);
    }
    private void OnDestroy() {
        ResolveDebtEventHub.Instance.unsub_ResolveDebtVisualRefresh(checkIfRaisingMoneyOver);
    }
    #endregion



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



    #region private
    private void checkIfRaisingMoneyOver() {
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        bool debtPaid = turnPlayer.DebtInfo == null;
        bool canRaiseMoney = turnPlayer.CanRaiseMoney;
        if (debtPaid || !canRaiseMoney) {
            WaitFrames.Instance.beforeAction(
                FrameConstants.MONEY_UPDATE + 50,
                () => ScreenAnimationEventHub.Instance.call_RemoveScreenAnimation()
            );
        }
    }
    #endregion
}
