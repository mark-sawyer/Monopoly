using UnityEngine;

[CreateAssetMenu(menuName = "State/IncomeTaxState")]
public class IncomeTaxState : State {
    [SerializeField] private QuestionEventsAndPrefabs questionEvents;
    [SerializeField] private GameEvent questionAnsweredEvent;
    private bool questionAnswered;



    #region GameState
    public override void enterState() {
        questionAnswered = false;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        questionEvents.IncomeTaxQuestion.invoke(playerInfo);
        questionAnsweredEvent.Listeners += listenForAnswer;
    }
    public override bool exitConditionMet() {
        return questionAnswered;
    }
    public override void exitState() {
        questionAnsweredEvent.Listeners -= listenForAnswer;
    }
    public override State getNextState() {
        return getState<ResolveTurnState>();
    }
    #endregion


    private void listenForAnswer() {
        questionAnswered = true;
    }
}
