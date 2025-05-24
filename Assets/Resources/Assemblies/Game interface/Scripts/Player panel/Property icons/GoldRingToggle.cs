using UnityEngine;
using UnityEngine.UI;

public class GoldRingToggle : MonoBehaviour {
    public void toggle(bool enabled) {
        Color colour = enabled ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 1f, 1f, 0f);
        for (int i = 0; i < 8; i++) {
            transform.GetChild(0).GetChild(i).GetComponent<Image>().color = colour;
        }
    }
}
