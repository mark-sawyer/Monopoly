using UnityEngine.Events;

public static class GameEvents {
    public static readonly UnityEvent<TokenMover, int> tokenOnSpaceChanged = new UnityEvent<TokenMover, int>();
    public static readonly UnityEvent tokenSettled = new UnityEvent();
    public static readonly UnityEvent<EstateInfo> propertyPurchaseQuestion = new UnityEvent<EstateInfo>();
}
