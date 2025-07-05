#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIAutoUpdater), true)]
public class UIAutoUpdaterEditor : Editor {
    private void OnEnable() {
        EditorApplication.update += checkForUIChange;
    }
    private void OnDisable() {
        EditorApplication.update -= checkForUIChange;
    }
    private void checkForUIChange() {
        if (target == null) return;
        UIAutoUpdater updater = (UIAutoUpdater)target;
        if (updater.changeOccurred()) {
            updater.updateUI();
            updater.updateLastState();
            updateChildrenRecursively(updater.transform);
        }
    }
    private void updateChildrenRecursively(Transform parent) {
        for (int i = 0; i < parent.childCount; i++) {
            Transform child = parent.GetChild(i);
            UIAutoUpdater childUpdater = child.GetComponent<UIAutoUpdater>();
            if (childUpdater != null) {
                childUpdater.updateUI();
                childUpdater.updateLastState();
            }
            updateChildrenRecursively(child);
        }
    }
}
#endif
