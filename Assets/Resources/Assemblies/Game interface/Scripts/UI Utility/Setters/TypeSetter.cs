using System.Collections.Generic;
using UnityEngine;

public abstract class TypeSetter<T> : UIAutoUpdater {
    [SerializeField] private List<MonoBehaviour> typeSettables;
    [SerializeField] private T value;
    private T lastValue;

    #region UIAutoUpdater
    public override void updateUI() {
        foreach (MonoBehaviour mb in typeSettables) {
            if (mb is TypeSettable<T> settable) {
                settable.setType(value);
            }
        }
    }
    public override bool changeOccurred() {
        return !EqualityComparer<T>.Default.Equals(lastValue, value);
    }
    public override void updateLastState() {
        lastValue = value;
    }
    #endregion
}
