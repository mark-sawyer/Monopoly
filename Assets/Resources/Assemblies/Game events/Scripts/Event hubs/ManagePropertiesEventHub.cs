using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/ManagePropertiesEventHub")]
public class ManagePropertiesEventHub : ScriptableObject {
    private static ManagePropertiesEventHub instance;
    [SerializeField] private GameEvent backButtonPressed;
    [SerializeField] private GameEvent iconsUpdatedAfterManagePropertiesClosed;
    [SerializeField] private GameEvent managePropertiesOpened;
    [SerializeField] private PlayerEvent managePropertiesVisualRefresh;
    [SerializeField] private PlayerEvent tokenSelectedInManageProperties;
    [SerializeField] private GameEvent updateIconsAfterManagePropertiesClosed;



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
    public void call_IconsUpdatedAfterManagePropertiesClosed() => iconsUpdatedAfterManagePropertiesClosed.invoke();
    public void call_ManagePropertiesOpened() => managePropertiesOpened.invoke();
    public void call_ManagePropertiesVisualRefresh(PlayerInfo playerInfo) => managePropertiesVisualRefresh.invoke(playerInfo);
    public void call_TokenSelectedInManageProperties(PlayerInfo playerInfo) => tokenSelectedInManageProperties.invoke(playerInfo);
    public void call_UpdateIconsAfterManagePropertiesClosed() => updateIconsAfterManagePropertiesClosed.invoke();
    #endregion



    #region Subscribing
    public void sub_BackButtonPressed(Action a) => backButtonPressed.Listeners += a;
    public void sub_IconsUpdatedAfterManagePropertiesClosed(Action a) => iconsUpdatedAfterManagePropertiesClosed.Listeners += a;
    public void sub_ManagePropertiesOpened(Action a) => managePropertiesOpened.Listeners += a;
    public void sub_ManagePropertiesVisualRefresh(Action<PlayerInfo> a) => managePropertiesVisualRefresh.Listeners += a;
    public void sub_TokenSelectedInManageProperties(Action<PlayerInfo> a) => tokenSelectedInManageProperties.Listeners += a;
    public void sub_UpdateIconsAfterManagePropertiesClosed(Action a) => updateIconsAfterManagePropertiesClosed.Listeners += a;
    #endregion



    #region Unsubscribing
    public void unsub_BackButtonPressed(Action a) => backButtonPressed.Listeners -= a;
    public void unsub_IconsUpdatedAfterManagePropertiesClosed(Action a) => iconsUpdatedAfterManagePropertiesClosed.Listeners -= a;
    public void unsub_ManagePropertiesOpened(Action a) => managePropertiesOpened.Listeners -= a;
    public void unsub_ManagePropertiesVisualRefresh(Action<PlayerInfo> a) => managePropertiesVisualRefresh.Listeners -= a;
    public void unsub_TokenSelectedInManageProperties(Action<PlayerInfo> a) => tokenSelectedInManageProperties.Listeners -= a;
    public void unsub_UpdateIconsAfterManagePropertiesClosed(Action a) => updateIconsAfterManagePropertiesClosed.Listeners -= a;
    #endregion
}
