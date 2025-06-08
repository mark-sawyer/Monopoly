using System;
using System.Collections;
using UnityEngine;

public class WaitFrames : MonoBehaviour {
    #region Singleton boilerplate
    public static WaitFrames Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion



    public void exe(int frames, Action a) {
        StartCoroutine(waitThenAction(frames, a));
    }
    public void exe<T>(int frames, Action<T> a, T arg) {
        StartCoroutine(waitThenAction(frames, a, arg));
    }
    public void exe<T1, T2>(int frames, Action<T1, T2> a, T1 arg1, T2 arg2) {
        StartCoroutine(waitThenAction(frames, a, arg1, arg2));
    }



    private static IEnumerator waitThenAction(int frames, Action a) {
        for (int i = 0; i < frames; i++) {
            yield return null;
        }
        a.Invoke();
    }
    private static IEnumerator waitThenAction<T>(int frames, Action<T> a, T arg) {
        for (int i = 0; i < frames; i++) {
            yield return null;
        }
        a.Invoke(arg);
    }
    private static IEnumerator waitThenAction<T1, T2>(int frames, Action<T1, T2> a, T1 arg1, T2 arg2) {
        for (int i = 0; i < frames; i++) {
            yield return null;
        }
        a.Invoke(arg1, arg2);
    }
}
