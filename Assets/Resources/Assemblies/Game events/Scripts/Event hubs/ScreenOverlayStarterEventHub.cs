using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/ScreenOverlayStarterEventHub")]
public class ScreenOverlayStarterEventHub : ScriptableObject {
    private static ScreenOverlayStarterEventHub instance;
    [SerializeField] private GameEvent playerNumberSelection;
    [SerializeField] private IntEvent playerNumberConfirmed;
    [SerializeField] private PlayerEvent incomeTaxQuestion;
    [SerializeField] private PlayerPropertyEvent purchaseQuestion;
    [SerializeField] private DebtEvent payingRentAnimationBegins;
    [SerializeField] private GameEvent cardShown;
    [SerializeField] private GameEvent spinningPoliceman;
    [SerializeField] private GameEvent luxuryTaxAnimationBegins;
    [SerializeField] private PropertyEvent unaffordableProperty;
    [SerializeField] private GameEvent tradeOpened;
    [SerializeField] private DebtEvent resolveDebt;
    [SerializeField] private QueuePropertiesEvent auctionsBegin;
    [SerializeField] private PlayerPropertyEvent resolveMortgage;
    [SerializeField] private PlayerEvent winnerAnnounced;
    [SerializeField] private BuildingTypeEvent auctionBuildingsBegins;
    [SerializeField] private GameEvent escapeMenu;



    #region public
    public static ScreenOverlayStarterEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<ScreenOverlayStarterEventHub>(
                    "ScriptableObjects/Events/0. Hubs/screen_overlay_starter_event_hub"
                );
            }
            return instance;
        }
    }
    #endregion



    #region Invoking
    public void call_PlayerNumberSelection() => playerNumberSelection.invoke();
    public void call_IncomeTaxQuestion(PlayerInfo playerInfo) => incomeTaxQuestion.invoke(playerInfo);
    public void call_PurchaseQuestion(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        purchaseQuestion.invoke(playerInfo, propertyInfo);
    }
    public void call_PayingRentAnimationBegins(DebtInfo debtInfo) => payingRentAnimationBegins.invoke(debtInfo);
    public void call_CardShown() => cardShown.invoke();
    public void call_SpinningPoliceman() => spinningPoliceman.invoke();
    public void call_LuxuryTaxAnimationBegins() => luxuryTaxAnimationBegins.invoke();
    public void call_UnaffordableProperty(PropertyInfo propertyInfo) => unaffordableProperty.invoke(propertyInfo);
    public void call_TradeOpened() => tradeOpened.invoke();
    public void call_ResolveDebt(DebtInfo debtInfo) => resolveDebt.invoke(debtInfo);
    public void call_AuctionsBegin(Queue<PropertyInfo> propertyInfos) => auctionsBegin.invoke(propertyInfos);
    public void call_AuctionBuildingsBegins(BuildingType buildingType) => auctionBuildingsBegins.invoke(buildingType);
    public void call_ResolveMortgage(PlayerInfo playerInfo, PropertyInfo propertyInfo) => resolveMortgage.invoke(playerInfo, propertyInfo);
    public void call_PlayerNumberConfirmed(int players) => playerNumberConfirmed.invoke(players);
    public void call_WinnerAnnounced(PlayerInfo winner) => winnerAnnounced.invoke(winner);
    public void call_EscapeMenu() => escapeMenu.invoke();
    #endregion



    #region Subscribing
    public void sub_PlayerNumberSelection(Action a) => playerNumberSelection.Listeners += a;
    public void sub_IncomeTaxQuestion(Action<PlayerInfo> a) => incomeTaxQuestion.Listeners += a;
    public void sub_PurchaseQuestion(Action<PlayerInfo, PropertyInfo> a) => purchaseQuestion.Listeners += a;
    public void sub_PayingRentAnimationBegins(Action<DebtInfo> a) => payingRentAnimationBegins.Listeners += a;
    public void sub_CardShown(Action a) => cardShown.Listeners += a;
    public void sub_SpinningPoliceman(Action a) => spinningPoliceman.Listeners += a;
    public void sub_LuxuryTaxAnimationBegins(Action a) => luxuryTaxAnimationBegins.Listeners += a;
    public void sub_UnaffordableProperty(Action<PropertyInfo> a) => unaffordableProperty.Listeners += a;
    public void sub_TradeOpened(Action a) => tradeOpened.Listeners += a;
    public void sub_ResolveDebt(Action<DebtInfo> a) => resolveDebt.Listeners += a;
    public void sub_AuctionsBegin(Action<Queue<PropertyInfo>> a) => auctionsBegin.Listeners += a;
    public void sub_AuctionBuildingsBegins(Action<BuildingType> a) => auctionBuildingsBegins.Listeners += a;
    public void sub_ResolveMortgage(Action<PlayerInfo, PropertyInfo> a) => resolveMortgage.Listeners += a;
    public void sub_PlayerNumberConfirmed(Action<int> a) => playerNumberConfirmed.Listeners += a;
    public void sub_WinnerAnnounced(Action<PlayerInfo> a) => winnerAnnounced.Listeners += a;
    public void sub_EscapeMenu(Action a) => escapeMenu.Listeners += a;
    #endregion



    #region Unubscribing
    public void unsub_TradeOpened(Action a) => tradeOpened.Listeners -= a;
    #endregion
}
