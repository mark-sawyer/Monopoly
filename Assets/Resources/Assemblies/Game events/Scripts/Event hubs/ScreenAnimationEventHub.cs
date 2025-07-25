using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/ScreenAnimationEventHub")]
public class ScreenAnimationEventHub : ScriptableObject {
    private static ScreenAnimationEventHub instance;
    [SerializeField] private PlayerEvent incomeTaxQuestion;
    [SerializeField] private PlayerPropertyEvent purchaseQuestion;
    [SerializeField] private DebtEvent payingRentAnimationBegins;
    [SerializeField] private GameEvent cardShown;
    [SerializeField] private GameEvent spinningPoliceman;
    [SerializeField] private GameEvent luxuryTaxAnimationBegins;
    [SerializeField] private GameEvent cardOKClicked;
    [SerializeField] private PropertyEvent unaffordableProperty;
    [SerializeField] private GameEvent removeScreenAnimation;
    [SerializeField] private GameEvent removeScreenAnimationKeepCover;



    #region public
    public static ScreenAnimationEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<ScreenAnimationEventHub>(
                    "ScriptableObjects/Events/0. Hubs/screen_animation_event_hub"
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
    public void call_PayingRentAnimationBegins(DebtInfo debtInfo) => payingRentAnimationBegins.invoke(debtInfo);
    public void call_CardShown() => cardShown.invoke();
    public void call_SpinningPoliceman() => spinningPoliceman.invoke();
    public void call_LuxuryTaxAnimationBegins() => luxuryTaxAnimationBegins.invoke();
    public void call_CardOKClicked() => cardOKClicked.invoke();
    public void call_UnaffordableProperty(PropertyInfo propertyInfo) => unaffordableProperty.invoke(propertyInfo);
    public void call_RemoveScreenAnimation() => removeScreenAnimation.invoke();
    public void call_RemoveScreenAnimationKeepCover() => removeScreenAnimationKeepCover.invoke();
    #endregion



    #region Subscribing
    public void sub_IncomeTaxQuestion(Action<PlayerInfo> a) => incomeTaxQuestion.Listeners += a;
    public void sub_PurchaseQuestion(Action<PlayerInfo, PropertyInfo> a) => purchaseQuestion.Listeners += a;
    public void sub_PayingRentAnimationBegins(Action<DebtInfo> a) => payingRentAnimationBegins.Listeners += a;
    public void sub_CardShown(Action a) => cardShown.Listeners += a;
    public void sub_SpinningPoliceman(Action a) => spinningPoliceman.Listeners += a;
    public void sub_LuxuryTaxAnimationBegins(Action a) => luxuryTaxAnimationBegins.Listeners += a;
    public void sub_CardOKClicked(Action a) => cardOKClicked.Listeners += a;
    public void sub_UnaffordableProperty(Action<PropertyInfo> a) => unaffordableProperty.Listeners += a;
    public void sub_RemoveScreenAnimation(Action a) => removeScreenAnimation.Listeners += a;
    public void sub_RemoveScreenAnimationKeepCover(Action a) => removeScreenAnimationKeepCover.Listeners += a;
    #endregion



    #region Unubscribing
    public void unsub_RemoveScreenAnimation(Action a) => removeScreenAnimation.Listeners -= a;
    public void unsub_RemoveScreenAnimationKeepCover(Action a) => removeScreenAnimationKeepCover.Listeners -= a;
    #endregion
}
