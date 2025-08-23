using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/TradeEventHub")]
public class TradeEventHub : ScriptableObject {
    private static TradeEventHub instance;
    [SerializeField] private GameEvent tradeChanged;
    [SerializeField] private GameEvent tradeConditionsMet;
    [SerializeField] private IntEvent numberedButtonClicked;
    [SerializeField] private GameEvent handshakeComplete;
    [SerializeField] private GameEvent updateVisualsAfterTradeFinalised;
    [SerializeField] private GameEvent allVisualsUpdatedAfterTradeFinalised;
    [SerializeField] private GameEvent tradingPlayersConfirmed;
    [SerializeField] private GameEvent tradingPlayerPlaced;



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
    public void call_TradeConditionsMet() => tradeConditionsMet.invoke();
    public void call_NumberedButtonClicked(int i) => numberedButtonClicked.invoke(i);
    public void call_HandshakeComplete() => handshakeComplete.invoke();
    public void call_UpdateVisualsAfterTradeFinalised() => updateVisualsAfterTradeFinalised.invoke();
    public void call_AllVisualsUpdatedAfterTradeFinalised() => allVisualsUpdatedAfterTradeFinalised.invoke();
    public void call_TradingPlayerPlaced() => tradingPlayerPlaced.invoke();
    public void call_TradingPlayersConfirmed() => tradingPlayersConfirmed.invoke();
    #endregion



    #region Subscribing
    public void sub_TradeChanged(Action a) => tradeChanged.Listeners += a;
    public void sub_TradeConditionsMet(Action a) => tradeConditionsMet.Listeners += a;
    public void sub_NumberedButtonClicked(Action<int> a) => numberedButtonClicked.Listeners += a;
    public void sub_HandshakeComplete(Action a) => handshakeComplete.Listeners += a;
    public void sub_UpdateVisualsAfterTradeFinalised(Action a) => updateVisualsAfterTradeFinalised.Listeners += a;
    public void sub_AllVisualsUpdatedAfterTradeFinalised(Action a) => allVisualsUpdatedAfterTradeFinalised.Listeners += a;
    public void sub_TradingPlayersConfirmed(Action a) => tradingPlayersConfirmed.Listeners += a;
    public void sub_TradingPlayerPlaced(Action a) => tradingPlayerPlaced.Listeners += a;
    #endregion



    #region Unsubscribing
    public void unsub_TradeChanged(Action a) => tradeChanged.Listeners -= a;
    public void unsub_TradeConditionsMet(Action a) => tradeConditionsMet.Listeners -= a;
    public void unsub_NumberedButtonClicked(Action<int> a) => numberedButtonClicked.Listeners -= a;
    public void unsub_HandshakeComplete(Action a) => handshakeComplete.Listeners -= a;
    public void unsub_AllVisualsUpdatedAfterTradeFinalised(Action a) => allVisualsUpdatedAfterTradeFinalised.Listeners -= a;
    public void unsub_TradingPlayersConfirmed(Action a) => tradingPlayersConfirmed.Listeners -= a;
    public void unsub_TradingPlayerPlaced(Action a) => tradingPlayerPlaced.Listeners -= a;
    #endregion
}
