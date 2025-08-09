using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/ResolveDebtEventHub")]
public class ResolveDebtEventHub : ScriptableObject {
    private static ResolveDebtEventHub instance;
    [SerializeField] private GameEvent resolveDebtVisualRefresh;



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
    #endregion



    #region Subscribing
    public void sub_ResolveDebtVisualRefresh(Action a) => resolveDebtVisualRefresh.Listeners += a;
    #endregion



    #region Unsubscribing
    public void unsub_ResolveDebtVisualRefresh(Action a) => resolveDebtVisualRefresh.Listeners -= a;
    #endregion
}
