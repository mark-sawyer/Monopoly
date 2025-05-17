using UnityEngine;

public abstract class UIAutoUpdater : MonoBehaviour {
    public abstract void updateUI();
    public abstract bool changeOccurred();
    public abstract void updateLastState();



    private void OnEnable() {
        updateUI();
    }
}
