using UnityEngine;

[CreateAssetMenu(menuName = "State/JailPreRoll")]
public class JailPreRollState : State {
    #region Button events
    [SerializeField] private GameEvent rollButtonClickedEvent;
    [SerializeField] private GameEvent payFiftyButtonClickedEvent;
    [SerializeField] private CardTypeEvent useCardButtonClickedEvent;
    #endregion
    #region Button bools
    private bool rollButtonClicked;
    private bool payFiftyButtonClicked;
    private bool useCardButtonClicked;
    #endregion
    [SerializeField] private GameEvent jailTurnBegin;
    [SerializeField] private PlayerIntEvent moneyAdjustment;
    [SerializeField] private GameEvent leaveJail;
    [SerializeField] private SoundEvent moneyChing;



    #region
    public override void enterState() {
        jailTurnBegin.invoke();
        rollButtonClicked = false;
        payFiftyButtonClicked = false;
        useCardButtonClicked = false;
        rollButtonClickedEvent.Listeners += rollButtonListener;
        payFiftyButtonClickedEvent.Listeners += payFiftyListener;
        useCardButtonClickedEvent.Listeners += useCardListener;
    }
    public override bool exitConditionMet() {
        return rollButtonClicked
            || payFiftyButtonClicked
            || useCardButtonClicked;
    }
    public override void exitState() {
        rollButtonClickedEvent.Listeners -= rollButtonListener;
        payFiftyButtonClickedEvent.Listeners -= payFiftyListener;
        useCardButtonClickedEvent.Listeners -= useCardListener;

        if (payFiftyButtonClicked) {
            moneyAdjustment.invoke(GameState.game.TurnPlayer, -GameConstants.PRICE_FOR_LEAVING_JAIL);
            moneyChing.play();
            leaveJail.invoke();
        }
        else if (useCardButtonClicked) {
            leaveJail.invoke();
        }
    }
    public override State getNextState() {
        if (payFiftyButtonClicked || useCardButtonClicked) return allStates.getState<PreRollState>();
        else return allStates.getState<JailRollAnimationState>();
    }
    #endregion



    #region private
    private void rollButtonListener() {
        rollButtonClicked = true;
    }
    private void payFiftyListener() {
        payFiftyButtonClicked = true;
    }
    private void useCardListener(CardType cardType) {
        useCardButtonClicked = true;
    }
    #endregion
}
