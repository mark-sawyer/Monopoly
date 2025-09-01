using System;
using System.Collections;
using UnityEngine;

public class ResolveDebtPanel : ScreenOverlay<DebtInfo> {
    [SerializeField] private RectTransform rt;
    [SerializeField] private ResolveDebtTopRow resolveDebtTopRow;
    [SerializeField] private RDEstateGroupSection[] estateGroupSections;
    [SerializeField] private RDOtherPropertyGroupSection[] otherPropertyGroupSections;
    [SerializeField] private DeclareBankruptcyButton declareBankruptcyButton;
    [SerializeField] private ResolveDebtTradeButton resolveDebtTradeButton;
    [SerializeField] private GameObject raiseMoneyTradeSelectionPrefab;
    private DebtInfo debtInfo;



    #region MonoBehaviour
    private void Start() {
        ResolveDebtEventHub.Instance.sub_ResolveDebtVisualRefresh(checkIfRaisingMoneyOver);
        ResolveDebtEventHub.Instance.sub_TradeButtonClicked(selectTradingPlayers);
        ResolveDebtEventHub.Instance.sub_DeclareBankruptcyButtonClicked(declareBankruptcy);
    }
    private void OnDestroy() {
        ResolveDebtEventHub.Instance.unsub_ResolveDebtVisualRefresh(checkIfRaisingMoneyOver);
        ResolveDebtEventHub.Instance.unsub_TradeButtonClicked(selectTradingPlayers);
        ResolveDebtEventHub.Instance.unsub_DeclareBankruptcyButtonClicked(declareBankruptcy);
    }
    #endregion



    #region ScreenAnimation
    public override void setup(DebtInfo debtInfo) {
        this.debtInfo = debtInfo;
        resolveDebtTopRow.setup(debtInfo);
        declareBankruptcyButton.setup(debtInfo.DebtorInfo);
        resolveDebtTradeButton.setup(debtInfo.DebtorInfo);

        foreach (RDEstateGroupSection estateGroupSection in estateGroupSections) {
            estateGroupSection.setup(debtInfo.DebtorInfo);
        }
        foreach (RDOtherPropertyGroupSection otherPropertyGroupSection in otherPropertyGroupSections) {
            otherPropertyGroupSection.setup(debtInfo.DebtorInfo);
        }
        ResolveDebtEventHub.Instance.call_ResolveDebtVisualRefresh();
    }
    public override void appear() {
        IEnumerator firstAppear(ScreenOverlayDropper screenOverlayDropper) {
            ResolveDebtEventHub.Instance.call_PanelInTransit();
            yield return screenOverlayDropper.drop();
            ResolveDebtEventHub.Instance.call_ResolveDebtPanelLowered();
        }


        SoundPlayer.Instance.play_DunDuuuuuuun();
        ScreenOverlayDropper screenOverlayDropper = new ScreenOverlayDropper(rt);
        StartCoroutine(firstAppear(screenOverlayDropper));
    }
    #endregion



    #region public
    public void appearFromTradeBack() {
        IEnumerator reappear() {
            yield return lowerResolveDebts();
            ResolveDebtEventHub.Instance.call_ResolveDebtPanelLowered();
        }


        StartCoroutine(reappear());
    }
    public void appearFromTradeComplete() {
        IEnumerator reappear(bool moneyWasExchanged) {
            ResolveDebtEventHub.Instance.call_PanelInTransit();
            yield return lowerResolveDebts();
            yield return WaitFrames.Instance.frames(20);
            if (moneyWasExchanged) yield return WaitFrames.Instance.frames(FrameConstants.MONEY_UPDATE);
            ResolveDebtEventHub.Instance.call_ResolveDebtPanelLowered();
            if (debtInfo.Paid) {
                yield return WaitFrames.Instance.frames(50);
                ScreenOverlayFunctionEventHub.Instance.call_RemoveScreenOverlay();
                ResolveDebtEventHub.Instance.call_DebtResolved();
            }
        }



        TradeInfo tradeInfo = GameState.game.CompletedTrade;
        if (tradeInfo.MoneyWasExchanged) {
            PlayerInfo debtor = debtInfo.DebtorInfo;
            DataUIPipelineEventHub.Instance.call_TradeMoneyPaidToDebt(debtor);
        }
        ResolveDebtEventHub.Instance.unsub_ResolveDebtVisualRefresh(checkIfRaisingMoneyOver);
        ResolveDebtEventHub.Instance.call_ResolveDebtVisualRefresh();
        ResolveDebtEventHub.Instance.sub_ResolveDebtVisualRefresh(checkIfRaisingMoneyOver);
        StartCoroutine(reappear(tradeInfo.MoneyWasExchanged));
    }
    #endregion



    #region private
    private void checkIfRaisingMoneyOver() {
        if (debtInfo.Paid) {
            WaitFrames.Instance.beforeAction(
                FrameConstants.MONEY_UPDATE + 50,
                () => {
                    ScreenOverlayFunctionEventHub.Instance.call_RemoveScreenOverlay();
                    ResolveDebtEventHub.Instance.call_DebtResolved();
                }
            );
        }
    }
    private void selectTradingPlayers() {
        StartCoroutine(tradeSelectionCoroutine());
    }
    private void declareBankruptcy() {
        ScreenOverlayFunctionEventHub.Instance.call_RemoveScreenOverlay();
    }
    private IEnumerator tradeSelectionCoroutine() {
        IEnumerator raiseResolveDebts() {
            float startY = rt.anchoredPosition.y;
            float endY = InterfaceConstants.STANDARD_HEIGHT_ABOVE_SCREEN;
            Func<float, float> getY = LinearValue.getFunc(startY, endY, FrameConstants.MANAGE_PROPERTIES_DROP);
            for (int i = 1; i <= FrameConstants.MANAGE_PROPERTIES_DROP; i++) {
                float yPos = getY(i);
                rt.anchoredPosition = new Vector2(0f, yPos);
                yield return null;
            }
            rt.anchoredPosition = new Vector2(0f, InterfaceConstants.STANDARD_HEIGHT_ABOVE_SCREEN);
        }


        ResolveDebtEventHub.Instance.call_PanelInTransit();
        SoundPlayer.Instance.play_Swoop();
        RectAnchorPivotMover rectAnchorPivotMover = new RectAnchorPivotMover(rt);
        rectAnchorPivotMover.moveAnchors(new Vector2(0.5f, 1f));
        rectAnchorPivotMover.movePivot(new Vector2(0.5f, 0f));
        yield return raiseResolveDebts();
        GameObject instance = Instantiate(raiseMoneyTradeSelectionPrefab, transform.parent);
        RaiseMoneyTradeSelection raiseMoneyTradeSelection = instance.GetComponent<RaiseMoneyTradeSelection>();
        raiseMoneyTradeSelection.setupAndAppear(debtInfo.DebtorInfo, this);
    }
    private IEnumerator lowerResolveDebts() {
        SoundPlayer.Instance.play_Swoop();
        RectAnchorPivotMover rectAnchorPivotMover = new RectAnchorPivotMover(rt);
        rectAnchorPivotMover.moveAnchors(new Vector2(0.5f, 0.5f));
        rectAnchorPivotMover.movePivot(new Vector2(0.5f, 0.5f));
        float startY = rt.anchoredPosition.y;
        Func<float, float> getY = LinearValue.getFunc(startY, 0, FrameConstants.MANAGE_PROPERTIES_DROP);
        for (int i = 1; i <= FrameConstants.MANAGE_PROPERTIES_DROP; i++) {
            float yPos = getY(i);
            rt.anchoredPosition = new Vector2(0f, yPos);
            yield return null;
        }
        rt.anchoredPosition = Vector2.zero;
    }
    #endregion
}
