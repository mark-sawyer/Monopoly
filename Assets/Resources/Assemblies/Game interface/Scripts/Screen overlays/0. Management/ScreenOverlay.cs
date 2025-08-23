using UnityEngine;

public abstract class ScreenOverlay : MonoBehaviour {
    public abstract void appear();
}
public abstract class ScreenOverlay<T> : ScreenOverlay {
    public abstract void setup(T t);
}
public abstract class ScreenOverlay<T1, T2> : ScreenOverlay {
    public abstract void setup(T1 t1, T2 t2);
}
