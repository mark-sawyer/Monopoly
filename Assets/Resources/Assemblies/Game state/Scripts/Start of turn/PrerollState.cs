using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/PreRollState")]
internal class PrerollState : State {
    private bool goToMoveToken;
    private bool goToJail;
    private bool goToResolveJailDebt;
    private bool goToUpdateTurnPlayer;
    private bool managePropertiesClicked;
    private bool tradeClicked;
    private bool goToResolveMortgage;



    #region GameState
    public override void enterState() {
        void setBoolsToFalse() {
            goToMoveToken = false;
            goToJail = false;
            goToResolveJailDebt = false;
            goToUpdateTurnPlayer = false;
            managePropertiesClicked = false;
            tradeClicked = false;
            goToResolveMortgage = false;
        }
        void subscribeToEvents() {
            UIPipelineEventHub.Instance.sub_RollButtonClicked(rollButtonListener);
            ManagePropertiesEventHub.Instance.sub_ManagePropertiesOpened(managePropertiesListener);
            ScreenOverlayEventHub.Instance.sub_TradeOpened(tradeListener);
        }

        setBoolsToFalse();
        subscribeToEvents();

        PlayerInfo playerWithUnresolvedMortgage = getPlayerWithUnresolvedMortgage();
        if (playerWithUnresolvedMortgage == null) {
            UIEventHub.Instance.call_PrerollStateStarting();
        }
        else {
            goToResolveMortgage = true;
        }
    }
    public override bool exitConditionMet() {
        return goToMoveToken
            || managePropertiesClicked
            || tradeClicked
            || goToJail
            || goToUpdateTurnPlayer
            || goToResolveJailDebt
            || goToResolveMortgage;
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
        //if (goToResolveJailDebt) return allStates.getState<ResolveJailDebtState>();
        if (goToUpdateTurnPlayer) return allStates.getState<UpdateTurnPlayerState>();
        if (managePropertiesClicked) return allStates.getState<ManagePropertiesState>();
        if (tradeClicked) return allStates.getState<TradeState>();
        if (goToResolveMortgage) return allStates.getState<ResolveMortgageState>();            
        throw new System.Exception();
    }
    #endregion



    #region Listeners
    private void rollButtonListener() {
        getTurnTokenVisual().prepForMoving();
        WaitFrames.Instance.beforeAction(
            InterfaceConstants.DIE_FRAMES_PER_IMAGE * InterfaceConstants.DIE_IMAGES_BEFORE_SETTLING,
            resolveRoll
        );
    }
    private void managePropertiesListener() {
        managePropertiesClicked = true;
    }
    private void tradeListener() {
        tradeClicked = true;
    }
    #endregion



    #region private
    private void resolveRoll() {
        if (!GameState.game.TurnPlayer.InJail) {
            UIEventHub.Instance.call_DoublesTickBoxUpdate();

            bool rolledDoubles = GameState.game.DiceInfo.RolledDoubles;
            bool threeInARow = GameState.game.DiceInfo.ThreeDoublesInARow;

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
        else {
            DataEventHub.Instance.call_TurnPlayerWillLoseTurn();
            resolveJailRoll();
        }
    }
    private void resolveJailRoll() {
        void doublesRolled() {
            SoundOnlyEventHub.Instance.call_CorrectOutcome();
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
            int jailIndex = GameConstants.JAIL_SPACE_INDEX;
            getTurnTokenVisual().moveTokenDirectlyToSpace(jailIndex, jailIndex);

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
    private PlayerInfo getPlayerWithUnresolvedMortgage() {
        PlayerInfo playerInfo = null;
        IEnumerable<PlayerInfo> activePlayers = GameState.game.ActivePlayers;
        foreach (PlayerInfo thisPlayer in activePlayers) {
            if (thisPlayer.HasAnUnresolvedMortgage) {
                playerInfo = thisPlayer;
                break;
            }
        }
        return playerInfo;
    }
    private TokenVisual getTurnTokenVisual() {
        int turnIndex = GameState.game.TurnPlayer.Index;
        return TokenVisualManager.Instance.getTokenVisual(turnIndex);
    }
    #endregion
}
