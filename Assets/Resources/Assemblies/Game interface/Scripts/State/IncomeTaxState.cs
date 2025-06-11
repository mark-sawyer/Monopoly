using UnityEngine;

[CreateAssetMenu(menuName = "State/IncomeTaxState")]
public class IncomeTaxState : State {
    [SerializeField] private GameEvent<PlayerInfo> incomeTaxQuestion;
    [SerializeField] private GameEvent questionAnsweredEvent;
    private bool questionAnswered;



    #region GameState
    public override void enterState() {
        questionAnswered = false;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        incomeTaxQuestion.invoke(playerInfo);
        questionAnsweredEvent.Listeners += listenForAnswer;
    }
    public override bool exitConditionMet() {
        return questionAnswered;
    }
    public override void exitState() {
        questionAnsweredEvent.Listeners -= listenForAnswer;
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    #endregion


    private void listenForAnswer() {
        questionAnswered = true;
    }
}
