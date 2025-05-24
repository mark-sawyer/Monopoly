using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HotelIconToggle : MonoBehaviour {
    [SerializeField] private Image outlineImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private GameColour hotelColour;
    private Color outlineColour = Color.black;

    private void Start() {
        outlineImage.color = Color.black;
        iconImage.color = hotelColour.Colour;
        toggle(false);
    }

    public void toggle(bool enabled) {
        float alpha = enabled ? 1f : 0f;
        setColour(outlineImage, alpha);
        setColour(iconImage, alpha);
    }

    private void setColour(Image image, float alpha) {
        Color currentColour = image.color;
        currentColour.a = alpha;
        image.color = currentColour;
    }
}
