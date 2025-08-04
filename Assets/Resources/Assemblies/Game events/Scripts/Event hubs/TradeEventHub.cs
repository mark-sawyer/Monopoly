using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/TradeEventHub")]
public class TradeEventHub : ScriptableObject {
    private static TradeEventHub instance;
    [SerializeField] private GameEvent tradeChanged;
    [SerializeField] private GameEvent agreeClicked;
    [SerializeField] private GameEvent tradeConditionsMet;
    [SerializeField] private IntEvent numberedButtonClicked;
    [SerializeField] private GameEvent handshakeComplete;



    #region public
    public static TradeEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<TradeEventHub>(
                    "ScriptableObjects/Events/0. Hubs/trade_event_hub"
                );
            }
            return instance;
        }
    }
    #endregion



    #region Invoking
    public void call_TradeChanged() => tradeChanged.invoke();
    public void call_AgreeClicked() => agreeClicked.invoke();
    public void call_TradeConditionsMet() => tradeConditionsMet.invoke();
    public void call_NumberedButtonClicked(int i) => numberedButtonClicked.invoke(i);
    public void call_HandshakeComplete() => handshakeComplete.invoke();
    #endregion



    #region Subscribing
    public void sub_TradeChanged(Action a) => tradeChanged.Listeners += a;
    public void sub_AgreeClicked(Action a) => agreeClicked.Listeners += a;
    public void sub_TradeConditionsMet(Action a) => tradeConditionsMet.Listeners += a;
    public void sub_NumberedButtonClicked(Action<int> a) => numberedButtonClicked.Listeners += a;
    public void sub_HandshakeComplete(Action a) => handshakeComplete.Listeners += a;
    #endregion



    #region Unsubscribing
    public void unsub_TradeChanged(Action a) => tradeChanged.Listeners -= a;
    public void unsub_AgreeClicked(Action a) => agreeClicked.Listeners -= a;
    public void unsub_TradeConditionsMet(Action a) => tradeConditionsMet.Listeners -= a;
    public void unsub_NumberedButtonClicked(Action<int> a) => numberedButtonClicked.Listeners -= a;
    public void unsub_HandshakeComplete(Action a) => handshakeComplete.Listeners -= a;
    #endregion
}
