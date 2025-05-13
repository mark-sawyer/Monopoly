using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Panel))]
public class PanelEditor : Editor {
    private Vector2 lastSize;
    private Color lastColour;
    private int lastRadius;

    private void OnEnable() {
        Panel panel = (Panel)target;
        if (panel != null) setLast(panel);
        EditorApplication.update += CheckForResize;
    }
    private void OnDisable() {
        EditorApplication.update -= CheckForResize;
    }
    private void CheckForResize() {
        if (target == null) return;
        Panel panel = (Panel)target;
        if (changeOccurred(panel)) {
            setLast(panel);
            panel.adjust();
        }
    }
    private bool changeOccurred(Panel panel) {
        return panel.GetComponent<RectTransform>().sizeDelta != lastSize ||
            panel.Colour != lastColour ||
            panel.Radius != lastRadius;
    }
    private void setLast(Panel panel) {
        lastSize = panel.GetComponent<RectTransform>().sizeDelta;
        lastColour = panel.Colour;
        lastRadius = panel.Radius;
    }
}
