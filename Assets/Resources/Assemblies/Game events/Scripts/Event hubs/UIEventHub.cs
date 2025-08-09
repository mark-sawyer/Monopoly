using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/UIEventHub")]
public class UIEventHub : ScriptableObject {
    private static UIEventHub instance;
    [SerializeField] private GameEvent doublesTickBoxUpdate;
    [SerializeField] private GameEvent payFiftyButtonClicked;
    [SerializeField] private GameEvent tokenSettled;
    [SerializeField] private GameEvent cardDrop;
    [SerializeField] private GameEvent moneyAppearOrDisappear;
    [SerializeField] private FloatEvent fadeScreenCoverIn;
    [SerializeField] private GameEvent fadeScreenCoverOut;
    [SerializeField] private GameEvent tradingPlayerPlaced;
    [SerializeField] private GameEvent tradingPlayersConfirmed;
    [SerializeField] private GameEvent prerollStateStarting;
    [SerializeField] private GameEvent prerollStateEnding;
    [SerializeField] private ArrayPlayerEvent updateUIMoney;
    [SerializeField] private GameEvent updateExpiredPropertyVisuals;
    [SerializeField] private GameEvent updateExpiredBoardVisuals;
    [SerializeField] private GameEvent allExpiredPropertyVisualsUpdated;
    [SerializeField] private GameEvent playerEliminatedAnimationOver;



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
    public void call_CardDrop() => cardDrop.invoke();
    public void call_MoneyAppearOrDisappear() => moneyAppearOrDisappear.invoke();
    public void call_FadeScreenCoverIn(float alpha) => fadeScreenCoverIn.invoke(alpha);
    public void call_FadeScreenCoverOut() => fadeScreenCoverOut.invoke();
    public void call_TradingPlayerPlaced() => tradingPlayerPlaced.invoke();
    public void call_PrerollStateStarting() => prerollStateStarting.invoke();
    public void call_PrerollStateEnding() => prerollStateEnding.invoke();
    public void call_UpdateUIMoney(PlayerInfo[] players) => updateUIMoney.invoke(players);
    public void call_UpdateExpiredPropertyVisuals() => updateExpiredPropertyVisuals.invoke();
    public void call_UpdateExpiredBoardVisuals() => updateExpiredBoardVisuals.invoke();
    public void call_AllExpiredPropertyVisualsUpdated() => allExpiredPropertyVisualsUpdated.invoke();
    public void call_PlayerEliminatedAnimationOver() => playerEliminatedAnimationOver.invoke();
    #endregion



    #region Subscribing
    public void sub_DoublesTickBoxUpdate(Action a) => doublesTickBoxUpdate.Listeners += a;
    public void sub_PayFiftyButtonClicked(Action a) => payFiftyButtonClicked.Listeners += a;
    public void sub_TokenSettled(Action a) => tokenSettled.Listeners += a;
    public void sub_CardDrop(Action a) => cardDrop.Listeners += a;
    public void sub_MoneyAppearOrDisappear(Action a) => moneyAppearOrDisappear.Listeners += a;
    public void sub_FadeScreenCoverIn(Action<float> a) => fadeScreenCoverIn.Listeners += a;
    public void sub_FadeScreenCoverOut(Action a) => fadeScreenCoverOut.Listeners += a;
    public void sub_TradingPlayerPlaced(Action a) => tradingPlayerPlaced.Listeners += a;
    public void sub_TradingPlayersConfirmed(Action a) => tradingPlayersConfirmed.Listeners += a;
    public void sub_PrerollStateStarting(Action a) => prerollStateStarting.Listeners += a;
    public void sub_PrerollStateEnding(Action a) => prerollStateEnding.Listeners += a;
    public void sub_UpdateUIMoney(Action<PlayerInfo[]> a) => updateUIMoney.Listeners += a;
    public void sub_UpdateExpiredPropertyVisuals(Action a) => updateExpiredPropertyVisuals.Listeners += a;
    public void sub_UpdateExpiredBoardVisuals(Action a) => updateExpiredBoardVisuals.Listeners += a;
    public void sub_AllExpiredPropertyVisualsUpdated(Action a) => allExpiredPropertyVisualsUpdated.Listeners += a;
    public void sub_PlayerEliminatedAnimationOver(Action a) => playerEliminatedAnimationOver.Listeners += a;
    #endregion



    #region Unsubscribing
    public void unsub_DoublesTickBoxUpdate(Action a) => doublesTickBoxUpdate.Listeners -= a;
    public void unsub_PayFiftyButtonClicked(Action a) => payFiftyButtonClicked.Listeners -= a;
    public void unsub_TokenSettled(Action a) => tokenSettled.Listeners -= a;
    public void unsub_TradingPlayerPlaced(Action a) => tradingPlayerPlaced.Listeners -= a;
    public void unsub_TradingPlayersConfirmed(Action a) => tradingPlayersConfirmed.Listeners -= a;
    public void unsub_AllExpiredPropertyVisualsUpdated(Action a) => allExpiredPropertyVisualsUpdated.Listeners -= a;
    public void unsub_PlayerEliminatedAnimationOver(Action a) => playerEliminatedAnimationOver.Listeners += a;
    #endregion
}
