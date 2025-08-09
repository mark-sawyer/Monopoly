using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;


[CreateAssetMenu(menuName = "GameEvent/Hubs/DataUIPipelineEventHub")]
public class DataUIPipelineEventHub : ScriptableObject {
    private static DataUIPipelineEventHub instance;
    private UIPipelineEventHub uiPipelineEvents;
    [SerializeField] private PlayerCardEvent playerGetsGOOJFCard;
    [SerializeField] private GameEvent leaveJail;
    [SerializeField] private PlayerIntEvent moneyAdjustment;
    [SerializeField] private PlayerPlayerIntEvent moneyBetweenPlayers;
    [SerializeField] private GameEvent nextPlayerTurn;
    [SerializeField] private PlayerPropertyEvent playerObtainedProperty;
    [SerializeField] private GameEvent rollButtonClicked;
    [SerializeField] private GameEvent tradeTerminated;
    [SerializeField] private TradablesPlayerIntEvent tradeUpdated;
    [SerializeField] private GameEvent tradeLockedIn;
    [SerializeField] private IntEvent turnPlayerMovedAlongBoard;
    [SerializeField] private SpaceEvent turnPlayerMovedToSpace;
    [SerializeField] private GameEvent turnPlayerSentToJail;
    [SerializeField] private CardTypeEvent useGOOJFCardButtonClicked;
    [SerializeField] private PlayerIntEvent debtReduced;
    [SerializeField] private PlayerIntEvent moneyRaisedForDebt;
    [SerializeField] private PlayerEvent playerEliminated;



    #region Setup
    private void OnEnable() {
        uiPipelineEvents = UIPipelineEventHub.Instance;
    }
    public static DataUIPipelineEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<DataUIPipelineEventHub>(
                    "ScriptableObjects/Events/0. Hubs/data_ui_pipeline_event_hub"
                );
            }
            return instance;
        }
    }
    #endregion



    #region Invoking
    public void call_RollButtonClicked() {
        rollButtonClicked.invoke();
        uiPipelineEvents.RollButtonClicked.invoke();
    }
    public void call_MoneyAdjustment(PlayerInfo playerInfo, int adjustment) {
        moneyAdjustment.invoke(playerInfo, adjustment);
        uiPipelineEvents.MoneyAdjustment.invoke(playerInfo);
    }
    public void call_MoneyBetweenPlayers(PlayerInfo losingPlayer, PlayerInfo gainingPlayer, int amount) {
        moneyBetweenPlayers.invoke(losingPlayer, gainingPlayer, amount);
        uiPipelineEvents.MoneyBetweenPlayers.invoke(losingPlayer, gainingPlayer);
    }
    public void call_TurnPlayerMovedAlongBoard(int startingIndex, int spacesMoved) {
        turnPlayerMovedAlongBoard.invoke(spacesMoved);
        uiPipelineEvents.TurnPlayerMovedAlongBoard.invoke(startingIndex, spacesMoved);
    }
    public void call_TurnPlayerMovedToSpace(SpaceInfo newSpace, int startingIndex) {
        turnPlayerMovedToSpace.invoke(newSpace);
        uiPipelineEvents.TurnPlayerMovedToSpace.invoke(startingIndex, newSpace.Index);
    }
    public void call_TurnPlayerSentToJail(int startingIndex) {
        turnPlayerSentToJail.invoke();
        uiPipelineEvents.TurnPlayerMovedToSpace.invoke(startingIndex, GameConstants.JAIL_SPACE_INDEX);
    }
    public void call_PlayerObtainedProperty(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        playerObtainedProperty.invoke(playerInfo, propertyInfo);
        uiPipelineEvents.PlayerObtainedProperty.invoke(playerInfo, propertyInfo);
    }
    public void call_NextPlayerTurn() {
        nextPlayerTurn.invoke();
        uiPipelineEvents.NextPlayerTurn.invoke();
    }
    public void call_PlayerGetsGOOJFCard(PlayerInfo playerInfo, CardInfo cardInfo) {
        playerGetsGOOJFCard.invoke(playerInfo, cardInfo);
        uiPipelineEvents.PlayerGetsGOOJFCard.invoke(playerInfo, cardInfo.CardType);
    }
    public void call_LeaveJail() {
        leaveJail.invoke();
        uiPipelineEvents.LeaveJail.invoke();
    }
    public void call_UseGOOJFCardButtonClicked(CardType cardType) {
        useGOOJFCardButtonClicked.invoke(cardType);
        uiPipelineEvents.UseGOOJFCardButtonClicked.invoke(cardType);
    }
    public void call_TradeTerminated() {
        tradeTerminated.invoke();
        uiPipelineEvents.TradeTerminated.invoke();
    }
    public void call_TradeUpdated(List<TradableInfo> t1, List<TradableInfo> t2, PlayerInfo moneyGiver, int money) {
        tradeUpdated.invoke(t1, t2, moneyGiver, money);
        uiPipelineEvents.TradeUpdated.invoke();
    }
    public void call_TradeLockedIn() {
        tradeLockedIn.invoke();
        uiPipelineEvents.TradeLockedIn.invoke();
    }
    public void call_DebtReduced(PlayerInfo debtor, int paid) {
        Creditor creditor = debtor.DebtInfo.Creditor;
        debtReduced.invoke(debtor, paid);
        if (creditor is PlayerInfo playerCreditor) {
            uiPipelineEvents.MoneyBetweenPlayers.invoke(debtor, playerCreditor);
        }
        else {
            uiPipelineEvents.MoneyAdjustment.invoke(debtor);
        }
    }
    public void call_MoneyRaisedForDebt(PlayerInfo debtor, int moneyRaised) {
        moneyRaisedForDebt.invoke(debtor, moneyRaised);
        uiPipelineEvents.MoneyRaisedForDebt.invoke();
    }
    public void call_PlayerEliminated(PlayerInfo eliminatedPlayer) {
        playerEliminated.invoke(eliminatedPlayer);
        uiPipelineEvents.PlayerEliminated.invoke(eliminatedPlayer);
    }
    #endregion



    #region Internal subscribing
    internal void sub_RollButtonClicked(Action a) => rollButtonClicked.Listeners += a;
    internal void sub_MoneyAdjustment(Action<PlayerInfo, int> a) => moneyAdjustment.Listeners += a;
    internal void sub_MoneyBetweenPlayers(Action<PlayerInfo, PlayerInfo, int> a) => moneyBetweenPlayers.Listeners += a;
    internal void sub_TurnPlayerMovedAlongBoard(Action<int> a) => turnPlayerMovedAlongBoard.Listeners += a;
    internal void sub_TurnPlayerMovedToSpace(Action<SpaceInfo> a) => turnPlayerMovedToSpace.Listeners += a;
    internal void sub_TurnPlayerSentToJail(Action a) => turnPlayerSentToJail.Listeners += a;
    internal void sub_PlayerObtainedProperty(Action<PlayerInfo, PropertyInfo> a) => playerObtainedProperty.Listeners += a;
    internal void sub_NextPlayerTurn(Action a) => nextPlayerTurn.Listeners += a;
    internal void sub_PlayerGetsGOOJFCard(Action<PlayerInfo, CardInfo> a) => playerGetsGOOJFCard.Listeners += a;
    internal void sub_LeaveJail(Action a) => leaveJail.Listeners += a;
    internal void sub_UseGOOJFCardButtonClicked(Action<CardType> a) => useGOOJFCardButtonClicked.Listeners += a;
    internal void sub_TradeUpdated(Action<List<TradableInfo>, List<TradableInfo>, PlayerInfo, int> a) {
        tradeUpdated.Listeners += a;
    }
    internal void sub_TradeTerminated(Action a) => tradeTerminated.Listeners += a;
    internal void sub_TradeLockedIn(Action a) => tradeLockedIn.Listeners += a;
    internal void sub_DebtReduced(Action<PlayerInfo, int> a) => debtReduced.Listeners += a;
    internal void sub_MoneyRaisedForDebt(Action<PlayerInfo, int> a) => moneyRaisedForDebt.Listeners += a;
    internal void sub_PlayerEliminated(Action<PlayerInfo> a) => playerEliminated.Listeners += a;
    #endregion
}
