using UnityEngine;

[CreateAssetMenu(menuName = "State/RaiseMoneyState")]
internal class RaiseMoneyState : State {
    private bool goToEliminatePlayer;
    private bool goToPostRaiseMoney;



    #region State
    public override void enterState() {
        goToEliminatePlayer = false;
        goToPostRaiseMoney = false;
        ResolveDebtEventHub.Instance.sub_DeclareBankruptcyButtonClicked(bankruptcyClicked);
        ResolveDebtEventHub.Instance.sub_DebtResolved(debtResolved);


        DebtInfo debtInfo = GameState.game.PlayerInDebt.DebtInfo;
        ScreenOverlayStarterEventHub.Instance.call_ResolveDebt(debtInfo);
    }
    public override bool exitConditionMet() {
        return goToEliminatePlayer
            || goToPostRaiseMoney;
    }
    public override void exitState() {
        ResolveDebtEventHub.Instance.unsub_DeclareBankruptcyButtonClicked(bankruptcyClicked);
        ResolveDebtEventHub.Instance.unsub_DebtResolved(debtResolved);
    }
    public override State getNextState() {
        if (goToEliminatePlayer) return allStates.getState<EliminatePlayerState>();
        if (goToPostRaiseMoney) return allStates.getState<PostRaiseMoneyState>();

        throw new System.Exception();
    }
    #endregion



    #region private
    private void bankruptcyClicked() {
        WaitFrames.Instance.beforeAction(
            FrameConstants.SCREEN_COVER_TRANSITION + 30,
            () => goToEliminatePlayer = true
        );
    }
    private void debtResolved() {
        WaitFrames.Instance.beforeAction(
            FrameConstants.SCREEN_COVER_TRANSITION,
            () => goToPostRaiseMoney = true
        );
    }
    #endregion
}
