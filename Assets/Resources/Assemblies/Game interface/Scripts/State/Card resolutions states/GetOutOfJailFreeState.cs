using UnityEngine;

[CreateAssetMenu(menuName = "State/GetOutOfJailFreeState")]
public class GetOutOfJailFreeState : State {
    [SerializeField] private PlayerCardEvent playerGetsGOOJFCard;
    [SerializeField] private GameEvent cardResolved;
    [SerializeField] private SoundEvent pop;
    private bool cardResolveInvoked;



    #region State
    public override void enterState() {
        cardResolveInvoked = false;
        CardInfo cardInfo = GameState.game.DrawnCard;
        WaitFrames.Instance.exe(40, pop.play);
        WaitFrames.Instance.exe(50, () => {
            playerGetsGOOJFCard.invoke(GameState.game.TurnPlayer, cardInfo);
            cardResolved.invoke();
            cardResolveInvoked = true;
        });
    }
    public override bool exitConditionMet() {
        return cardResolveInvoked;
    }
    public override State getNextState() {
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion
}
