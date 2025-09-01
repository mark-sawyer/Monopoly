using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/ManagePropertiesEventHub")]
public class ManagePropertiesEventHub : ScriptableObject {
    private static ManagePropertiesEventHub instance;
    [SerializeField] private GameEvent backButtonPressed;
    [SerializeField] private GameEvent managePropertiesOpened;
    [SerializeField] private PlayerBoolEvent managePropertiesVisualRefresh;
    [SerializeField] private GameEvent managePropertiesVisualClear;
    [SerializeField] private PlayerEvent wipeToCommence;
    [SerializeField] private GameEvent panelPaused;
    [SerializeField] private GameEvent panelUnpaused;
    [SerializeField] private GameEvent remainingBuildingsPlaced;



    #region public
    public static ManagePropertiesEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<ManagePropertiesEventHub>(
                    "ScriptableObjects/Events/0. Hubs/manage_properties_event_hub"
                );
            }
            return instance;
        }
    }
    #endregion



    #region Invoking
    public void call_BackButtonPressed() => backButtonPressed.invoke();
    public void call_ManagePropertiesOpened() => managePropertiesOpened.invoke();
    public void call_ManagePropertiesVisualRefresh(PlayerInfo playerInfo, bool isRegular) => managePropertiesVisualRefresh.invoke(playerInfo, isRegular);
    public void call_ManagePropertiesVisualClear() => managePropertiesVisualClear.invoke();
    public void call_WipeToCommence(PlayerInfo playerInfo) => wipeToCommence.invoke(playerInfo);
    public void call_PanelPaused() => panelPaused.invoke();
    public void call_PanelUnpaused() => panelUnpaused.invoke();
    public void call_RemainingBuildingsPlaced() => remainingBuildingsPlaced.invoke();
    #endregion



    #region Subscribing
    public void sub_BackButtonPressed(Action a) => backButtonPressed.Listeners += a;
    public void sub_ManagePropertiesOpened(Action a) => managePropertiesOpened.Listeners += a;
    public void sub_ManagePropertiesVisualRefresh(Action<PlayerInfo, bool> a) => managePropertiesVisualRefresh.Listeners += a;
    public void sub_ManagePropertiesVisualClear(Action a) => managePropertiesVisualClear.Listeners += a;
    public void sub_WipeToCommence(Action<PlayerInfo> a) => wipeToCommence.Listeners += a;
    public void sub_PanelPaused(Action a) => panelPaused.Listeners += a;
    public void sub_PanelUnpaused(Action a) => panelUnpaused.Listeners += a;
    public void sub_RemainingBuildingsPlaced(Action a) => remainingBuildingsPlaced.Listeners += a;
    #endregion



    #region Unsubscribing
    public void unsub_BackButtonPressed(Action a) => backButtonPressed.Listeners -= a;
    public void unsub_ManagePropertiesOpened(Action a) => managePropertiesOpened.Listeners -= a;
    public void unsub_ManagePropertiesVisualRefresh(Action<PlayerInfo, bool> a) => managePropertiesVisualRefresh.Listeners -= a;
    public void unsub_WipeToCommence(Action<PlayerInfo> a) => wipeToCommence.Listeners -= a;
    public void unsub_PanelPaused(Action a) => panelPaused.Listeners -= a;
    public void unsub_PanelUnpaused(Action a) => panelUnpaused.Listeners -= a;
    public void unsub_RemainingBuildingsPlaced(Action a) => remainingBuildingsPlaced.Listeners -= a;
    #endregion
}
