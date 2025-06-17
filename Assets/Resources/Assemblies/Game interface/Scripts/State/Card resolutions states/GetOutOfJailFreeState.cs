using UnityEngine;

[CreateAssetMenu(menuName = "State/GetOutOfJailFreeState")]
public class GetOutOfJailFreeState : State {
    [SerializeField] private PlayerCardEvent playerGetsGOOJFCard;
    [SerializeField] private GameEvent cardResolved;



    #region State
    public override void enterState() {
        CardInfo cardInfo = GameState.game.DrawnCard;
        playerGetsGOOJFCard.invoke(GameState.game.TurnPlayer, cardInfo);
        cardResolved.invoke();
    }
    public override bool exitConditionMet() {
        return true;
    }
    public override State getNextState() {
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion
}
