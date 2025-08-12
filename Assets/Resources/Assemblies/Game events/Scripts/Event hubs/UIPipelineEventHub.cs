using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/UIPipelineEventHub")]
public class UIPipelineEventHub : ScriptableObject {
    private static UIPipelineEventHub instance;
    [SerializeField] private GameEvent rollButtonClicked;
    [SerializeField] private PlayerEvent moneyAdjustment;
    [SerializeField] private PlayerPlayerEvent moneyBetweenPlayers;
    [SerializeField] private PlayerArrayEvent moneyPaidToPlayerFromPlayers;
    [SerializeField] private IntIntEvent turnPlayerMovedAlongBoard;
    [SerializeField] private IntIntEvent turnPlayerMovedToSpace;
    [SerializeField] private PlayerPropertyEvent playerPropertyAdjustment;
    [SerializeField] private GameEvent nextPlayerTurn;
    [SerializeField] private PlayerCardTypeEvent playerGetsGOOJFCard;
    [SerializeField] private GameEvent leaveJail;
    [SerializeField] private CardTypeEvent useGOOJFCardButtonClicked;
    [SerializeField] private GameEvent tradeTerminated;
    [SerializeField] private GameEvent tradeUpdated;
    [SerializeField] private GameEvent tradeLockedIn;
    [SerializeField] private GameEvent moneyRaisedForDebt;
    [SerializeField] private PlayerEvent playerEliminated;



    #region public
    public static UIPipelineEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<UIPipelineEventHub>(
                    "ScriptableObjects/Events/0. Hubs/ui_pipeline_event_hub"
                );
            }
            return instance;
        }
    }
    #endregion



    #region internal
    internal GameEvent RollButtonClicked => rollButtonClicked;
    internal PlayerEvent MoneyAdjustment => moneyAdjustment;
    internal PlayerPlayerEvent MoneyBetweenPlayers => moneyBetweenPlayers;
    internal PlayerArrayEvent MoneyPaidToPlayerFromPlayers => moneyPaidToPlayerFromPlayers;
    internal IntIntEvent TurnPlayerMovedAlongBoard => turnPlayerMovedAlongBoard;
    internal IntIntEvent TurnPlayerMovedToSpace => turnPlayerMovedToSpace;
    internal PlayerPropertyEvent PlayerObtainedProperty => playerPropertyAdjustment;
    internal GameEvent NextPlayerTurn => nextPlayerTurn;
    internal PlayerCardTypeEvent PlayerGetsGOOJFCard => playerGetsGOOJFCard;
    internal GameEvent LeaveJail => leaveJail;
    internal CardTypeEvent UseGOOJFCardButtonClicked => useGOOJFCardButtonClicked;
    internal GameEvent TradeTerminated => tradeTerminated;
    internal GameEvent TradeUpdated => tradeUpdated;
    internal GameEvent TradeLockedIn => tradeLockedIn;
    internal GameEvent MoneyRaisedForDebt => moneyRaisedForDebt;
    internal PlayerEvent PlayerEliminated => playerEliminated;
    #endregion



    #region Subscribing
    public void sub_RollButtonClicked(Action a) => rollButtonClicked.Listeners += a;
    public void sub_MoneyAdjustment(Action<PlayerInfo> a) => moneyAdjustment.Listeners += a;
    public void sub_MoneyBetweenPlayers(Action<PlayerInfo, PlayerInfo> a) => moneyBetweenPlayers.Listeners += a;
    public void sub_TurnPlayerMovedAlongBoard(Action<int, int> a) => turnPlayerMovedAlongBoard.Listeners += a;
    public void sub_TurnPlayerMovedToSpace(Action<int, int> a) => turnPlayerMovedToSpace.Listeners += a;
    public void sub_PlayerPropertyAdjustment(Action<PlayerInfo, PropertyInfo> a) => playerPropertyAdjustment.Listeners += a;
    public void sub_NextPlayerTurn(Action a) => nextPlayerTurn.Listeners += a;
    public void sub_PlayerGetsGOOJFCard(Action<PlayerInfo, CardType> a) => playerGetsGOOJFCard.Listeners += a;
    public void sub_LeaveJail(Action a) => leaveJail.Listeners += a;
    public void sub_UseGOOJFCardButtonClicked(Action<CardType> a) => useGOOJFCardButtonClicked.Listeners += a;
    public void sub_TradeTerminated(Action a) => tradeTerminated.Listeners += a;
    public void sub_TradeUpdated(Action a) => tradeUpdated.Listeners += a;
    public void sub_TradeLockedIn(Action a) => tradeLockedIn.Listeners += a;
    public void sub_MoneyRaisedForDebt(Action a) => moneyRaisedForDebt.Listeners += a;
    public void sub_PlayerEliminated(Action<PlayerInfo> a) => playerEliminated.Listeners += a;
    #endregion



    #region Unsubscribing
    public void unsub_MoneyAdjustment(Action<PlayerInfo> a) => moneyAdjustment.Listeners -= a;
    public void unsub_RollButtonClicked(Action a) => rollButtonClicked.Listeners -= a;
    public void unsub_NextPlayerTurn(Action a) => nextPlayerTurn.Listeners -= a;
    public void unsub_TradeTerminated(Action a) => tradeTerminated.Listeners -= a;
    public void unsub_TradeUpdated(Action a) => tradeUpdated.Listeners -= a;
    public void unsub_TradeLockedIn(Action a) => tradeLockedIn.Listeners -= a;
    public void unsub_MoneyRaisedForDebt(Action a) => moneyRaisedForDebt.Listeners -= a;
    #endregion
}
