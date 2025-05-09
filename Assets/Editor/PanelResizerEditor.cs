using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Panel))]
public class PanelResizerEditor : Editor {
    private Vector2 lastSize;

    private void OnEnable() {
        var rect = ((Panel)target).GetComponent<RectTransform>();
        if (rect != null) lastSize = rect.sizeDelta;

        EditorApplication.update += CheckForResize;
    }
    private void OnDisable() {
        EditorApplication.update -= CheckForResize;
    }
    private void CheckForResize() {
        if (target == null) return;

        var panel = (Panel)target;
        var rect = panel.GetComponent<RectTransform>();
        if (rect == null) return;

        if (lastSize != rect.sizeDelta) {
            lastSize = rect.sizeDelta;
            panel.adjust();
        }
    }
}
