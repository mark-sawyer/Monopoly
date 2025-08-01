using UnityEngine;

[CreateAssetMenu(menuName = "State/JailPreRoll")]
public class JailPreRollState : State {
    private bool payFiftyButtonClicked;
    private bool useCardButtonClicked;
    private bool goToMoveToken;
    private bool goToUpdateTurnPlayer;
    private bool goToResolveJailDebt;
    private bool tradeClicked;
    private bool managePropertiesClicked;
    private const int FRAMES_WAITED_FOR_LEAVING_JAIL = 200;



    #region
    public override void enterState() {
        void setBoolsToFalse() {
            payFiftyButtonClicked = false;
            useCardButtonClicked = false;
            goToMoveToken = false;
            goToUpdateTurnPlayer = false;
            goToResolveJailDebt = false;
            tradeClicked = false;
            managePropertiesClicked = false;
        }
        void subscribeToEvents() {
            UIEventHub.Instance.sub_RollButtonClicked(rollButtonListener);
            UIEventHub.Instance.sub_PayFiftyButtonClicked(payFiftyListener);
            UIEventHub.Instance.sub_UseGOOJFCardButtonClicked(useCardListener);
            ManagePropertiesEventHub.Instance.sub_ManagePropertiesOpened(managePropertiesListener);
            ScreenAnimationEventHub.Instance.sub_TradeOpened(tradeListener);
        }


        setBoolsToFalse();
        subscribeToEvents();
        UIEventHub.Instance.call_JailPreRollStateStarting();
    }
    public override bool exitConditionMet() {
        return payFiftyButtonClicked
            || useCardButtonClicked
            || goToMoveToken
            || goToUpdateTurnPlayer
            || goToResolveJailDebt
            || managePropertiesClicked
            || tradeClicked;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_RollButtonClicked(rollButtonListener);
        UIEventHub.Instance.unsub_PayFiftyButtonClicked(payFiftyListener);
        UIEventHub.Instance.unsub_UseGOOJFCardButtonClicked(useCardListener);
        ManagePropertiesEventHub.Instance.unsub_ManagePropertiesOpened(managePropertiesListener);
        ScreenAnimationEventHub.Instance.unsub_TradeOpened(tradeListener);
    }
    public override State getNextState() {
        if (payFiftyButtonClicked || useCardButtonClicked) return allStates.getState<PreRollState>();
        if (goToMoveToken) return allStates.getState<MoveTokenState>();
        if (goToUpdateTurnPlayer) return allStates.getState<UpdateTurnPlayerState>();
        if (goToResolveJailDebt) return allStates.getState<ResolveJailDebtState>();
        if (managePropertiesClicked) return allStates.getState<ManagePropertiesState>();
        if (tradeClicked) return allStates.getState<TradeState>();

        throw new System.Exception();
    }
    #endregion



    #region private
    private void rollButtonListener() {
        int turnIndex = GameState.game.IndexOfTurnPlayer;
        TokenVisual turnTokenVisual = TokenVisualManager.Instance.getTokenVisual(turnIndex);
        turnTokenVisual.prepForMoving();
        WaitFrames.Instance.exe(
            InterfaceConstants.DIE_FRAMES_PER_IMAGE * InterfaceConstants.DIE_IMAGES_BEFORE_SETTLING,
            resolveDiceRollOutcomes,
            turnTokenVisual
        );
    }
    private void resolveDiceRollOutcomes(TokenVisual turnTokenVisual) {
        void doublesRolled() {
            DataEventHub.Instance.call_DoublesCountReset();
            turnTokenVisual.prepForMoving();
            WaitFrames.Instance.exe(
                InterfaceConstants.FRAMES_FOR_TOKEN_SCALING,
                () => goToMoveToken = true
            );
        }
        void nonDoublesTurnOneTwo() {
            int jailIndex = GameConstants.JAIL_SPACE_INDEX;
            turnTokenVisual.moveTokenDirectlyToSpace(jailIndex, jailIndex);

            WaitFrames.Instance.exe(
                InterfaceConstants.FRAMES_FOR_TOKEN_SCALING + 3,
                () => { goToUpdateTurnPlayer = true; }
            );
        }
        void nonDoublesTurnThree() {
            DataEventHub.Instance.call_LeaveJail();
            DataEventHub.Instance.call_PlayerIncurredDebt(
                GameState.game.TurnPlayer,
                GameState.game.BankCreditor,
                GameConstants.PRICE_FOR_LEAVING_JAIL
            );

            WaitFrames.Instance.exe(
                FRAMES_WAITED_FOR_LEAVING_JAIL,
                () => { goToResolveJailDebt = true; }
            );
        }
        void nonDoublesRolled() {
            if (GameState.game.TurnPlayer.TurnInJail < 3) {
                nonDoublesTurnOneTwo();
            }
            else {
                nonDoublesTurnThree();
            }
        }


        bool rolledDoubles = GameState.game.DiceInfo.RolledDoubles;
        if (rolledDoubles) {
            UIEventHub.Instance.call_CorrectOutcome();
            DataEventHub.Instance.call_LeaveJail();
            WaitFrames.Instance.exe(FRAMES_WAITED_FOR_LEAVING_JAIL, doublesRolled);
        }
        else {
            UIEventHub.Instance.call_IncorrectOutcome();
            nonDoublesRolled();
        }
    }
    private void payFiftyListener() {
        DataEventHub.Instance.call_MoneyAdjustment(GameState.game.TurnPlayer, -GameConstants.PRICE_FOR_LEAVING_JAIL);
        DataEventHub.Instance.call_LeaveJail();
        WaitFrames.Instance.exe(
            FRAMES_WAITED_FOR_LEAVING_JAIL,
            () => { payFiftyButtonClicked = true; }
        );
    }
    private void useCardListener(CardType cardType) {
        DataEventHub.Instance.call_LeaveJail();
        WaitFrames.Instance.exe(
            FRAMES_WAITED_FOR_LEAVING_JAIL,
            () => { useCardButtonClicked = true; }
        );
    }
    private void managePropertiesListener() {
        managePropertiesClicked = true;
    }
    private void tradeListener() {
        tradeClicked = true;
    }
    #endregion
}
