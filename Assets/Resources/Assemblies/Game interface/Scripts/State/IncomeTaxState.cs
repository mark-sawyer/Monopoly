using UnityEngine;

[CreateAssetMenu(menuName = "State/IncomeTaxState")]
public class IncomeTaxState : State {
    [SerializeField] private QuestionEventRaiser questionEventRaiser;
    [SerializeField] private GameEvent questionAnsweredEvent;
    private bool questionAnswered;



    #region GameState
    public override void enterState() {
        questionAnswered = false;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        questionEventRaiser.invokeIncomeTaxQuestion(playerInfo);
        questionAnsweredEvent.Listeners += listenForAnswer;
    }
    public override bool exitConditionMet() {
        return questionAnswered;
    }
    public override void exitState() {
        questionAnsweredEvent.Listeners -= listenForAnswer;
    }
    public override State getNextState() {
        return possibleNextStates[0];
    }
    #endregion


    private void listenForAnswer() {
        questionAnswered = true;
    }
}
