using UnityEngine;

[CreateAssetMenu(menuName = "State/IncomeTaxState")]
public class IncomeTaxState : State {
    [SerializeField] private SoundEvent questionChime;
    private bool questionAnswered;



    #region GameState
    public override void enterState() {
        questionAnswered = false;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        ScreenAnimationEventHub.Instance.call_IncomeTaxQuestion(playerInfo);
        questionChime.play();
        ScreenAnimationEventHub.Instance.sub_RemoveScreenAnimation(screenAnimationRemoved);
    }
    public override bool exitConditionMet() {
        return questionAnswered;
    }
    public override void exitState() {
        ScreenAnimationEventHub.Instance.unsub_RemoveScreenAnimation(screenAnimationRemoved);
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    #endregion



    private void screenAnimationRemoved() {
        questionAnswered = true;
    }
}
