using UnityEngine;

[CreateAssetMenu(menuName = "State/JailPreRoll")]
public class JailPreRollState : State {
    private bool rollAnimationOver;
    private bool payFiftyButtonClicked;
    private bool useCardButtonClicked;
    private bool goToMoveToken;
    private bool goToUpdateTurnPlayer;
    private const int WAITED_FRAMES = 200;



    #region
    public override void enterState() {
        DataEventHub.Instance.call_TurnBegin(true);
        rollAnimationOver = false;
        payFiftyButtonClicked = false;
        useCardButtonClicked = false;
        goToMoveToken = false;
        goToUpdateTurnPlayer = false;
        UIEventHub.Instance.sub_RollButtonClicked(rollButtonListener);
        UIEventHub.Instance.sub_PayFiftyButtonClicked(payFiftyListener);
        UIEventHub.Instance.sub_UseGOOJFCardButtonClicked(useCardListener);
    }
    public override bool exitConditionMet() {
        return rollAnimationOver
            || payFiftyButtonClicked
            || useCardButtonClicked;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_RollButtonClicked(rollButtonListener);
        UIEventHub.Instance.unsub_PayFiftyButtonClicked(payFiftyListener);
        UIEventHub.Instance.unsub_UseGOOJFCardButtonClicked(useCardListener);

        if (payFiftyButtonClicked) {
            DataEventHub.Instance.call_MoneyAdjustment(GameState.game.TurnPlayer, -GameConstants.PRICE_FOR_LEAVING_JAIL);
            DataEventHub.Instance.call_LeaveJail();
        }
        else if (useCardButtonClicked) {
            DataEventHub.Instance.call_LeaveJail();
        }
    }
    public override State getNextState() {
        if (payFiftyButtonClicked || useCardButtonClicked) return allStates.getState<PreRollState>();
        if (goToMoveToken) return allStates.getState<MoveTokenState>();
        if (goToUpdateTurnPlayer) return allStates.getState<UpdateTurnPlayerState>();

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
            () => resolveDiceRollOutcomes(turnTokenVisual)
        );
    }
    private void resolveDiceRollOutcomes(TokenVisual turnTokenVisual) {
        void doublesRolled() {
            DataEventHub.Instance.call_DoublesCountReset();
            turnTokenVisual.prepForMoving();
            WaitFrames.Instance.exe(InterfaceConstants.FRAMES_FOR_TOKEN_GROWING, () => goToMoveToken = true);
        }
        void nonDoublesRolled() {
            if (GameState.game.TurnPlayer.TurnInJail < 3) {
                goToUpdateTurnPlayer = true;
            }
            else {
                DataEventHub.Instance.call_LeaveJail();
                DataEventHub.Instance.call_PlayerIncurredDebt(
                    GameState.game.TurnPlayer,
                    GameState.game.Bank,
                    GameConstants.PRICE_FOR_LEAVING_JAIL
                );
            }
        }


        bool rolledDoubles = GameState.game.DiceInfo.RolledDoubles;
        if (rolledDoubles) {
            UIEventHub.Instance.call_CorrectOutcome();
            DataEventHub.Instance.call_LeaveJail();
            WaitFrames.Instance.exe(WAITED_FRAMES, doublesRolled);
        }
        else {
            UIEventHub.Instance.call_IncorrectOutcome();
            WaitFrames.Instance.exe(WAITED_FRAMES, nonDoublesRolled);
        }
    }
    private void payFiftyListener() {
        payFiftyButtonClicked = true;
    }
    private void useCardListener(CardType cardType) {
        useCardButtonClicked = true;
    }
    #endregion
}
