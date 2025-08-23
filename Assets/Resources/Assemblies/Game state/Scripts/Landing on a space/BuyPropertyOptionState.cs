using UnityEngine;

[CreateAssetMenu(menuName = "State/BuyPropertyOptionState")]
internal class BuyPropertyOptionState : State {
    private bool purchaseAccepted;
    private bool purchaseDeclined;



    #region GameState
    public override void enterState() {
        purchaseAccepted = false;
        purchaseDeclined = false;

        ScreenOverlayFunctionEventHub.Instance.sub_PurchaseYesClicked(yesClicked);
        ScreenOverlayFunctionEventHub.Instance.sub_PurchaseNoClicked(noClicked);

        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        PropertyInfo propertyInfo = ((PropertySpaceInfo)GameState.game.TurnPlayer.SpaceInfo).PropertyInfo;
        ScreenOverlayStarterEventHub.Instance.call_PurchaseQuestion(playerInfo, propertyInfo);
    }
    public override bool exitConditionMet() {
        return purchaseAccepted
            || purchaseDeclined;
    }
    public override void exitState() {
        ScreenOverlayFunctionEventHub.Instance.unsub_PurchaseYesClicked(yesClicked);
        ScreenOverlayFunctionEventHub.Instance.unsub_PurchaseNoClicked(noClicked);
    }
    public override State getNextState() {
        if (purchaseAccepted) return allStates.getState<PrerollState>();
        else return allStates.getState<AuctionPropertyState>();
    }
    #endregion


    
    #region private
    private void yesClicked() {
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        PropertyInfo propertyInfo = ((PropertySpaceInfo)turnPlayer.SpaceInfo).PropertyInfo;
        DataUIPipelineEventHub.Instance.call_MoneyAdjustment(turnPlayer, -propertyInfo.Cost);
        ScreenOverlayFunctionEventHub.Instance.call_RemoveScreenOverlay();
        AccompanyingVisualSpawner.Instance.removeObjectAndResetPosition();
        WaitFrames.Instance.beforeAction(
            FrameConstants.MONEY_UPDATE,
            () => {
                DataUIPipelineEventHub.Instance.call_PlayerObtainedProperty(turnPlayer, propertyInfo);
                purchaseAccepted = true;
            }
        );
    }
    private void noClicked() {
        ScreenOverlayFunctionEventHub.Instance.call_RemoveScreenOverlayKeepCover();
        purchaseDeclined = true;
    }
    #endregion
}
