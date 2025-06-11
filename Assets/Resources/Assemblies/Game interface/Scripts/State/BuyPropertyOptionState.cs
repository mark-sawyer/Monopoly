using UnityEngine;

[CreateAssetMenu(menuName = "State/BuyPropertyOptionState")]
public class BuyPropertyOptionState : State {
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> purchaseQuestion;
    [SerializeField] private GameEvent questionAnsweredEvent;
    private bool questionAnswered;



    #region GameState
    public override void enterState() {
        questionAnswered = false;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        PropertyInfo propertyInfo = ((PropertySpaceInfo)GameState.game.SpaceInfoOfTurnPlayer).PropertyInfo;
        purchaseQuestion.invoke(playerInfo, propertyInfo);
        questionAnsweredEvent.Listeners += listenForAnswer;
    }
    public override bool exitConditionMet() {
        return questionAnswered;
    }
    public override void exitState() {
        questionAnsweredEvent.Listeners -= listenForAnswer;
    }
    public override State getNextState() {
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion



    private void listenForAnswer() {
        questionAnswered = true;
    }
}
