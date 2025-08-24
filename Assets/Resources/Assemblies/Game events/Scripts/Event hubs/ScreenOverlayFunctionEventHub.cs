using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/ScreenOverlayFunctionEventHub")]
public class ScreenOverlayFunctionEventHub : ScriptableObject {
    private static ScreenOverlayFunctionEventHub instance;
    [SerializeField] private GameEvent removeScreenOverlay;
    [SerializeField] private GameEvent removeScreenOverlayKeepCover;
    [SerializeField] private GameEvent purchaseYesClicked;
    [SerializeField] private GameEvent purchaseNoClicked;
    [SerializeField] private GameEvent keepMortgageClicked;
    [SerializeField] private GameEvent unmortgageClicked;
    [SerializeField] private GameEvent selectedTokensChanged;
    [SerializeField] private GameEvent continueClicked;
    [SerializeField] private GameEvent incomeTaxAnswered;



    #region public
    public static ScreenOverlayFunctionEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<ScreenOverlayFunctionEventHub>(
                    "ScriptableObjects/Events/0. Hubs/screen_overlay_function_event_hub"
                );
            }
            return instance;
        }
    }
    #endregion



    #region Invoking
    public void call_RemoveScreenOverlay() => removeScreenOverlay.invoke();
    public void call_RemoveScreenOverlayKeepCover() => removeScreenOverlayKeepCover.invoke();
    public void call_PurchaseYesClicked() => purchaseYesClicked.invoke();
    public void call_PurchaseNoClicked() => purchaseNoClicked.invoke();
    public void call_KeepMortgageClicked() => keepMortgageClicked.invoke();
    public void call_UnmortgageClicked() => unmortgageClicked.invoke();
    public void call_SelectedTokensChanged() => selectedTokensChanged.invoke();
    public void call_ContinueClicked() => continueClicked.invoke();
    public void call_IncomeTaxAnswered() => incomeTaxAnswered.invoke();
    #endregion



    #region Subscribing
    public void sub_RemoveScreenOverlay(Action a) => removeScreenOverlay.Listeners += a;
    public void sub_RemoveScreenOverlayKeepCover(Action a) => removeScreenOverlayKeepCover.Listeners += a;
    public void sub_PurchaseYesClicked(Action a) => purchaseYesClicked.Listeners += a;
    public void sub_PurchaseNoClicked(Action a) => purchaseNoClicked.Listeners += a;
    public void sub_KeepMortgageClicked(Action a) => keepMortgageClicked.Listeners += a;
    public void sub_UnmortgageClicked(Action a) => unmortgageClicked.Listeners += a;
    public void sub_SelectedTokensChanged(Action a) => selectedTokensChanged.Listeners += a;
    public void sub_ContinueClicked(Action a) => continueClicked.Listeners += a;
    public void sub_IncomeTaxAnswered(Action a) => incomeTaxAnswered.Listeners += a;
    #endregion



    #region Unsubscribing
    public void unsub_RemoveScreenOverlay(Action a) => removeScreenOverlay.Listeners -= a;
    public void unsub_RemoveScreenOverlayKeepCover(Action a) => removeScreenOverlayKeepCover.Listeners -= a;
    public void unsub_PurchaseYesClicked(Action a) => purchaseYesClicked.Listeners -= a;
    public void unsub_PurchaseNoClicked(Action a) => purchaseNoClicked.Listeners -= a;
    public void unsub_KeepMortgageClicked(Action a) => keepMortgageClicked.Listeners -= a;
    public void unsub_UnmortgageClicked(Action a) => unmortgageClicked.Listeners -= a;
    public void unsub_SelectedTokensChanged(Action a) => selectedTokensChanged.Listeners -= a;
    public void unsub_ContinueClicked(Action a) => continueClicked.Listeners -= a;
    #endregion
}
