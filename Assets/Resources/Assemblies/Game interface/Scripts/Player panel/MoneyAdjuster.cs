using System.Collections;
using TMPro;
using UnityEngine;

public class MoneyAdjuster : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI frontText;
    [SerializeField] private TextMeshProUGUI backText;
    [SerializeField] private GameObject floatingMoneyPrefab;
    [SerializeField] private Transform floatingMoneySpawnPoint;
    private string moneyString = "$1500";
    private const int FRAMES = 90;
    private const float AMPLITUDE = 0.06f;



    #region public
    public void adjustMoney(int difference) {
        GameObject floatingMoney = Instantiate(
            floatingMoneyPrefab,
            floatingMoneySpawnPoint.position,
            Quaternion.identity,
            floatingMoneySpawnPoint
        );
        floatingMoney.GetComponent<FloatingMoneyDifference>().floatAway(difference);
        changeMoneyVisual(difference);
        startMoneyWobble(difference > 0);
    }
    #endregion





    private void changeMoneyVisual(int adjustment) {
        int moneyInt = int.Parse(moneyString.Substring(1));
        int newMoney = moneyInt + adjustment;
        moneyString = "$" + newMoney.ToString();
        frontText.text = moneyString;
        backText.text = moneyString;
    }
    private void startMoneyWobble(bool isPositive) {
        StartCoroutine(wobbleText());
        if (isPositive) StartCoroutine(glowGreen());
        else StartCoroutine(glowRed());
    }
    private float getGlowVal(float x, float scalar) {
        if (x <= FRAMES / 3f) return (3f * x * scalar) / FRAMES;
        else if (x <= 2 * FRAMES / 3f) return scalar;
        else return scalar * ((-3f * x) / FRAMES + 3f);
    }
    private IEnumerator glowGreen() {
        for (int i = 1; i <= FRAMES; i++) {
            float greenVal = getGlowVal(i, 202f / 255f);
            float redVal = getGlowVal(i, 42f / 255f);
            frontText.color = new Color(redVal, greenVal, 0f);
            yield return null;
        }
        frontText.color = new Color(0f, 0f, 0f);
    }
    private IEnumerator glowRed() {
        for (int i = 1; i <= FRAMES; i++) {
            float glowVal = getGlowVal(i, 1f);
            frontText.color = new Color(glowVal, 0f, 0f);
            yield return null;
        }
        frontText.color = new Color(0f, 0f, 0f);
    }
    private IEnumerator wobbleText() {
        float getScale(float i) {
            float x = (4f * Mathf.PI * i) / FRAMES;
            return AMPLITUDE * Mathf.Sin(x) + 0.9f;
        }

        for (int i = 1; i <= FRAMES; i++) {
            float scale = getScale(i);
            transform.localScale = new Vector3(scale, 1f, 1f);
            yield return null;
        }
        transform.localScale = new Vector3(0.9f, 1f, 1f);
    }
}
