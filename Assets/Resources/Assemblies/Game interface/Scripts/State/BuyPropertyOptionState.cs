using UnityEngine;

[CreateAssetMenu(menuName = "State/BuyPropertyOptionState")]
public class BuyPropertyOptionState : State {
    [SerializeField] private PlayerPropertyEvent purchaseQuestion;
    [SerializeField] private SoundEvent questionChime;
    private bool questionAnswered;



    #region GameState
    public override void enterState() {
        questionAnswered = false;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        PropertyInfo propertyInfo = ((PropertySpaceInfo)GameState.game.SpaceInfoOfTurnPlayer).PropertyInfo;
        purchaseQuestion.invoke(playerInfo, propertyInfo);
        questionChime.play();
        ScreenAnimation.removeScreenAnimation.Listeners += screenAnimationRemoved;
    }
    public override bool exitConditionMet() {
        return questionAnswered;
    }
    public override void exitState() {
        ScreenAnimation.removeScreenAnimation.Listeners -= screenAnimationRemoved;
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
