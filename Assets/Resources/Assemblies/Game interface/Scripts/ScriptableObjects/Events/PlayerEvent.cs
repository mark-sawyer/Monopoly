using System;
using UnityEngine;

public class PlayerEvent : ScriptableObject {
    private event Action<PlayerInfo> eventOccurred;
    public event Action<PlayerInfo> EventOccurred {
        add { eventOccurred += value; }
        remove { eventOccurred -= value; }
    }

    public void raiseEvent(PlayerInfo playerInfo) {
        eventOccurred?.Invoke(playerInfo);
    }
}
