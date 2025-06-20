using UnityEngine;

[CreateAssetMenu(menuName = "State/JailRollAnimation")]
public class JailRollAnimationState : State {
    [SerializeField] private GameEvent doublesCountReset;
    [SerializeField] private GameEvent diceAnimationOver;
    [SerializeField] private GameEvent leaveJail;
    [SerializeField] private PlayerCreditorIntEvent playerIncurredDebt;
    [SerializeField] private SoundEvent correct;
    [SerializeField] private SoundEvent incorrect;
    private bool goToMoveToken;
    private bool goToUpdateTurnPlayer;
    private const int WAITED_FRAMES = 200;
    



    #region State
    public override void enterState() {
        diceAnimationOver.Listeners += animationOverCalled;
        goToMoveToken = false;
        goToUpdateTurnPlayer = false;
    }
    public override bool exitConditionMet() {
        return goToMoveToken || goToUpdateTurnPlayer;
    }
    public override void exitState() {
        diceAnimationOver.Listeners -= animationOverCalled;
    }
    public override State getNextState() {
        if (goToMoveToken) return allStates.getState<MoveTokenState>();
        if (goToUpdateTurnPlayer) return allStates.getState<UpdateTurnPlayerState>();

        throw new System.Exception();
    }
    #endregion



    #region private
    private void animationOverCalled() {
        bool rolledDoubles = GameState.game.DiceInfo.RolledDoubles;
        if (rolledDoubles) {
            correct.play();
            leaveJail.invoke();
            WaitFrames.Instance.exe(WAITED_FRAMES, doublesRolled);
        }
        else {
            incorrect.play();
            WaitFrames.Instance.exe(WAITED_FRAMES, nonDoublesRolled);            
        }
    }
    private void doublesRolled() {
        doublesCountReset.invoke();
        int playerIndex = GameState.game.IndexOfTurnPlayer;
        TokenScaler tokenScaler = TokenVisualManager.Instance.getTokenScaler(playerIndex);
        tokenScaler.beginScaleChange(InterfaceConstants.SCALE_FOR_MOVING_TOKEN);
        WaitFrames.Instance.exe(InterfaceConstants.FRAMES_FOR_TOKEN_GROWING, () => goToMoveToken = true);
    }
    private void nonDoublesRolled() {
        if (GameState.game.TurnPlayer.TurnInJail < 3) {
            goToUpdateTurnPlayer = true;
        }
        else {
            leaveJail.invoke();
            playerIncurredDebt.invoke(
                GameState.game.TurnPlayer,
                GameState.game.Bank,
                GameConstants.PRICE_FOR_LEAVING_JAIL
            );
        }
    }
    #endregion
}
