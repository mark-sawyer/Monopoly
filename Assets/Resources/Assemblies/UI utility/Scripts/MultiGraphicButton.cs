using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class MultiGraphicButton : Button {
    [SerializeField] private List<Graphic> additionalGraphics = new List<Graphic>();



    protected override void OnEnable() {
        base.OnEnable();
        DoStateTransition(currentSelectionState, true);
    }
    protected override void DoStateTransition(SelectionState state, bool instant) {
        if (!IsActive() || additionalGraphics == null) return;

        Color targetColor = getTargetColour(state);
        targetColor *= colors.colorMultiplier;

        foreach (var graphic in additionalGraphics) {
            if (graphic != null) {
                graphic.CrossFadeColor(
                    targetColor,
                    instant ? 0f : colors.fadeDuration,
                    true,
                    true
                );
            }
        }

        base.DoStateTransition(state, instant);
    }
    public override void OnSelect(BaseEventData eventData) { }
    public override void OnDeselect(BaseEventData eventData) { }
    private Color getTargetColour(SelectionState state) {
        Color targetColor;
        switch (state) {
            case SelectionState.Normal:
                targetColor = colors.normalColor;
                break;
            case SelectionState.Highlighted:
                targetColor = colors.highlightedColor;
                break;
            case SelectionState.Pressed:
                targetColor = colors.pressedColor;
                break;
            case SelectionState.Selected:
                targetColor = colors.selectedColor;
                break;
            case SelectionState.Disabled:
                targetColor = colors.disabledColor;
                break;
            default:
                targetColor = Color.black;
                break;
        }
        return targetColor;
    }
}
