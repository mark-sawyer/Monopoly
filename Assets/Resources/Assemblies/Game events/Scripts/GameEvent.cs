using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/GameEvent")]
internal class GameEvent : ScriptableObject {
    private event Action action;
    public event Action Listeners {
        add { action += value; }
        remove { action -= value; }
    }
    public void invoke() {
        action?.Invoke();
    }
}

internal class GameEvent<T> : ScriptableObject {
    private event Action<T> action;
    internal event Action<T> Listeners {
        add { action += value; }
        remove { action -= value; }
    }
    internal void invoke(T value) {
        action?.Invoke(value);
    }
}

internal class GameEvent<T1, T2> : ScriptableObject {
    private event Action<T1, T2> action;
    internal event Action<T1, T2> Listeners {
        add { action += value; }
        remove { action -= value; }
    }
    internal void invoke(T1 value1, T2 value2) {
        action?.Invoke(value1, value2);
    }
}

internal class GameEvent<T1, T2, T3> : ScriptableObject {
    private event Action<T1, T2, T3> action;
    internal event Action<T1, T2, T3> Listeners {
        add { action += value; }
        remove { action -= value; }
    }
    internal void invoke(T1 value1, T2 value2, T3 value3) {
        action?.Invoke(value1, value2, value3);
    }
}
