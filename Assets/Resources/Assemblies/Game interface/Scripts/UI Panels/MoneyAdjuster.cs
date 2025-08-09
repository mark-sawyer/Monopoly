using Codice.CM.Common.Merge;
using System.Collections;
using TMPro;
using UnityEngine;

public class MoneyAdjuster : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI frontText;
    [SerializeField] private TextMeshProUGUI backText;
    [SerializeField] private GameObject floatingMoneyPrefab;
    private Coroutine wobble;
    private Coroutine glow;
    private string moneyString = "$1500";
    private int frames;
    private const float AMPLITUDE = 0.06f;



    #region MonoBehaviour
    private void Start() {
        frames = FrameConstants.MONEY_UPDATE;
    }
    #endregion



    #region public
    public int DisplayedMoney => int.Parse(frontText.text.Substring(1));
    public void adjustMoney(PlayerInfo playerInfo) {
        int currentMoney = playerInfo.Money;
        int priorMoney = DisplayedMoney;
        int difference = currentMoney - priorMoney;

        GameObject floatingMoney = Instantiate(
            floatingMoneyPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );
        floatingMoney.GetComponent<FloatingMoneyDifference>().floatAway(difference);
        changeMoneyVisual(currentMoney);
        startMoneyWobble(difference > 0);
    }
    public void adjustMoney(DebtInfo debtInfo) {
        int currentDebt = debtInfo.Owed;
        int priorDebt = DisplayedMoney;
        int difference = currentDebt - priorDebt;
        changeMoneyVisual(currentDebt);

        GameObject floatingMoney = Instantiate(
            floatingMoneyPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );
        floatingMoney.GetComponent<FloatingMoneyDifference>().floatAway(difference);
        changeMoneyVisual(currentDebt);
        startMoneyWobble(false);
    }
    public void adjustMoneyQuietly(PlayerInfo playerInfo) {
        int currentMoney = playerInfo.Money;
        changeMoneyVisual(currentMoney);
    }
    public void setStartingMoney(int startingMoney) {
        changeMoneyVisual(startingMoney);
    }
    #endregion



    #region private
    private void changeMoneyVisual(int money) {
        moneyString = "$" + money.ToString();
        frontText.text = moneyString;
        backText.text = moneyString;
    }
    private void startMoneyWobble(bool isPositive) {
        if (wobble != null) {
            StopCoroutine(wobble);
            StopCoroutine(glow);
        }
        wobble = StartCoroutine(wobbleText());
        if (isPositive) glow = StartCoroutine(glowGreen());
        else glow = StartCoroutine(glowRed());
    }
    private float getGlowVal(float x, float scalar) {
        if (x <= frames / 3f) return (3f * x * scalar) / frames;
        else if (x <= 2 * frames / 3f) return scalar;
        else return scalar * ((-3f * x) / frames + 3f);
    }
    private IEnumerator glowGreen() {
        for (int i = 1; i <= frames; i++) {
            float greenVal = getGlowVal(i, 202f / 255f);
            float redVal = getGlowVal(i, 42f / 255f);
            frontText.color = new Color(redVal, greenVal, 0f);
            yield return null;
        }
        frontText.color = new Color(0f, 0f, 0f);
    }
    private IEnumerator glowRed() {
        for (int i = 1; i <= frames; i++) {
            float glowVal = getGlowVal(i, 1f);
            frontText.color = new Color(glowVal, 0f, 0f);
            yield return null;
        }
        frontText.color = new Color(0f, 0f, 0f);
    }
    private IEnumerator wobbleText() {
        float getScale(float i) {
            float x = (4f * Mathf.PI * i) / frames;
            return AMPLITUDE * Mathf.Sin(x) + 0.9f;
        }

        for (int i = 1; i <= frames; i++) {
            float scale = getScale(i);
            transform.localScale = new Vector3(scale, 1f, 1f);
            yield return null;
        }
        transform.localScale = new Vector3(0.9f, 1f, 1f);
    }
    #endregion
}
