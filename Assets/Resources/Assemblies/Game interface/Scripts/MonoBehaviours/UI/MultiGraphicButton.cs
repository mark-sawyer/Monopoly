using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MultiGraphicButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    [SerializeField] public List<Graphic> extraGraphics;
    [SerializeField] private Button button;
    [SerializeField] private Color pressedColor;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private float fadeTime = 0.1f;

    public void OnPointerDown(PointerEventData eventData) {
        foreach (var g in extraGraphics)
            g.CrossFadeColor(pressedColor, fadeTime, true, true);
    }

    public void OnPointerUp(PointerEventData eventData) {
        foreach (var g in extraGraphics)
            g.CrossFadeColor(normalColor, fadeTime, true, true);
    }
}
