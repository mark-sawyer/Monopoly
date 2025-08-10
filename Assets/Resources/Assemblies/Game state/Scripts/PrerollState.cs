using UnityEngine;

[CreateAssetMenu(menuName = "State/PreRollState")]
internal class PrerollState : State {
    private bool goToMoveToken;
    private bool goToJail;
    private bool goToResolveJailDebt;
    private bool goToUpdateTurnPlayer;
    private bool managePropertiesClicked;
    private bool tradeClicked;
    private TokenVisual turnTokenVisual;



    #region GameState
    public override void enterState() {
        void setBoolsToFalse() {
            goToMoveToken = false;
            goToJail = false;
            goToResolveJailDebt = false;
            goToUpdateTurnPlayer = false;
            managePropertiesClicked = false;
            tradeClicked = false;
        }
        void subscribeToEvents() {
            UIPipelineEventHub.Instance.sub_RollButtonClicked(rollButtonListener);
            ManagePropertiesEventHub.Instance.sub_ManagePropertiesOpened(managePropertiesListener);
            ScreenOverlayEventHub.Instance.sub_TradeOpened(tradeListener);
        }


        int turnIndex = GameState.game.TurnPlayer.Index;
        turnTokenVisual = TokenVisualManager.Instance.getTokenVisual(turnIndex);

        setBoolsToFalse();
        subscribeToEvents();

        UIEventHub.Instance.call_PrerollStateStarting();
    }
    public override bool exitConditionMet() {
        return goToMoveToken
            || managePropertiesClicked
            || tradeClicked
            || goToJail
            || goToUpdateTurnPlayer
            || goToResolveJailDebt;
    }
    public override void exitState() {
        UIPipelineEventHub.Instance.unsub_RollButtonClicked(rollButtonListener);
        ManagePropertiesEventHub.Instance.unsub_ManagePropertiesOpened(managePropertiesListener);
        ScreenOverlayEventHub.Instance.unsub_TradeOpened(tradeListener);

        UIEventHub.Instance.call_PrerollStateEnding();
    }
    public override State getNextState() {
        if (goToMoveToken) return allStates.getState<MoveTokenState>();
        if (goToJail) return allStates.getState<MoveTokenToJailState>();
        if (goToResolveJailDebt) return allStates.getState<ResolveJailDebtState>();
        if (goToUpdateTurnPlayer) return allStates.getState<UpdateTurnPlayerState>();
        if (managePropertiesClicked) return allStates.getState<ManagePropertiesState>();
        if (tradeClicked) return allStates.getState<TradeState>();
        throw new System.Exception();
    }
    #endregion



    #region private
    private void rollButtonListener() {
        void resolveRoll() {
            if (!GameState.game.TurnPlayer.InJail) {
                if (GameState.game.DiceInfo.ThreeDoublesInARow) goToJail = true;
                else goToMoveToken = true;
                UIEventHub.Instance.call_DoublesTickBoxUpdate();
            }
            else {
                resolveJailRoll();
            }
        }

        turnTokenVisual.prepForMoving();
        WaitFrames.Instance.beforeAction(
            InterfaceConstants.DIE_FRAMES_PER_IMAGE * InterfaceConstants.DIE_IMAGES_BEFORE_SETTLING,
            resolveRoll
        );
    }
    private void resolveJailRoll() {
        void doublesRolled() {
            SoundOnlyEventHub.Instance.call_CorrectOutcome();
            DataUIPipelineEventHub.Instance.call_LeaveJail();

            WaitFrames.Instance.beforeAction(
                FrameConstants.WAIT_FOR_LEAVING_JAIL,
                () => {
                    DataEventHub.Instance.call_DoublesCountReset();
                    turnTokenVisual.prepForMoving();
                    WaitFrames.Instance.beforeAction(
                        FrameConstants.TOKEN_SCALING,
                        () => goToMoveToken = true
                    );
                }
            );
        }
        void nonDoublesTurnOneTwo() {
            SoundOnlyEventHub.Instance.call_IncorrectOutcome();
            int jailIndex = GameConstants.JAIL_SPACE_INDEX;
            turnTokenVisual.moveTokenDirectlyToSpace(jailIndex, jailIndex);

            WaitFrames.Instance.beforeAction(
                FrameConstants.TOKEN_SCALING + 3,
                () => { goToUpdateTurnPlayer = true; }
            );
        }
        void nonDoublesTurnThree() {
            SoundOnlyEventHub.Instance.call_IncorrectOutcome();
            DataUIPipelineEventHub.Instance.call_LeaveJail();
            DataEventHub.Instance.call_PlayerIncurredDebt(
                GameState.game.TurnPlayer,
                GameState.game.BankCreditor,
                GameConstants.PRICE_FOR_LEAVING_JAIL
            );

            WaitFrames.Instance.beforeAction(
                FrameConstants.WAIT_FOR_LEAVING_JAIL,
                () => { goToResolveJailDebt = true; }
            );
        }


        bool rolledDoubles = GameState.game.DiceInfo.RolledDoubles;
        if (rolledDoubles) {
            doublesRolled();
        }
        else if (GameState.game.TurnPlayer.TurnInJail < 3) {
            nonDoublesTurnOneTwo();
        }
        else {
            nonDoublesTurnThree();
        }
    }
    private void managePropertiesListener() {
        managePropertiesClicked = true;
    }
    private void tradeListener() {
        tradeClicked = true;
    }
    #endregion
}
