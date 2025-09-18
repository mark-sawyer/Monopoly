using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/UIEventHub")]
public class UIEventHub : ScriptableObject {
    private static UIEventHub instance;
    [SerializeField] private GameEvent startGameClicked;
    [SerializeField] private GameEvent doublesTickBoxUpdate;
    [SerializeField] private GameEvent payFiftyButtonClicked;
    [SerializeField] private GameEvent tokenSettled;
    [SerializeField] private FloatEvent fadeScreenCoverIn;
    [SerializeField] private GameEvent fadeScreenCoverOut;
    [SerializeField] private GameEvent prerollStateStarting;
    [SerializeField] private GameEvent prerollStateEnding;
    [SerializeField] private PlayerArrayEvent updateUIMoney;
    [SerializeField] private PlayerPropertyEvent playerPropertyAdjustment;
    [SerializeField] private GameEvent updateExpiredIconVisuals;
    [SerializeField] private GameEvent updateIconsAfterResolveDebt;
    [SerializeField] private GameEvent updateExpiredBoardVisuals;
    [SerializeField] private GameEvent allExpiredPropertyVisualsUpdated;
    [SerializeField] private GameEvent playerEliminatedAnimationOver;
    [SerializeField] private GameEvent escapeClicked;
    [SerializeField] private IntIntEvent nonTurnDiceRoll;



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
    public void call_StartGameClicked() => startGameClicked.invoke();
    public void call_DoublesTickBoxUpdate() => doublesTickBoxUpdate.invoke();
    public void call_PayFiftyButtonClicked() => payFiftyButtonClicked.invoke();
    public void call_TokenSettled() => tokenSettled.invoke();
    public void call_FadeScreenCoverIn(float alpha) => fadeScreenCoverIn.invoke(alpha);
    public void call_FadeScreenCoverOut() => fadeScreenCoverOut.invoke();
    public void call_PrerollStateStarting() => prerollStateStarting.invoke();
    public void call_PrerollStateEnding() => prerollStateEnding.invoke();
    public void call_UpdateUIMoney(PlayerInfo[] players) => updateUIMoney.invoke(players);
    public void call_PlayerPropertyAdjustment(PlayerInfo pl, PropertyInfo pr) => playerPropertyAdjustment.invoke(pl, pr);  // Subscribed to in UI pipeline
    public void call_UpdateExpiredIconVisuals() => updateExpiredIconVisuals.invoke();
    public void call_UpdateIconsAfterResolveDebt() => updateIconsAfterResolveDebt.invoke();
    public void call_UpdateExpiredBoardVisuals() => updateExpiredBoardVisuals.invoke();
    public void call_AllExpiredPropertyVisualsUpdated() => allExpiredPropertyVisualsUpdated.invoke();
    public void call_PlayerEliminatedAnimationOver() => playerEliminatedAnimationOver.invoke();
    public void call_EscapeClicked() => escapeClicked.invoke();
    public void call_NonTurnDiceRoll(int value1, int value2) => nonTurnDiceRoll.invoke(value1, value2);
    #endregion



    #region Subscribing
    public void sub_StartGameClicked(Action a) => startGameClicked.Listeners += a;
    public void sub_DoublesTickBoxUpdate(Action a) => doublesTickBoxUpdate.Listeners += a;
    public void sub_PayFiftyButtonClicked(Action a) => payFiftyButtonClicked.Listeners += a;
    public void sub_TokenSettled(Action a) => tokenSettled.Listeners += a;
    public void sub_FadeScreenCoverIn(Action<float> a) => fadeScreenCoverIn.Listeners += a;
    public void sub_FadeScreenCoverOut(Action a) => fadeScreenCoverOut.Listeners += a;
    public void sub_PrerollStateStarting(Action a) => prerollStateStarting.Listeners += a;
    public void sub_PrerollStateEnding(Action a) => prerollStateEnding.Listeners += a;
    public void sub_UpdateUIMoney(Action<PlayerInfo[]> a) => updateUIMoney.Listeners += a;
    public void sub_UpdateExpiredIconVisuals(Action a) => updateExpiredIconVisuals.Listeners += a;
    public void sub_UpdateIconsAfterResolveDebt(Action a) => updateIconsAfterResolveDebt.Listeners += a;
    public void sub_UpdateExpiredBoardVisuals(Action a) => updateExpiredBoardVisuals.Listeners += a;
    public void sub_AllExpiredPropertyVisualsUpdated(Action a) => allExpiredPropertyVisualsUpdated.Listeners += a;
    public void sub_PlayerEliminatedAnimationOver(Action a) => playerEliminatedAnimationOver.Listeners += a;
    public void sub_EscapeClicked(Action a) => escapeClicked.Listeners += a;
    public void sub_NonTurnDiceRoll(Action<int, int> a) => nonTurnDiceRoll.Listeners += a;
    #endregion



    #region Unsubscribing
    public void unsub_StartGameClicked(Action a) => startGameClicked.Listeners -= a;
    public void unsub_PrerollStateStarting(Action a) => prerollStateStarting.Listeners -= a;
    public void unsub_PrerollStateEnding(Action a) => prerollStateEnding.Listeners -= a;
    public void unsub_DoublesTickBoxUpdate(Action a) => doublesTickBoxUpdate.Listeners -= a;
    public void unsub_PayFiftyButtonClicked(Action a) => payFiftyButtonClicked.Listeners -= a;
    public void unsub_TokenSettled(Action a) => tokenSettled.Listeners -= a;
    public void unsub_AllExpiredPropertyVisualsUpdated(Action a) => allExpiredPropertyVisualsUpdated.Listeners -= a;
    public void unsub_PlayerEliminatedAnimationOver(Action a) => playerEliminatedAnimationOver.Listeners -= a;
    public void unsub_EscapeClicked(Action a) => escapeClicked.Listeners -= a;
    #endregion
}
