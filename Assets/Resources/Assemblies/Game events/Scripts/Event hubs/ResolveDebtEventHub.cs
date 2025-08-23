using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/ResolveDebtEventHub")]
public class ResolveDebtEventHub : ScriptableObject {
    private static ResolveDebtEventHub instance;
    [SerializeField] private GameEvent resolveDebtVisualRefresh;
    [SerializeField] private GameEvent tradeButtonClicked;
    [SerializeField] private GameEvent declareBankruptcyButtonClicked;
    [SerializeField] private GameEvent resolveDebtPanelLowered;
    [SerializeField] private GameEvent debtResolved;
    [SerializeField] private GameEvent panelInTransit;



    #region public
    public static ResolveDebtEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<ResolveDebtEventHub>(
                    "ScriptableObjects/Events/0. Hubs/resolve_debt_event_hub"
                );
            }
            return instance;
        }
    }
    #endregion



    #region Invoking
    public void call_ResolveDebtVisualRefresh() => resolveDebtVisualRefresh.invoke();
    public void call_TradeButtonClicked() => tradeButtonClicked.invoke();
    public void call_DeclareBankruptcyButtonClicked() => declareBankruptcyButtonClicked.invoke();
    public void call_ResolveDebtPanelLowered() => resolveDebtPanelLowered.invoke();
    public void call_DebtResolved() => debtResolved.invoke();
    public void call_PanelInTransit() => panelInTransit.invoke();
    #endregion



    #region Subscribing
    public void sub_ResolveDebtVisualRefresh(Action a) => resolveDebtVisualRefresh.Listeners += a;
    public void sub_TradeButtonClicked(Action a) => tradeButtonClicked.Listeners += a;
    public void sub_DeclareBankruptcyButtonClicked(Action a) => declareBankruptcyButtonClicked.Listeners += a;
    public void sub_ResolveDebtPanelLowered(Action a) => resolveDebtPanelLowered.Listeners += a;
    public void sub_DebtResolved(Action a) => debtResolved.Listeners += a;
    public void sub_PanelInTransit(Action a) => panelInTransit.Listeners += a;
    #endregion



    #region Unsubscribing
    public void unsub_ResolveDebtVisualRefresh(Action a) => resolveDebtVisualRefresh.Listeners -= a;
    public void unsub_TradeButtonClicked(Action a) => tradeButtonClicked.Listeners -= a;
    public void unsub_DeclareBankruptcyButtonClicked(Action a) => declareBankruptcyButtonClicked.Listeners -= a;
    public void unsub_ResolveDebtPanelLowered(Action a) => resolveDebtPanelLowered.Listeners -= a;
    public void unsub_DebtResolved(Action a) => debtResolved.Listeners -= a;
    public void unsub_PanelInTransit(Action a) => panelInTransit.Listeners -= a;
    #endregion
}
