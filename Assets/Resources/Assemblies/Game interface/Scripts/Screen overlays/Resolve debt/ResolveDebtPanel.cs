using UnityEngine;

public class ResolveDebtPanel : ScreenOverlay<DebtInfo> {
    [SerializeField] private RectTransform rt;
    [SerializeField] private ResolveDebtTopRow resolveDebtTopRow;
    [SerializeField] private RDEstateGroupSection[] estateGroupSections;
    [SerializeField] private RDOtherPropertyGroupSection[] otherPropertyGroupSections;
    private ScreenOverlayDropper screenOverlayDropper;

    

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
        screenOverlayDropper = new ScreenOverlayDropper(rt);
        screenOverlayDropper.adjustSize();
        resolveDebtTopRow.setup(debtInfo);
        foreach (RDEstateGroupSection estateGroupSection in estateGroupSections) estateGroupSection.setup(debtInfo.DebtorInfo);
        foreach (RDOtherPropertyGroupSection otherPropertyGroupSection in otherPropertyGroupSections) otherPropertyGroupSection.setup(debtInfo.DebtorInfo);
    }
    public override void appear() {
        StartCoroutine(screenOverlayDropper.drop());
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
                () => ScreenOverlayEventHub.Instance.call_RemoveScreenAnimation()
            );
        }
    }
    #endregion
}
