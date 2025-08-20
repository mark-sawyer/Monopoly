using UnityEngine;

[CreateAssetMenu(menuName = "State/PreRollState")]
internal class PrerollState : State {
    private bool goToMoveToken;
    private bool goToJail;
    private bool managePropertiesClicked;
    private bool tradeClicked;
    private bool goToPreroll;
    private bool goToResolveDebt;
    private bool goToEscapeMenu;



    #region GameState
    public override void enterState() {
        void setBoolsToFalse() {
            goToMoveToken = false;
            goToJail = false;
            managePropertiesClicked = false;
            tradeClicked = false;
            goToPreroll = false;
            goToResolveDebt = false;
            goToEscapeMenu = false;
        }
        void subscribeToEvents() {
            UIPipelineEventHub.Instance.sub_RollButtonClicked(rollButtonListener);
            ManagePropertiesEventHub.Instance.sub_ManagePropertiesOpened(managePropertiesListener);
            ScreenOverlayEventHub.Instance.sub_TradeOpened(tradeListener);
            UIEventHub.Instance.sub_EscapeClicked(escapeListener);
        }
        void adjustTurnPlayer() {
            DiceInfo diceInfo = GameState.game.DiceInfo;
            PlayerInfo turnPlayer = GameState.game.TurnPlayer;
            if (turnPlayer.HasLostTurn || !turnPlayer.IsActive) {
                DataUIPipelineEventHub.Instance.call_NextPlayerTurn();
                PlayerInfo newTurnPlayer = GameState.game.TurnPlayer;
                if (newTurnPlayer.InJail) {
                    DataEventHub.Instance.call_IncrementJailTurn();
                }
            }
        }


        setBoolsToFalse();
        subscribeToEvents();
        adjustTurnPlayer();
        UIEventHub.Instance.call_PrerollStateStarting();
    }
    public override bool exitConditionMet() {
        return goToMoveToken
            || managePropertiesClicked
            || tradeClicked
            || goToJail
            || goToPreroll
            || goToResolveDebt
            || goToEscapeMenu;
    }
    public override void exitState() {

        UIPipelineEventHub.Instance.unsub_RollButtonClicked(rollButtonListener);
        ManagePropertiesEventHub.Instance.unsub_ManagePropertiesOpened(managePropertiesListener);
        ScreenOverlayEventHub.Instance.unsub_TradeOpened(tradeListener);
        UIEventHub.Instance.unsub_EscapeClicked(escapeListener);
    }
    public override State getNextState() {
        if (goToEscapeMenu) return allStates.getState<EscapeMenuState>();
        if (goToMoveToken) return allStates.getState<MoveTokenState>();
        if (managePropertiesClicked) return allStates.getState<ManagePropertiesState>();
        if (tradeClicked) return allStates.getState<TradeState>();
        if (goToJail) return allStates.getState<MoveTokenToJailState>();
        if (goToPreroll) return this;
        if (goToResolveDebt) return allStates.getState<ResolveDebtState>();
        throw new System.Exception();
    }
    #endregion



    #region Listeners
    private void rollButtonListener() {
        UIEventHub.Instance.call_PrerollStateEnding();
        getTurnTokenVisual().prepForMoving();
        WaitFrames.Instance.beforeAction(
            InterfaceConstants.DIE_FRAMES_PER_IMAGE * InterfaceConstants.DIE_IMAGES_BEFORE_SETTLING,
            resolveRoll
        );
    }
    private void managePropertiesListener() {
        UIEventHub.Instance.call_PrerollStateEnding();
        managePropertiesClicked = true;
    }
    private void tradeListener() {
        UIEventHub.Instance.call_PrerollStateEnding();
        tradeClicked = true;
    }
    private void escapeListener() {
        UIEventHub.Instance.call_PrerollStateEnding();
        goToEscapeMenu = true;
    }
    #endregion



    #region private
    private void resolveRoll() {
        if (GameState.game.TurnPlayer.InJail) {
            DataEventHub.Instance.call_TurnPlayerWillLoseTurn();
            resolveJailRoll();
        }
        else {
            UIEventHub.Instance.call_DoublesTickBoxUpdate();

            bool rolledDoubles = GameState.game.DiceInfo.RolledDoubles;
            bool threeInARow = GameState.game.DiceInfo.DoublesInARow == 3;

            if (!rolledDoubles) {
                DataEventHub.Instance.call_TurnPlayerWillLoseTurn();
                goToMoveToken = true;
            }
            else if (!threeInARow) {
                goToMoveToken = true;
            }
            else {
                DataEventHub.Instance.call_TurnPlayerWillLoseTurn();
                goToJail = true;
            }
        }
    }
    private void resolveJailRoll() {
        int incorrectSoundBufferFrames = 15;
        void doublesRolled() {
            DataUIPipelineEventHub.Instance.call_LeaveJail();

            WaitFrames.Instance.beforeAction(
                FrameConstants.WAIT_FOR_LEAVING_JAIL,
                () => {
                    DataEventHub.Instance.call_DoublesCountReset();
                    getTurnTokenVisual().prepForMoving();
                    WaitFrames.Instance.beforeAction(
                        FrameConstants.TOKEN_SCALING,
                        () => goToMoveToken = true
                    );
                }
            );
        }
        void nonDoublesTurnOneTwo() {
            SoundOnlyEventHub.Instance.call_IncorrectOutcome();
            WaitFrames.Instance.beforeAction(
                incorrectSoundBufferFrames,
                () => {
                    int jailIndex = GameConstants.JAIL_SPACE_INDEX;
                    getTurnTokenVisual().moveTokenDirectlyToSpace(jailIndex, jailIndex);

                    WaitFrames.Instance.beforeAction(
                        FrameConstants.TOKEN_SCALING + 3,
                        () => { goToPreroll = true; }
                    );
                }
            );
        }
        void nonDoublesTurnThree() {
            SoundOnlyEventHub.Instance.call_IncorrectOutcome();
            WaitFrames.Instance.beforeAction(
                incorrectSoundBufferFrames,
                () => {
                    DataUIPipelineEventHub.Instance.call_LeaveJail();
                    DataEventHub.Instance.call_PlayerIncurredDebt(
                        GameState.game.TurnPlayer,
                        GameState.game.BankCreditor,
                        GameConstants.PRICE_FOR_LEAVING_JAIL
                    );

                    WaitFrames.Instance.beforeAction(
                        FrameConstants.WAIT_FOR_LEAVING_JAIL,
                        () => {
                            DataEventHub.Instance.call_SetJailDebtBool(GameState.game.TurnPlayer, true);
                            goToResolveDebt = true;
                        }
                    );
                }
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
    private TokenVisual getTurnTokenVisual() {
        int turnIndex = GameState.game.TurnPlayer.Index;
        return TokenVisualManager.Instance.getTokenVisual(turnIndex);
    }
    #endregion
}
