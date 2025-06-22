using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingMoneyDifference : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI frontText;
    [SerializeField] private TextMeshProUGUI backText;
    private const int FRAMES = 90;

    public void floatAway(int value) {
        Color textColour = value > 0 ? new Color(42f/255f, 202f/255f, 0f) : new Color(255f, 0f, 0f);
        frontText.color = textColour;
        string sign = value > 0 ? "+" : "";
        string moneyText = sign + value.ToString();
        frontText.text = moneyText;
        backText.text = moneyText;
        Vector3 positionChange = value > 0 ? new Vector3(0f, 1f, 0f) : new Vector3(0f, -1f, 0f);
        StartCoroutine(floatCoroutine(textColour, positionChange));
    }

    private IEnumerator floatCoroutine(Color textColour, Vector3 positionChange) {
        for (int i = 0; i < FRAMES; i++) {
            transform.position += positionChange;
            textColour.a = (float)-i / FRAMES + 1f;
            frontText.color = textColour;
            backText.color = textColour;
            yield return null;
        }
        Destroy(gameObject);
    }
}
