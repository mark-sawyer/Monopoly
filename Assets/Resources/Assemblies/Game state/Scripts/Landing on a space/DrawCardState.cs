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
        CardInfo cardInfo = GameState.game.DrawnCard;
        CardMechanicInfo cardMechanicInfo = cardInfo.CardMechanicInfo;

        if (cardMechanicInfo is GoToJailCardInfo) SoundPlayer.Instance.play_Whistle();
        else SoundPlayer.Instance.play_CardDrawn();
        ScreenOverlayStarterEventHub.Instance.call_CardShown();
        ScreenOverlayFunctionEventHub.Instance.sub_RemoveScreenOverlay(screenAnimationRemoved);
    }
    public override bool exitConditionMet() {
        return okClicked;
    }
    public override void exitState() {
        ScreenOverlayFunctionEventHub.Instance.unsub_RemoveScreenOverlay(screenAnimationRemoved);
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
        if (cardMechanicInfo is GoToJailCardInfo) return allStates.getState<GoToJailCardState>();
        if (cardMechanicInfo is PlayerMoneyDifferenceCardInfo) return allStates.getState<PlayerMoneyCardState>();
        throw new System.Exception("Card mechanic not found.");
    }
    #endregion



    private void screenAnimationRemoved() {
        okClicked = true;
    }
}
