using UnityEngine;

[CreateAssetMenu(menuName = "State/BuyPropertyOptionState")]
public class BuyPropertyOptionState : State {
    [SerializeField] private SoundEvent questionChime;
    private bool questionAnswered;



    #region GameState
    public override void enterState() {
        questionAnswered = false;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        PropertyInfo propertyInfo = ((PropertySpaceInfo)GameState.game.SpaceInfoOfTurnPlayer).PropertyInfo;
        ScreenAnimationEventHub.Instance.call_PurchaseQuestion(playerInfo, propertyInfo);
        questionChime.play();
        ScreenAnimationEventHub.Instance.sub_RemoveScreenAnimation(screenAnimationRemoved);
    }
    public override bool exitConditionMet() {
        return questionAnswered;
    }
    public override void exitState() {
        ScreenAnimationEventHub.Instance.unsub_RemoveScreenAnimation(screenAnimationRemoved);
    }
    public override State getNextState() {
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion



    private void screenAnimationRemoved() {
        WaitFrames.Instance.exe(
            100,  // Waiting for the UI update and bloop sound (occurs in PurchaseQuestion).
            () => { questionAnswered = true; }
        );
    }
}
