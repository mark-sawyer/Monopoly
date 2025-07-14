using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/DataEventHub")]
public class DataEventHub : ScriptableObject {
    private static DataEventHub instance;
    private UIEventHub uiEvents;
    #region Data only
    [SerializeField] private GameEvent cardResolved;
    [SerializeField] private CardTypeEvent cardDrawn;
    [SerializeField] private PlayerEvent debtResolved;
    [SerializeField] private GameEvent doublesCountReset;
    [SerializeField] private PlayerCreditorIntEvent playerIncurredDebt;
    [SerializeField] private EstateEvent estateAddedBuilding;
    [SerializeField] private EstateEvent estateRemovedBuilding;
    [SerializeField] private PropertyEvent propertyMortgaged;
    [SerializeField] private PropertyEvent propertyUnmortgaged;
    #endregion
    #region In pipeline
    [SerializeField] private GameEvent rollButtonClicked;
    [SerializeField] private PlayerIntEvent moneyAdjustment;
    [SerializeField] private PlayerPlayerIntEvent moneyBetweenPlayers;
    [SerializeField] private IntEvent turnPlayerMovedAlongBoard;
    [SerializeField] private SpaceEvent turnPlayerMovedToSpace;
    [SerializeField] private GameEvent turnPlayerSentToJail;
    [SerializeField] private PlayerPropertyEvent playerObtainedProperty;
    [SerializeField] private GameEvent nextPlayerTurn;
    [SerializeField] private PlayerCardEvent playerGetsGOOJFCard;
    [SerializeField] private BoolEvent turnBegin;
    [SerializeField] private GameEvent leaveJail;
    [SerializeField] private CardTypeEvent useGOOJFCardButtonClicked;
    #endregion



    #region Setup
    private void OnEnable() {
        uiEvents = UIEventHub.Instance;
    }
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
    public void call_TurnPlayerMovedAlongBoard(int spacesMoved) => turnPlayerMovedAlongBoard.invoke(spacesMoved);
    public void call_CardResolved() => cardResolved.invoke();
    public void call_CardDrawn(CardType cardType) => cardDrawn.invoke(cardType);
    public void call_DebtResolved(PlayerInfo playerInfo) => debtResolved.invoke(playerInfo);
    public void call_DoublesCountReset() => doublesCountReset.invoke();
    public void call_PlayerIncurredDebt(PlayerInfo playerInfo, Creditor creditor, int debtVal) {
        playerIncurredDebt.invoke(playerInfo, creditor, debtVal);
    }
    public void call_EstateAddedBuilding(EstateInfo estateInfo) => estateAddedBuilding.invoke(estateInfo);
    public void call_EstateRemovedBuilding(EstateInfo estateInfo) => estateRemovedBuilding.invoke(estateInfo);
    public void call_PropertyMortgaged(PropertyInfo propertyInfo) => propertyMortgaged.invoke(propertyInfo);
    public void call_PropertyUnmortgaged(PropertyInfo propertyInfo) => propertyUnmortgaged.invoke(propertyInfo);
    #endregion



    #region Pipeline invoking
    public void call_RollButtonClicked() {
        rollButtonClicked.invoke();
        uiEvents.RollButtonClicked.invoke();
    }
    public void call_MoneyAdjustment(PlayerInfo playerInfo, int adjustment) {
        moneyAdjustment.invoke(playerInfo, adjustment);
        uiEvents.MoneyAdjustment.invoke(playerInfo);
    }
    public void call_MoneyBetweenPlayers(PlayerInfo losingPlayer, PlayerInfo gainingPlayer, int amount) {
        moneyBetweenPlayers.invoke(losingPlayer, gainingPlayer, amount);
        uiEvents.MoneyBetweenPlayers.invoke(losingPlayer, gainingPlayer);
    }
    public void call_TurnPlayerMovedAlongBoard(int startingIndex, int spacesMoved) {
        turnPlayerMovedAlongBoard.invoke(spacesMoved);
        uiEvents.TurnPlayerMovedAlongBoard.invoke(startingIndex, spacesMoved);
    }
    public void call_TurnPlayerMovedToSpace(SpaceInfo newSpace, int startingIndex) {
        turnPlayerMovedToSpace.invoke(newSpace);
        uiEvents.TurnPlayerMovedToSpace.invoke(startingIndex, newSpace.Index);
    }
    public void call_TurnPlayerSentToJail(int startingIndex) {
        turnPlayerSentToJail.invoke();
        uiEvents.TurnPlayerMovedToSpace.invoke(startingIndex, GameConstants.JAIL_SPACE_INDEX);
    }
    public void call_PlayerObtainedProperty(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        playerObtainedProperty.invoke(playerInfo, propertyInfo);
        uiEvents.PlayerObtainedProperty.invoke(playerInfo, propertyInfo);
    }
    public void call_NextPlayerTurn() {
        nextPlayerTurn.invoke();
        uiEvents.NextPlayerTurn.invoke();
    }
    public void call_PlayerGetsGOOJFCard(PlayerInfo playerInfo, CardInfo cardInfo) {
        playerGetsGOOJFCard.invoke(playerInfo, cardInfo);
        uiEvents.PlayerGetsGOOJFCard.invoke(playerInfo, cardInfo.CardType);
    }
    public void call_TurnBegin(bool turnPlayerInJail) {
        turnBegin.invoke(turnPlayerInJail);
        uiEvents.TurnBegin.invoke(turnPlayerInJail);
    }
    public void call_LeaveJail() {
        leaveJail.invoke();
        uiEvents.LeaveJail.invoke();
    }
    public void call_UseGOOJFCardButtonClicked(CardType cardType) {
        useGOOJFCardButtonClicked.invoke(cardType);
        uiEvents.UseGOOJFCardButtonClicked.invoke(cardType);
    }
    #endregion



    #region Internal subscribing
    internal void sub_RollButtonClicked(Action a) => rollButtonClicked.Listeners += a;
    internal void sub_TurnPlayerMovedAlongBoard(Action<int> a) => turnPlayerMovedAlongBoard.Listeners += a;
    internal void sub_TurnPlayerSentToJail(Action a) => turnPlayerSentToJail.Listeners += a;
    internal void sub_TurnPlayerMovedToSpace(Action<SpaceInfo> a) => turnPlayerMovedToSpace.Listeners += a;
    internal void sub_CardResolved(Action a) => cardResolved.Listeners += a;
    internal void sub_CardDrawn(Action<CardType> a) => cardDrawn.Listeners += a;
    internal void sub_DebtResolved(Action<PlayerInfo> a) => debtResolved.Listeners += a;
    internal void sub_DoublesCountReset(Action a) => doublesCountReset.Listeners += a;
    internal void sub_PlayerIncurredDebt(Action<PlayerInfo, Creditor, int> a) => playerIncurredDebt.Listeners += a;
    internal void sub_EstateAddedBuilding(Action<EstateInfo> a) => estateAddedBuilding.Listeners += a;
    internal void sub_EstateRemovedBuilding(Action<EstateInfo> a) => estateRemovedBuilding.Listeners += a;
    internal void sub_PropertyMortgaged(Action<PropertyInfo> a) => propertyMortgaged.Listeners += a;
    internal void sub_PropertyUnmortgaged(Action<PropertyInfo> a) => propertyUnmortgaged.Listeners += a;


    internal void sub_MoneyAdjustment(Action<PlayerInfo, int> a) => moneyAdjustment.Listeners += a;
    internal void sub_MoneyBetweenPlayers(Action<PlayerInfo, PlayerInfo, int> a) => moneyBetweenPlayers.Listeners += a;
    internal void sub_PlayerObtainedProperty(Action<PlayerInfo, PropertyInfo> a) => playerObtainedProperty.Listeners += a;
    internal void sub_NextPlayerTurn(Action a) => nextPlayerTurn.Listeners += a;
    internal void sub_PlayerGetsGOOJFCard(Action<PlayerInfo, CardInfo> a) => playerGetsGOOJFCard.Listeners += a;
    internal void sub_TurnBegin(Action<bool> a) => turnBegin.Listeners += a;
    internal void sub_LeaveJail(Action a) => leaveJail.Listeners += a;
    internal void sub_UseGOOJFCardButtonClicked(Action<CardType> a) => useGOOJFCardButtonClicked.Listeners += a;
    #endregion
}
