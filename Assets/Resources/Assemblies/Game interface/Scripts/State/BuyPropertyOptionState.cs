using UnityEngine;

[CreateAssetMenu(menuName = "State/BuyPropertyOptionState")]
public class BuyPropertyOptionState : State {
    [SerializeField] QuestionEventRaiser questionEventRaiser;
    [SerializeField] GameEvent questionAnsweredEvent;
    private bool questionAnswered;


    #region GameState
    public override void enterState() {
        questionAnswered = false;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        PropertyInfo propertyInfo = ((PropertySpaceInfo)GameState.game.SpaceInfoOfTurnPlayer).PropertyInfo;
        questionEventRaiser.invokePurchaseQuestion(playerInfo, propertyInfo);
        questionAnsweredEvent.Listeners += listenForAnswer;
    }
    public override bool exitConditionMet() {
        return questionAnswered;
    }
    public override State getNextState() {
        return possibleNextStates[0];
    }
    #endregion


    private void listenForAnswer() {
        questionAnswered = true;

    }
}
