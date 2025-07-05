using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/UIEventHub")]
public class UIEventHub : ScriptableObject {
    private static UIEventHub instance;
    #region UI only
    [SerializeField] private GameEvent doublesTickBoxUpdate;
    [SerializeField] private GameEvent payFiftyButtonClicked;
    [SerializeField] private GameEvent tokenSettled;
    #endregion
    #region In pipeline
    [SerializeField] private GameEvent rollButtonClicked;
    [SerializeField] private PlayerEvent moneyAdjustment;
    [SerializeField] private IntIntEvent turnPlayerMovedAlongBoard;
    [SerializeField] private IntIntEvent turnPlayerMovedToSpace;
    [SerializeField] private PlayerPropertyEvent playerPropertyAdjustment;
    [SerializeField] private GameEvent nextPlayerTurn;
    [SerializeField] private PlayerCardTypeEvent playerGetsGOOJFCard;
    [SerializeField] private BoolEvent turnBegin;
    [SerializeField] private GameEvent leaveJail;
    [SerializeField] private CardTypeEvent useGOOJFCardButtonClicked;
    [SerializeField] private EstateEvent estateAddedBuilding;
    #endregion



    #region public
    public static UIEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<UIEventHub>(
                    "ScriptableObjects/Events/0. Hubs/ui_event_hub"
                );
            }
            return instance;
        }
    }
    #endregion



    #region internal
    internal GameEvent RollButtonClicked => rollButtonClicked;
    internal PlayerEvent MoneyAdjustment => moneyAdjustment;
    internal IntIntEvent TurnPlayerMovedAlongBoard => turnPlayerMovedAlongBoard;
    internal IntIntEvent TurnPlayerMovedToSpace => turnPlayerMovedToSpace;
    internal PlayerPropertyEvent PlayerObtainedProperty => playerPropertyAdjustment;
    internal GameEvent NextPlayerTurn => nextPlayerTurn;
    internal PlayerCardTypeEvent PlayerGetsGOOJFCard => playerGetsGOOJFCard;
    internal BoolEvent TurnBegin => turnBegin;
    internal GameEvent LeaveJail => leaveJail;
    internal CardTypeEvent UseGOOJFCardButtonClicked => useGOOJFCardButtonClicked;
    internal EstateEvent EstateAddedBuilding => estateAddedBuilding;
    #endregion



    #region UI invoking
    public void call_DoublesTickBoxUpdate() {
        doublesTickBoxUpdate.invoke();
    }
    public void call_PayFiftyButtonClicked() {
        payFiftyButtonClicked.invoke();
    }
    public void call_TokenSettled() {
        tokenSettled.invoke();
    }
    #endregion



    #region UI public subscribing
    public void sub_DoublesTickBoxUpdate(Action a) {
        doublesTickBoxUpdate.Listeners += a;
    }
    public void sub_PayFiftyButtonClicked(Action a) {
        payFiftyButtonClicked.Listeners += a;
    }
    public void sub_TokenSettled(Action a) {
        tokenSettled.Listeners += a;
    }
    public void sub_RollButtonClicked(Action a) {
        rollButtonClicked.Listeners += a;
    }
    public void sub_MoneyAdjustment(Action<PlayerInfo> a) {
        moneyAdjustment.Listeners += a;
    }
    public void sub_TurnPlayerMovedAlongBoard(Action<int, int> a) {
        turnPlayerMovedAlongBoard.Listeners += a;
    }
    public void sub_TurnPlayerMovedToSpace(Action<int, int> a) {
        turnPlayerMovedToSpace.Listeners += a;
    }
    public void sub_PlayerPropertyAdjustment(Action<PlayerInfo, PropertyInfo> a) {
        playerPropertyAdjustment.Listeners += a;
    }
    public void sub_NextPlayerTurn(Action a) {
        nextPlayerTurn.Listeners += a;
    }
    public void sub_PlayerGetsGOOJFCard(Action<PlayerInfo, CardType> a) {
        playerGetsGOOJFCard.Listeners += a;
    }
    public void sub_TurnBegin(Action<bool> a) {
        turnBegin.Listeners += a;
    }
    public void sub_LeaveJail(Action a) {
        leaveJail.Listeners += a;
    }
    public void sub_UseGOOJFCardButtonClicked(Action<CardType> a) {
        useGOOJFCardButtonClicked.Listeners += a;
    }
    public void sub_EstateAddedBuilding(Action<EstateInfo> a) {
        estateAddedBuilding.Listeners += a;
    }
    #endregion



    #region UI public unsubscribing
    public void unsub_DoublesTickBoxUpdate(Action a) {
        doublesTickBoxUpdate.Listeners -= a;
    }
    public void unsub_PayFiftyButtonClicked(Action a) {
        payFiftyButtonClicked.Listeners -= a;
    }
    public void unsub_TokenSettled(Action a) {
        tokenSettled.Listeners -= a;
    }
    public void unsub_RollButtonClicked(Action a) {
        rollButtonClicked.Listeners -= a;
    }
    public void unsub_NextPlayerTurn(Action a) {
        nextPlayerTurn.Listeners -= a;
    }
    public void unsub_TurnBegin(Action<bool> a) {
        turnBegin.Listeners -= a;
    }
    public void unsub_UseGOOJFCardButtonClicked(Action<CardType> a) {
        useGOOJFCardButtonClicked.Listeners -= a;
    }
    #endregion
}
