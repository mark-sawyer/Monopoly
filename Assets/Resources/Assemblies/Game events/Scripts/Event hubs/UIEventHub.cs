using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/UIEventHub")]
public class UIEventHub : ScriptableObject {
    private static UIEventHub instance;
    #region UI only
    [SerializeField] private GameEvent doublesTickBoxUpdate;
    [SerializeField] private GameEvent payFiftyButtonClicked;
    [SerializeField] private GameEvent tokenSettled;
    [SerializeField] private GameEvent correctOutcome;
    [SerializeField] private GameEvent incorrectOutcome;
    [SerializeField] private GameEvent cardDrop;
    [SerializeField] private GameEvent moneyAppearOrDisappear;
    [SerializeField] private GameEvent buttonDown;
    [SerializeField] private GameEvent buttonUp;
    #endregion
    #region In pipeline
    [SerializeField] private GameEvent rollButtonClicked;
    [SerializeField] private PlayerEvent moneyAdjustment;
    [SerializeField] private PlayerPlayerEvent moneyBetweenPlayers;
    [SerializeField] private IntIntEvent turnPlayerMovedAlongBoard;
    [SerializeField] private IntIntEvent turnPlayerMovedToSpace;
    [SerializeField] private PlayerPropertyEvent playerPropertyAdjustment;
    [SerializeField] private GameEvent nextPlayerTurn;
    [SerializeField] private PlayerCardTypeEvent playerGetsGOOJFCard;
    [SerializeField] private BoolEvent turnBegin;
    [SerializeField] private GameEvent leaveJail;
    [SerializeField] private CardTypeEvent useGOOJFCardButtonClicked;
    [SerializeField] private EstateEvent estateAddedBuilding;
    [SerializeField] private EstateEvent estateRemovedBuilding;
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
    internal PlayerPlayerEvent MoneyBetweenPlayers => moneyBetweenPlayers;
    internal IntIntEvent TurnPlayerMovedAlongBoard => turnPlayerMovedAlongBoard;
    internal IntIntEvent TurnPlayerMovedToSpace => turnPlayerMovedToSpace;
    internal PlayerPropertyEvent PlayerObtainedProperty => playerPropertyAdjustment;
    internal GameEvent NextPlayerTurn => nextPlayerTurn;
    internal PlayerCardTypeEvent PlayerGetsGOOJFCard => playerGetsGOOJFCard;
    internal BoolEvent TurnBegin => turnBegin;
    internal GameEvent LeaveJail => leaveJail;
    internal CardTypeEvent UseGOOJFCardButtonClicked => useGOOJFCardButtonClicked;
    internal EstateEvent EstateAddedBuilding => estateAddedBuilding;
    internal EstateEvent EstateRemovedBuilding => estateRemovedBuilding;
    #endregion



    #region UI invoking
    public void call_DoublesTickBoxUpdate() => doublesTickBoxUpdate.invoke();
    public void call_PayFiftyButtonClicked() => payFiftyButtonClicked.invoke();
    public void call_TokenSettled() => tokenSettled.invoke();
    public void call_CorrectOutcome() => correctOutcome.invoke();
    public void call_IncorrectOutcome() => incorrectOutcome.invoke();
    public void call_CardDrop() => cardDrop.invoke();
    public void call_MoneyAppearOrDisappear() => moneyAppearOrDisappear.invoke();
    public void call_buttonDown() => buttonDown.invoke();
    public void call_buttonUp() => buttonUp.invoke();
    #endregion



    #region UI public subscribing
    public void sub_DoublesTickBoxUpdate(Action a) => doublesTickBoxUpdate.Listeners += a;
    public void sub_PayFiftyButtonClicked(Action a) => payFiftyButtonClicked.Listeners += a;
    public void sub_TokenSettled(Action a) => tokenSettled.Listeners += a;
    public void sub_CorrectOutcome(Action a) => correctOutcome.Listeners += a;
    public void sub_IncorrectOutcome(Action a) => incorrectOutcome.Listeners += a;
    public void sub_CardDrop(Action a) => cardDrop.Listeners += a;
    public void sub_MoneyAppearOrDisappear(Action a) => moneyAppearOrDisappear.Listeners += a;
    public void sub_ButtonDown(Action a) => buttonDown.Listeners += a;
    public void sub_ButtonUp(Action a) => buttonUp.Listeners += a;


    public void sub_RollButtonClicked(Action a) => rollButtonClicked.Listeners += a;
    public void sub_MoneyAdjustment(Action<PlayerInfo> a) => moneyAdjustment.Listeners += a;
    public void sub_MoneyBetweenPlayers(Action<PlayerInfo, PlayerInfo> a) => moneyBetweenPlayers.Listeners += a;
    public void sub_TurnPlayerMovedAlongBoard(Action<int, int> a) => turnPlayerMovedAlongBoard.Listeners += a;
    public void sub_TurnPlayerMovedToSpace(Action<int, int> a) => turnPlayerMovedToSpace.Listeners += a;
    public void sub_PlayerPropertyAdjustment(Action<PlayerInfo, PropertyInfo> a) => playerPropertyAdjustment.Listeners += a;
    public void sub_NextPlayerTurn(Action a) => nextPlayerTurn.Listeners += a;
    public void sub_PlayerGetsGOOJFCard(Action<PlayerInfo, CardType> a) => playerGetsGOOJFCard.Listeners += a;
    public void sub_TurnBegin(Action<bool> a) => turnBegin.Listeners += a;
    public void sub_LeaveJail(Action a) => leaveJail.Listeners += a;
    public void sub_UseGOOJFCardButtonClicked(Action<CardType> a) => useGOOJFCardButtonClicked.Listeners += a;
    public void sub_EstateAddedBuilding(Action<EstateInfo> a) => estateAddedBuilding.Listeners += a;
    public void sub_EstateRemovedBuilding(Action<EstateInfo> a) => estateRemovedBuilding.Listeners += a;
    #endregion



    #region UI public unsubscribing
    public void unsub_DoublesTickBoxUpdate(Action a) => doublesTickBoxUpdate.Listeners -= a;
    public void unsub_PayFiftyButtonClicked(Action a) => payFiftyButtonClicked.Listeners -= a;
    public void unsub_TokenSettled(Action a) => tokenSettled.Listeners -= a;
    public void unsub_RollButtonClicked(Action a) => rollButtonClicked.Listeners -= a;
    public void unsub_NextPlayerTurn(Action a) => nextPlayerTurn.Listeners -= a;
    public void unsub_TurnBegin(Action<bool> a) => turnBegin.Listeners -= a;
    public void unsub_UseGOOJFCardButtonClicked(Action<CardType> a) => useGOOJFCardButtonClicked.Listeners -= a;
    #endregion
}
