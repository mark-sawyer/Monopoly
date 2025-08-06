using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/UIEventHub")]
public class UIEventHub : ScriptableObject {
    private static UIEventHub instance;
    #region UI only
    [SerializeField] private GameEvent doublesTickBoxUpdate;
    [SerializeField] private GameEvent payFiftyButtonClicked;
    [SerializeField] private GameEvent tokenSettled;
    [SerializeField] private GameEvent correctOutcome;
    [SerializeField] private GameEvent incorrectOutcome;
    [SerializeField] private GameEvent appearingPop;
    [SerializeField] private GameEvent cardDrop;
    [SerializeField] private GameEvent moneyAppearOrDisappear;
    [SerializeField] private GameEvent buttonDown;
    [SerializeField] private GameEvent buttonUp;
    [SerializeField] private FloatEvent fadeScreenCoverIn;
    [SerializeField] private GameEvent fadeScreenCoverOut;
    [SerializeField] private GameEvent tradingPlayerPlaced;
    [SerializeField] private GameEvent tradingPlayersConfirmed;
    [SerializeField] private GameEvent preRollStateStarting;
    [SerializeField] private GameEvent jailPreRollStateStarting;
    #endregion



    #region public
    public static UIEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<UIEventHub>(
                    "ScriptableObjects/Events/0. Hubs/ui_event_hub"
                );
            }
            return instance;
        }
    }
    #endregion



    #region UI invoking
    public void call_DoublesTickBoxUpdate() => doublesTickBoxUpdate.invoke();
    public void call_PayFiftyButtonClicked() => payFiftyButtonClicked.invoke();
    public void call_TokenSettled() => tokenSettled.invoke();
    public void call_CorrectOutcome() => correctOutcome.invoke();
    public void call_IncorrectOutcome() => incorrectOutcome.invoke();
    public void call_AppearingPop() => appearingPop.invoke();
    public void call_CardDrop() => cardDrop.invoke();
    public void call_MoneyAppearOrDisappear() => moneyAppearOrDisappear.invoke();
    public void call_ButtonDown() => buttonDown.invoke();
    public void call_ButtonUp() => buttonUp.invoke();
    public void call_FadeScreenCoverIn(float alpha) => fadeScreenCoverIn.invoke(alpha);
    public void call_FadeScreenCoverOut() => fadeScreenCoverOut.invoke();
    public void call_TradingPlayerPlaced() => tradingPlayerPlaced.invoke();
    public void call_PreRollStateStarting() => preRollStateStarting.invoke();
    public void call_JailPreRollStateStarting() => jailPreRollStateStarting.invoke();
    #endregion



    #region Subscribing
    public void sub_DoublesTickBoxUpdate(Action a) => doublesTickBoxUpdate.Listeners += a;
    public void sub_PayFiftyButtonClicked(Action a) => payFiftyButtonClicked.Listeners += a;
    public void sub_TokenSettled(Action a) => tokenSettled.Listeners += a;
    public void sub_CorrectOutcome(Action a) => correctOutcome.Listeners += a;
    public void sub_IncorrectOutcome(Action a) => incorrectOutcome.Listeners += a;
    public void sub_AppearingPop(Action a) => appearingPop.Listeners += a;
    public void sub_CardDrop(Action a) => cardDrop.Listeners += a;
    public void sub_MoneyAppearOrDisappear(Action a) => moneyAppearOrDisappear.Listeners += a;
    public void sub_ButtonDown(Action a) => buttonDown.Listeners += a;
    public void sub_ButtonUp(Action a) => buttonUp.Listeners += a;
    public void sub_FadeScreenCoverIn(Action<float> a) => fadeScreenCoverIn.Listeners += a;
    public void sub_FadeScreenCoverOut(Action a) => fadeScreenCoverOut.Listeners += a;
    public void sub_TradingPlayerPlaced(Action a) => tradingPlayerPlaced.Listeners += a;
    public void sub_TradingPlayersConfirmed(Action a) => tradingPlayersConfirmed.Listeners += a;
    public void sub_PreRollStateStarting(Action a) => preRollStateStarting.Listeners += a;
    public void sub_JailPreRollStateStarting(Action a) => jailPreRollStateStarting.Listeners += a;
    #endregion



    #region Unsubscribing
    public void unsub_DoublesTickBoxUpdate(Action a) => doublesTickBoxUpdate.Listeners -= a;
    public void unsub_PayFiftyButtonClicked(Action a) => payFiftyButtonClicked.Listeners -= a;
    public void unsub_TokenSettled(Action a) => tokenSettled.Listeners -= a;
    public void unsub_JailPreRollStateStarting(Action a) => jailPreRollStateStarting.Listeners -= a;
    public void unsub_TradingPlayerPlaced(Action a) => tradingPlayerPlaced.Listeners -= a;
    public void unsub_TradingPlayersConfirmed(Action a) => tradingPlayersConfirmed.Listeners -= a;
    #endregion
}
