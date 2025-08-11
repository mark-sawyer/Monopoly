using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/ScreenOverlayEventHub")]
public class ScreenOverlayEventHub : ScriptableObject {
    private static ScreenOverlayEventHub instance;
    #region Starters
    [SerializeField] private PlayerEvent incomeTaxQuestion;
    [SerializeField] private PlayerPropertyEvent purchaseQuestion;
    [SerializeField] private DebtEvent payingRentAnimationBegins;
    [SerializeField] private GameEvent cardShown;
    [SerializeField] private GameEvent spinningPoliceman;
    [SerializeField] private GameEvent luxuryTaxAnimationBegins;
    [SerializeField] private PropertyEvent unaffordableProperty;
    [SerializeField] private GameEvent tradeOpened;
    [SerializeField] private DebtEvent resolveDebt;
    [SerializeField] private QueueTradablesEvent auctionsBegin;
    [SerializeField] private PlayerPropertyEvent resolveMortgage;
    #endregion
    [SerializeField] private GameEvent purchaseYesClicked;
    [SerializeField] private GameEvent purchaseNoClicked;
    [SerializeField] private GameEvent cardOKClicked;
    [SerializeField] private GameEvent keepMortgageClicked;
    [SerializeField] private GameEvent unmortgageClicked;
    [SerializeField] private GameEvent removeScreenOverlay;
    [SerializeField] private GameEvent removeScreenOverlayKeepCover;



    #region public
    public static ScreenOverlayEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<ScreenOverlayEventHub>(
                    "ScriptableObjects/Events/0. Hubs/screen_overlay_event_hub"
                );
            }
            return instance;
        }
    }
    #endregion



    #region Invoking
    public void call_IncomeTaxQuestion(PlayerInfo playerInfo) => incomeTaxQuestion.invoke(playerInfo);
    public void call_PurchaseQuestion(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        purchaseQuestion.invoke(playerInfo, propertyInfo);
    }
    public void call_PurchaseYesClicked() => purchaseYesClicked.invoke();
    public void call_PurchaseNoClicked() => purchaseNoClicked.invoke();
    public void call_PayingRentAnimationBegins(DebtInfo debtInfo) => payingRentAnimationBegins.invoke(debtInfo);
    public void call_CardShown() => cardShown.invoke();
    public void call_SpinningPoliceman() => spinningPoliceman.invoke();
    public void call_LuxuryTaxAnimationBegins() => luxuryTaxAnimationBegins.invoke();
    public void call_CardOKClicked() => cardOKClicked.invoke();
    public void call_UnaffordableProperty(PropertyInfo propertyInfo) => unaffordableProperty.invoke(propertyInfo);
    public void call_RemoveScreenAnimation() => removeScreenOverlay.invoke();
    public void call_RemoveScreenAnimationKeepCover() => removeScreenOverlayKeepCover.invoke();
    public void call_TradeOpened() => tradeOpened.invoke();
    public void call_ResolveDebt(DebtInfo debtInfo) => resolveDebt.invoke(debtInfo);
    public void call_AuctionsBegin(Queue<TradableInfo> tradableInfos) => auctionsBegin.invoke(tradableInfos);
    public void call_ResolveMortgage(PlayerInfo playerInfo, PropertyInfo propertyInfo) => resolveMortgage.invoke(playerInfo, propertyInfo);
    public void call_KeepMortgageClicked() => keepMortgageClicked.invoke();
    public void call_UnmortgageClicked() => unmortgageClicked.invoke();
    #endregion



    #region Subscribing
    public void sub_IncomeTaxQuestion(Action<PlayerInfo> a) => incomeTaxQuestion.Listeners += a;
    public void sub_PurchaseQuestion(Action<PlayerInfo, PropertyInfo> a) => purchaseQuestion.Listeners += a;
    public void sub_PurchaseYesClicked(Action a) => purchaseYesClicked.Listeners += a;
    public void sub_PurchaseNoClicked(Action a) => purchaseNoClicked.Listeners += a;
    public void sub_PayingRentAnimationBegins(Action<DebtInfo> a) => payingRentAnimationBegins.Listeners += a;
    public void sub_CardShown(Action a) => cardShown.Listeners += a;
    public void sub_SpinningPoliceman(Action a) => spinningPoliceman.Listeners += a;
    public void sub_LuxuryTaxAnimationBegins(Action a) => luxuryTaxAnimationBegins.Listeners += a;
    public void sub_CardOKClicked(Action a) => cardOKClicked.Listeners += a;
    public void sub_UnaffordableProperty(Action<PropertyInfo> a) => unaffordableProperty.Listeners += a;
    public void sub_RemoveScreenOverlay(Action a) => removeScreenOverlay.Listeners += a;
    public void sub_RemoveScreenOverlayKeepCover(Action a) => removeScreenOverlayKeepCover.Listeners += a;
    public void sub_TradeOpened(Action a) => tradeOpened.Listeners += a;
    public void sub_ResolveDebt(Action<DebtInfo> a) => resolveDebt.Listeners += a;
    public void sub_AuctionsBegin(Action<Queue<TradableInfo>> a) => auctionsBegin.Listeners += a;
    public void sub_ResolveMortgage(Action<PlayerInfo, PropertyInfo> a) => resolveMortgage.Listeners += a;
    public void sub_KeepMortgageClicked(Action a) => keepMortgageClicked.Listeners += a;
    public void sub_UnmortgageClicked(Action a) => unmortgageClicked.Listeners += a;
    #endregion



    #region Unubscribing
    public void unsub_RemoveScreenOverlay(Action a) => removeScreenOverlay.Listeners -= a;
    public void unsub_RemoveScreenOverlayKeepCover(Action a) => removeScreenOverlayKeepCover.Listeners -= a;
    public void unsub_TradeOpened(Action a) => tradeOpened.Listeners -= a;
    public void unsub_PurchaseYesClicked(Action a) => purchaseYesClicked.Listeners -= a;
    public void unsub_PurchaseNoClicked(Action a) => purchaseNoClicked.Listeners -= a;
    public void unsub_KeepMortgageClicked(Action a) => keepMortgageClicked.Listeners -= a;
    public void unsub_UnmortgageClicked(Action a) => unmortgageClicked.Listeners -= a;
    #endregion
}
