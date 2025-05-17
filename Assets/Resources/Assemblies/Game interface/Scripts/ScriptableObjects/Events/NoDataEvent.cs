using System;
using UnityEngine;

public class NoDataEvent : ScriptableObject {
    private event Action eventOccurred;
    public event Action EventOccurred {
        add { eventOccurred += value; }
        remove { eventOccurred -= value; }
    }

    public void raiseEvent() {
        eventOccurred?.Invoke();
    }
}
