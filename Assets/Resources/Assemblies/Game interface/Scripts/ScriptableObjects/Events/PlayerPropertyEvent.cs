using System;
using UnityEngine;

public class PlayerPropertyEvent : ScriptableObject {
    private event Action<PlayerInfo, PropertyInfo> eventOccurred;
    public event Action<PlayerInfo, PropertyInfo> EventOccurred {
        add { eventOccurred += value; }
        remove { eventOccurred -= value; }
    }

    public void raiseEvent(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        eventOccurred?.Invoke(playerInfo, propertyInfo);
    }
}
