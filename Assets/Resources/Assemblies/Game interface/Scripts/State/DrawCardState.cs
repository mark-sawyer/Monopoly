using UnityEngine;

[CreateAssetMenu(menuName = "State/DrawCardState")]
public class DrawCardState : State {
    [SerializeField] private GameEvent cardSoundEvent;
    [SerializeField] private CardTypeEvent cardDrawn;
    [SerializeField] private GameEvent cardShown;
    private bool okClicked;



    #region GameState
    public override void enterState() {
        okClicked = false;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        CardSpaceInfo cardSpaceInfo = (CardSpaceInfo)playerInfo.SpaceInfo;
        CardType cardType = cardSpaceInfo.CardType;
        cardDrawn.invoke(cardType);
        cardShown.invoke();
        cardSoundEvent.invoke();
        ScreenAnimation.removeScreenAnimation.Listeners += screenAnimationRemoved;
    }
    public override bool exitConditionMet() {
        return okClicked;
    }
    public override void exitState() {
        ScreenAnimation.removeScreenAnimation.Listeners -= screenAnimationRemoved;
    }
    public override State getNextState() {
        CardMechanicInfo cardMechanicInfo = GameState.game.DrawnCard.CardMechanicInfo;
        if (cardMechanicInfo is AdvanceToCardInfo) return allStates.getState<AdvanceToState>();
        if (cardMechanicInfo is BackThreeSpacesCardInfo) return allStates.getState<BackThreeState>();
        if (cardMechanicInfo is MoneyDifferenceCardInfo) return allStates.getState<MoneyCardState>();
        if (cardMechanicInfo is GoToNextRailroadCardInfo) return allStates.getState<NextRailroadState>();
        if (cardMechanicInfo is GoToNextUtilityCardInfo) return allStates.getState<NextUtilityState>();
        throw new System.Exception();
    }
    #endregion



    private void screenAnimationRemoved() {
        okClicked = true;
    }
}
