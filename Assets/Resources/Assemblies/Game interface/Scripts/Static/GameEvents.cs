using UnityEngine.Events;

public static class GameEvents {
    public static readonly UnityEvent<TokenMover, int> tokenOnSpaceChanged = new UnityEvent<TokenMover, int>();
    public static readonly UnityEvent tokenSettled = new UnityEvent();

    public static readonly UnityEvent<PlayerInfo, PropertyInfo> purchaseQuestionAsked = new UnityEvent<PlayerInfo, PropertyInfo>();
    public static readonly UnityEvent<PlayerInfo> incomeTaxQuestionAsked = new UnityEvent<PlayerInfo>();
    public static readonly UnityEvent<PlayerInfo, PropertyInfo> unmortgageQuestionAsked = new UnityEvent<PlayerInfo, PropertyInfo>();
}
