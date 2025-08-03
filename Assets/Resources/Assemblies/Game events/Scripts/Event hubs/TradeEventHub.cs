using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/TradeEventHub")]
public class TradeEventHub : ScriptableObject {
    private static TradeEventHub instance;
    [SerializeField] private GameEvent tradeChanged;
    [SerializeField] private GameEvent agreeClicked;



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
    #endregion



    #region Subscribing
    public void sub_TradeChanged(Action a) => tradeChanged.Listeners += a;
    public void sub_AgreeClicked(Action a) => agreeClicked.Listeners += a;
    #endregion



    #region Unsubscribing
    public void unsub_TradeChanged(Action a) => tradeChanged.Listeners -= a;
    public void unsub_AgreeClicked(Action a) => agreeClicked.Listeners -= a;
    #endregion
}
