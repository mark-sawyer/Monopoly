using UnityEngine;

[CreateAssetMenu(menuName = "State/BuyPropertyOptionState")]
public class BuyPropertyOptionState : State {
    private ScreenAnimationEventHub screenAnimationEvents;
    private bool purchaseAccepted;
    private bool purchaseDeclined;



    #region GameState
    public override void enterState() {
        if (screenAnimationEvents == null) screenAnimationEvents = ScreenAnimationEventHub.Instance;
        purchaseAccepted = false;
        purchaseDeclined = false;

        screenAnimationEvents.sub_RemoveScreenAnimation(purchaseAcceptedCalled);
        screenAnimationEvents.sub_RemoveScreenAnimationKeepCover(purchaseDeclinedCalled);

        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        PropertyInfo propertyInfo = ((PropertySpaceInfo)GameState.game.SpaceInfoOfTurnPlayer).PropertyInfo;
        screenAnimationEvents.call_PurchaseQuestion(playerInfo, propertyInfo);
    }
    public override bool exitConditionMet() {
        return purchaseAccepted || purchaseDeclined;
    }
    public override void exitState() {
        screenAnimationEvents.unsub_RemoveScreenAnimation(purchaseAcceptedCalled);
        screenAnimationEvents.unsub_RemoveScreenAnimationKeepCover(purchaseDeclinedCalled);
    }
    public override State getNextState() {
        if (purchaseAccepted) return allStates.getState<UpdateTurnPlayerState>();
        else return allStates.getState<AuctionPropertyState>();
    }
    #endregion


    
    #region private
    private void purchaseAcceptedCalled() {
        WaitFrames.Instance.exe(
            100,  // Waiting for the UI update and bloop sound (occurs in PurchaseQuestion).
            () => { purchaseAccepted = true; }
        );
    }
    private void purchaseDeclinedCalled() {
        purchaseDeclined = true;
    }
    #endregion
}
