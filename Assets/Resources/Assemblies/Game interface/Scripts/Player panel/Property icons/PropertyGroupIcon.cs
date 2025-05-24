using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class PropertyGroupIcon : MonoBehaviour {
    [SerializeField] Canvas canvas;

    public abstract void updateVisual(PlayerInfo playerInfo);
    public IEnumerator pulseAndUpdate(PlayerInfo playerInfo) {
        float getScale(float x) {
            if (x <= 5) return 1f + 0.2f * x;
            else return 2f - (1f / 15f) * (x - 5f);
        }

        canvas.overrideSorting = true;
        canvas.sortingOrder = 1;
        for (int i = 1; i <= 20; i++) {
            float scale = getScale(i);
            transform.localScale = new Vector3(scale, scale, scale);
            if (i == 5) updateVisual(playerInfo);
            yield return null;
        }
        transform.localScale = new Vector3(1f, 1f, 1f);
        canvas.overrideSorting = false;
        canvas.sortingOrder = 0;
    }
    protected void updatePanelColour(Color colour) {
        for (int i = 0; i < 9; i++) {
            transform.GetChild(0).GetChild(i).GetComponent<Image>().color = colour;
        }
    }
}
