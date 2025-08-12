using System;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/DataEventHub")]
public class DataEventHub : ScriptableObject {
    private static DataEventHub instance;
    [SerializeField] private GameEvent cardResolved;
    [SerializeField] private CardTypeEvent cardDrawn;
    [SerializeField] private GameEvent doublesCountReset;
    [SerializeField] private PlayerCreditorIntEvent playerIncurredDebt;
    [SerializeField] private PlayerIntEvent playerIncurredMultiCreditorDebt;
    [SerializeField] private EstateEvent estateAddedBuilding;
    [SerializeField] private EstateEvent estateRemovedBuilding;
    [SerializeField] private EstateGroupEvent estateGroupRemovedAllBuildings;
    [SerializeField] private PropertyEvent propertyMortgaged;
    [SerializeField] private PropertyEvent propertyUnmortgaged;
    [SerializeField] private GameEvent incrementJailTurn;
    [SerializeField] private PlayerPlayerEvent tradeCommenced;
    [SerializeField] private PlayerPropertyEvent mortgageIsResolved;
    [SerializeField] private GameEvent turnPlayerWillLoseTurn;
    [SerializeField] private CardInfoEvent cardReturned;
    [SerializeField] private PlayerBoolEvent setJailDebtBool;



    #region public
    public static DataEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<DataEventHub>(
                    "ScriptableObjects/Events/0. Hubs/data_event_hub"
                );
            }
            return instance;
        }
    }
    #endregion



    #region Data invoking
    public void call_CardResolved() => cardResolved.invoke();
    public void call_CardDrawn(CardType cardType) => cardDrawn.invoke(cardType);
    public void call_DoublesCountReset() => doublesCountReset.invoke();
    public void call_PlayerIncurredDebt(PlayerInfo playerInfo, Creditor creditor, int debtVal) {
        playerIncurredDebt.invoke(playerInfo, creditor, debtVal);
    }
    public void call_PlayerIncurredMultiCreditorDebt(PlayerInfo playerInfo, int debtValToEach) {
        playerIncurredMultiCreditorDebt.invoke(playerInfo, debtValToEach);
    }
    public void call_EstateAddedBuilding(EstateInfo estateInfo) => estateAddedBuilding.invoke(estateInfo);
    public void call_EstateRemovedBuilding(EstateInfo estateInfo) => estateRemovedBuilding.invoke(estateInfo);
    public void call_EstateGroupRemovedAllBuildings(EstateGroupInfo estateGroupInfo) {
        estateGroupRemovedAllBuildings.invoke(estateGroupInfo);
    }
    public void call_IncrementJailTurn() => incrementJailTurn.invoke();
    public void call_PropertyMortgaged(PropertyInfo propertyInfo) => propertyMortgaged.invoke(propertyInfo);
    public void call_PropertyUnmortgaged(PropertyInfo propertyInfo) => propertyUnmortgaged.invoke(propertyInfo);
    public void call_TradeCommenced(PlayerInfo p1, PlayerInfo p2) => tradeCommenced.invoke(p1, p2);
    public void call_MortgageIsResolved(PlayerInfo playerInfo, PropertyInfo propertyInfo) => mortgageIsResolved.invoke(playerInfo, propertyInfo);
    public void call_TurnPlayerWillLoseTurn() => turnPlayerWillLoseTurn.invoke();
    public void call_CardReturned(CardInfo cardInfo) => cardReturned.invoke(cardInfo);
    public void call_SetJailDebtBool(PlayerInfo playerInfo, bool b) => setJailDebtBool.invoke(playerInfo, b);
    #endregion



    #region Internal subscribing
    internal void sub_CardResolved(Action a) => cardResolved.Listeners += a;
    internal void sub_CardDrawn(Action<CardType> a) => cardDrawn.Listeners += a;
    internal void sub_DoublesCountReset(Action a) => doublesCountReset.Listeners += a;
    internal void sub_PlayerIncurredDebt(Action<PlayerInfo, Creditor, int> a) => playerIncurredDebt.Listeners += a;
    internal void sub_PlayerIncurredMultiCreditorDebt(Action<PlayerInfo, int> a) => playerIncurredMultiCreditorDebt.Listeners += a;
    internal void sub_EstateAddedBuilding(Action<EstateInfo> a) => estateAddedBuilding.Listeners += a;
    internal void sub_EstateRemovedBuilding(Action<EstateInfo> a) => estateRemovedBuilding.Listeners += a;
    internal void sub_EstateGroupRemovedAllBuildings(Action<EstateGroupInfo> a) => estateGroupRemovedAllBuildings.Listeners += a;
    internal void sub_IncrementJailTurn(Action a) => incrementJailTurn.Listeners += a;
    internal void sub_PropertyMortgaged(Action<PropertyInfo> a) => propertyMortgaged.Listeners += a;
    internal void sub_PropertyUnmortgaged(Action<PropertyInfo> a) => propertyUnmortgaged.Listeners += a;
    internal void sub_TradeCommenced(Action<PlayerInfo, PlayerInfo> a) => tradeCommenced.Listeners += a;
    internal void sub_MortgageIsResolved(Action<PlayerInfo, PropertyInfo> a) => mortgageIsResolved.Listeners += a;
    internal void sub_TurnPlayerWillLoseTurn(Action a) => turnPlayerWillLoseTurn.Listeners += a;
    internal void sub_CardReturned(Action<CardInfo> a) => cardReturned.Listeners += a;
    internal void sub_SetJailDebtBool(Action<PlayerInfo, bool> a) => setJailDebtBool.Listeners += a;
    #endregion
}
