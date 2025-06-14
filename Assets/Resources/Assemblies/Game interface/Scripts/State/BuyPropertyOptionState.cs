using UnityEngine;

[CreateAssetMenu(menuName = "State/BuyPropertyOptionState")]
public class BuyPropertyOptionState : State {
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> purchaseQuestion;
    [SerializeField] private GameEvent questionAskedEvent;
    private bool questionAnswered;



    #region GameState
    public override void enterState() {
        questionAnswered = false;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        PropertyInfo propertyInfo = ((PropertySpaceInfo)GameState.game.SpaceInfoOfTurnPlayer).PropertyInfo;
        purchaseQuestion.invoke(playerInfo, propertyInfo);
        questionAskedEvent.invoke();
        ScreenAnimation.removeScreenAnimation.Listeners += screenAnimationRemoved;
    }
    public override bool exitConditionMet() {
        return questionAnswered;
    }
    public override void exitState() {
        ScreenAnimation.removeScreenAnimation.Listeners -= screenAnimationRemoved;
    }
    public override State getNextState() {
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion



    private void screenAnimationRemoved() {
        questionAnswered = true;
    }
}
