using UnityEngine;

[CreateAssetMenu(menuName = "State/DrawCardState")]
public class DrawCardState : State {
    [SerializeField] private SoundEvent cardSoundEvent;
    private bool okClicked;



    #region GameState
    public override void enterState() {
        okClicked = false;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        CardSpaceInfo cardSpaceInfo = (CardSpaceInfo)playerInfo.SpaceInfo;
        CardType cardType = cardSpaceInfo.CardType;
        DataEventHub.Instance.call_CardDrawn(cardType);
        ScreenAnimationEventHub.Instance.call_CardShown();
        cardSoundEvent.play();
        ScreenAnimationEventHub.Instance.sub_RemoveScreenAnimation(screenAnimationRemoved);
    }
    public override bool exitConditionMet() {
        return okClicked;
    }
    public override void exitState() {
        ScreenAnimationEventHub.Instance.unsub_RemoveScreenAnimation(screenAnimationRemoved);
    }
    public override State getNextState() {
        CardMechanicInfo cardMechanicInfo = GameState.game.DrawnCard.CardMechanicInfo;
        if (cardMechanicInfo is AdvanceToCardInfo) return allStates.getState<AdvanceToState>();
        if (cardMechanicInfo is BackThreeSpacesCardInfo) return allStates.getState<BackThreeState>();
        if (cardMechanicInfo is MoneyDifferenceCardInfo) return allStates.getState<MoneyCardState>();
        if (cardMechanicInfo is GoToNextRailroadCardInfo) return allStates.getState<NextRailroadState>();
        if (cardMechanicInfo is GoToNextUtilityCardInfo) return allStates.getState<NextUtilityState>();
        if (cardMechanicInfo is GetOutOfJailFreeCardInfo) return allStates.getState<GetOutOfJailFreeState>();
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion



    private void screenAnimationRemoved() {
        okClicked = true;
    }
}
