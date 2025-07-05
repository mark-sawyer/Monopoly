using UnityEngine;

public abstract class ScreenAnimation : MonoBehaviour {
    public abstract void appear();
}

public abstract class ScreenAnimation<T> : ScreenAnimation {
    public abstract void setup(T t);
}

public abstract class ScreenAnimation<T1, T2> : ScreenAnimation {
    public abstract void setup(T1 t1, T2 t2);
}
