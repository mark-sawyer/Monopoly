using UnityEngine;

[CreateAssetMenu(menuName = "State/BuyPropertyOptionState")]
public class BuyPropertyOptionState : State {
    [SerializeField] QuestionEventsAndPrefabs questionEvents;
    [SerializeField] GameEvent questionAnsweredEvent;
    private bool questionAnswered;



    #region GameState
    public override void enterState() {
        questionAnswered = false;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        PropertyInfo propertyInfo = ((PropertySpaceInfo)GameState.game.SpaceInfoOfTurnPlayer).PropertyInfo;
        questionEvents.PurchaseQuestion.invoke(playerInfo, propertyInfo);
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
