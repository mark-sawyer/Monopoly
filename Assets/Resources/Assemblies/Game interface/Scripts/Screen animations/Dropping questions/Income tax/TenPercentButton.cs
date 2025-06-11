using System.Collections;
using TMPro;
using UnityEngine;

public class TenPercentButton : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textMesh;

    public void updateText(int amount) {
        textMesh.text = "$" + amount.ToString();
        StartCoroutine(pulse());
    }

    private IEnumerator pulse() {
        float getScale(float x) {
            if (x <= 5) return 1f + 0.06f * x;
            else return 1.4f - 0.02f * x;
        }

        for (int i = 1; i <= 20; i++) {
            float scale = getScale(i);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
