using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/GameEvent")]
internal class GameEvent : ScriptableObject {
    private event Action action;
    public event Action Listeners {
        add {
            if (action != null && action.GetInvocationList().Contains(value)) {
                throw new InvalidOperationException(
                  $"Listener {value.Method.DeclaringType}.{value.Method.Name} already registered to {name}."
              );
            }
            action += value;
        }
        remove { action -= value; }
    }
    public void invoke() {
        action?.Invoke();
    }
}

internal class GameEvent<T> : ScriptableObject {
    private event Action<T> action;
    internal event Action<T> Listeners {
        add {
            if (action != null && action.GetInvocationList().Contains(value)) {
                throw new InvalidOperationException(
                  $"Listener {value.Method.DeclaringType}.{value.Method.Name} already registered to {name}."
              );
            }
            action += value;
        }
        remove { action -= value; }
    }
    internal void invoke(T value) {
        action?.Invoke(value);
    }
}

internal class GameEvent<T1, T2> : ScriptableObject {
    private event Action<T1, T2> action;
    internal event Action<T1, T2> Listeners {
        add {
            if (action != null && action.GetInvocationList().Contains(value)) {
                throw new InvalidOperationException(
                  $"Listener {value.Method.DeclaringType}.{value.Method.Name} already registered to {name}."
              );
            }
            action += value;
        }
        remove { action -= value; }
    }
    internal void invoke(T1 value1, T2 value2) {
        action?.Invoke(value1, value2);
    }
}

internal class GameEvent<T1, T2, T3> : ScriptableObject {
    private event Action<T1, T2, T3> action;
    internal event Action<T1, T2, T3> Listeners {
        add {
            if (action != null && action.GetInvocationList().Contains(value)) {
                throw new InvalidOperationException(
                  $"Listener {value.Method.DeclaringType}.{value.Method.Name} already registered to {name}."
              );
            }
            action += value;
        }
        remove { action -= value; }
    }
    internal void invoke(T1 value1, T2 value2, T3 value3) {
        action?.Invoke(value1, value2, value3);
    }
}

internal class GameEvent<T1, T2, T3, T4> : ScriptableObject {
    private event Action<T1, T2, T3, T4> action;
    internal event Action<T1, T2, T3, T4> Listeners {
        add {
            if (action != null && action.GetInvocationList().Contains(value)) {
                throw new InvalidOperationException(
                  $"Listener {value.Method.DeclaringType}.{value.Method.Name} already registered to {name}."
              );
            }
            action += value;
        }
        remove { action -= value; }
    }
    internal void invoke(T1 value1, T2 value2, T3 value3, T4 value4) {
        action?.Invoke(value1, value2, value3, value4);
    }
}
