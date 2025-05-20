using UnityEngine;

public abstract class UIAutoUpdater : MonoBehaviour {
    // These are useful for when I want things to update quickly in the inspector, not during runtime.

    public abstract void updateUI();
    public abstract bool changeOccurred();
    public abstract void updateLastState();



    private void OnEnable() {
        updateUI();
    }
}
