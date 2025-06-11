using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/GameEvent")]
public class GameEvent : ScriptableObject {
    private event Action action;
    public event Action Listeners {
        add { action += value; }
        remove { action -= value; }
    }

    public void invoke() {
        action?.Invoke();
    }
}

public class GameEvent<T> : ScriptableObject {
    private event Action<T> action;
    public event Action<T> Listeners {
        add { action += value; }
        remove { action -= value; }
    }

    public void invoke(T value) {
        action?.Invoke(value);
    }
}

public class GameEvent<T1, T2> : ScriptableObject {
    private event Action<T1, T2> action;
    public event Action<T1, T2> Listeners {
        add { action += value; }
        remove { action -= value; }
    }

    public void invoke(T1 value1, T2 value2) {
        action?.Invoke(value1, value2);
    }
}

public class GameEvent<T1, T2, T3> : ScriptableObject {
    private event Action<T1, T2, T3> action;
    public event Action<T1, T2, T3> Listeners {
        add { action += value; }
        remove { action -= value; }
    }

    public void invoke(T1 value1, T2 value2, T3 value3) {
        action?.Invoke(value1, value2, value3);
    }
}
