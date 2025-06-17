using UnityEngine;

[CreateAssetMenu(menuName = "State/IncomeTaxState")]
public class IncomeTaxState : State {
    [SerializeField] private GameEvent<PlayerInfo> incomeTaxQuestion;
    [SerializeField] private SoundEvent questionChime;
    private bool questionAnswered;



    #region GameState
    public override void enterState() {
        questionAnswered = false;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        incomeTaxQuestion.invoke(playerInfo);
        questionChime.play();
        ScreenAnimation.removeScreenAnimation.Listeners += screenAnimationRemoved;
    }
    public override bool exitConditionMet() {
        return questionAnswered;
    }
    public override void exitState() {
        ScreenAnimation.removeScreenAnimation.Listeners -= screenAnimationRemoved;
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    #endregion



    private void screenAnimationRemoved() {
        questionAnswered = true;
    }
}
