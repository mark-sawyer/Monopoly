using UnityEngine;

public class BuyPropertyOptionState : State {
    [SerializeField] QuestionEventRaiser questionEventRaiser;


    #region GameState
    public override void enterState() {
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        PropertyInfo propertyInfo = ((PropertySpaceInfo)GameState.game.SpaceInfoOfTurnPlayer).PropertyInfo;
        questionEventRaiser.invokePurchaseQuestion(playerInfo, propertyInfo);
    }
    public override bool exitConditionMet() {
        return false;
    }
    public override void exitState() {

    }
    public override State getNextState() {
        return possibleNextStates[0];
    }
    #endregion
}
