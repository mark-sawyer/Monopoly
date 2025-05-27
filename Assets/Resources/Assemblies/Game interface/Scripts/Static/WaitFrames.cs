using System;
using System.Collections;

public static class WaitFrames {
    public static IEnumerator exe(int frames, Action a) {
        for (int i = 0; i < frames; i++) {
            yield return null;
        }
        a.Invoke();
    }
    public static IEnumerator exe<T>(int frames, Action<T> a, T arg) {
        for (int i = 0; i < frames; i++) {
            yield return null;
        }
        a.Invoke(arg);
    }
}
