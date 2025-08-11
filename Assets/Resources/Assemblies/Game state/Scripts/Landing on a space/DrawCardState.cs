using UnityEngine;

[CreateAssetMenu(menuName = "State/DrawCardState")]
internal class DrawCardState : State {
    private bool okClicked;



    #region GameState
    public override void enterState() {
        okClicked = false;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        CardSpaceInfo cardSpaceInfo = (CardSpaceInfo)playerInfo.SpaceInfo;
        CardType cardType = cardSpaceInfo.CardType;
        DataEventHub.Instance.call_CardDrawn(cardType);
        ScreenOverlayEventHub.Instance.call_CardShown();
        ScreenOverlayEventHub.Instance.sub_RemoveScreenOverlay(screenAnimationRemoved);
    }
    public override bool exitConditionMet() {
        return okClicked;
    }
    public override void exitState() {
        ScreenOverlayEventHub.Instance.unsub_RemoveScreenOverlay(screenAnimationRemoved);
    }
    public override State getNextState() {
        CardMechanicInfo cardMechanicInfo = GameState.game.DrawnCard.CardMechanicInfo;
        if (cardMechanicInfo is AdvanceToCardInfo) return allStates.getState<AdvanceToState>();
        if (cardMechanicInfo is BackThreeSpacesCardInfo) return allStates.getState<BackThreeState>();
        if (cardMechanicInfo is MoneyDifferenceCardInfo) return allStates.getState<MoneyCardState>();
        if (cardMechanicInfo is GoToNextRailroadCardInfo) return allStates.getState<NextRailroadState>();
        if (cardMechanicInfo is GoToNextUtilityCardInfo) return allStates.getState<NextUtilityState>();
        if (cardMechanicInfo is GetOutOfJailFreeCardInfo) return allStates.getState<GetOutOfJailFreeState>();
        if (cardMechanicInfo is BuildingCostCardInfo) return allStates.getState<BuildingCostState>();
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion



    private void screenAnimationRemoved() {
        okClicked = true;
    }
}
