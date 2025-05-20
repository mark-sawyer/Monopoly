#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(MultiGraphicButton))]
public class MultiGraphicButtonEditor : ButtonEditor {
    SerializedProperty additionalGraphics;

    protected override void OnEnable() {
        base.OnEnable();
        additionalGraphics = serializedObject.FindProperty("additionalGraphics");
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(additionalGraphics, true);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
