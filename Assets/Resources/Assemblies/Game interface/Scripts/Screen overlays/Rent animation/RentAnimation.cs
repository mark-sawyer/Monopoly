using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RentAnimation : ScreenOverlay<DebtInfo> {
    #region Internal references
    [SerializeField] private RectTransform textRT;
    [SerializeField] private RectTransform debtorRT;
    [SerializeField] private RectTransform creditorRT;
    [SerializeField] private TextMeshProUGUI owedText;
    [SerializeField] private TextMeshProUGUI paidText;
    #endregion
    #region External references
    [SerializeField] private GameObject moneyPrefab;
    #endregion
    #region Private attributes
    private Dictionary<int, MoneyNoteEnum> moneyValToEnum = new Dictionary<int, MoneyNoteEnum>() {
        { 1, MoneyNoteEnum.ONE },
        { 5, MoneyNoteEnum.FIVE },
        { 10, MoneyNoteEnum.TEN },
        { 20, MoneyNoteEnum.TWENTY },
        { 50, MoneyNoteEnum.FIFTY },
        { 100, MoneyNoteEnum.ONE_HUNDRED },
        { 500, MoneyNoteEnum.FIVE_HUNDRED }
    };
    private ScreenOverlaySizeAdjuster screenAnimationSizeAdjuster;
    #endregion
    #region numeric fields
    private int[] moneyNoteValues = new int[7] { 1, 5, 10, 20, 50, 100, 500 };
    private int owed;
    private int paid = 0;
    private float totalGameWealth;
    private float debtorWealth;
    private float creditorWealth;
    private float evenProportion;
    private const int MOVEMENT_FRAMES = 50;
    private const int MONEY_THROW_FRAMES = 100;
    private const float HORIZONTAL_PROPORTION = 800f / 1920f;
    private const float DEFAULT_WIDTH = 800f;
    #endregion



    #region ScreenAnimation
    public override void setup(DebtInfo debtInfo) {
        int getTotalGameWealth() {
            int totalGameWealth = 0;
            for (int i = 0; i < GameState.game.NumberOfPlayers; i++) {
                PlayerInfo playerInfo = GameState.game.getPlayerInfo(i);
                totalGameWealth += playerInfo.TotalWorth;
            }
            return totalGameWealth;
        }

        owed = debtInfo.TotalOwed;
        PlayerInfo debtorPlayer = debtInfo.DebtorInfo;
        PlayerInfo creditorPlayer = (PlayerInfo)((SingleCreditorDebtInfo)debtInfo).Creditor;
        debtorRT.GetComponent<TokenIcon>().setup(debtorPlayer.Token, debtorPlayer.Colour);
        creditorRT.GetComponent<TokenIcon>().setup(creditorPlayer.Token, creditorPlayer.Colour);
        owedText.text = "$" + owed.ToString();

        evenProportion = 1.0f / GameState.game.NumberOfPlayers;
        totalGameWealth = getTotalGameWealth();
        debtorWealth = debtorPlayer.TotalWorth;
        creditorWealth = creditorPlayer.TotalWorth;

        float debtorScale = getScale(debtorWealth / totalGameWealth);
        float creditorScale = getScale(creditorWealth / totalGameWealth);
        debtorRT.localScale = new Vector3(debtorScale, debtorScale, debtorScale);
        creditorRT.localScale = new Vector3(creditorScale, creditorScale, creditorScale);

        screenAnimationSizeAdjuster = new ScreenOverlaySizeAdjuster(
            HORIZONTAL_PROPORTION,
            DEFAULT_WIDTH,
            (RectTransform)transform
        );
        screenAnimationSizeAdjuster.adjustChildrenSize();
    }
    public override void appear() {
        SoundPlayer.Instance.play_UhOh();
        float width = ((RectTransform)transform).rect.width;
        StartCoroutine(moveToken(debtorRT, 2 * width / 5f));
        StartCoroutine(moveToken(creditorRT, -2 * width / 5f));
        StartCoroutine(moveText());
        WaitFrames.Instance.beforeAction(
            MOVEMENT_FRAMES + 10,
            () => StartCoroutine(sendMoney())
        );
    }
    #endregion



    #region private
    private IEnumerator moveToken(RectTransform t, float xDestination) {
        float xStart = t.anchoredPosition.x;
        for (int i = 1; i <= MOVEMENT_FRAMES; i++) {
            float xPos = LinearValue.exe(i, xStart, xDestination, MOVEMENT_FRAMES);
            t.anchoredPosition = new Vector3(xPos, 0f, 0f);
            yield return null;
        }
        t.anchoredPosition = new Vector3(xDestination, 0f, 0f);
    }
    private IEnumerator moveText() {
        float yStart = textRT.anchoredPosition.y;
        float canvasHeight = ((RectTransform)transform).rect.height;
        float yDestination = -0.4f * canvasHeight;
        for (int i = 1; i <= MOVEMENT_FRAMES; i++) {
            float yPos = LinearValue.exe(i, yStart, yDestination, MOVEMENT_FRAMES);
            textRT.anchoredPosition = new Vector3(0f, yPos, 0f);
            yield return null;
        }
        textRT.anchoredPosition = new Vector3(0f, yDestination, 0f);
        yield return null;
    }
    private IEnumerator sendMoney() {
        void adjustDebtorScale(float nextPayment) {
            debtorWealth -= nextPayment;
            float newScale = getScale(debtorWealth / totalGameWealth) * screenAnimationSizeAdjuster.Scale;
            debtorRT.localScale = new Vector3(newScale, newScale, newScale);
        }

        int money = GameState.game.TurnPlayer.Money;
        int remainingPayment = money - owed >= 0 ? owed : money;
        while (remainingPayment > 0) {
            int nextPayment = getNextPayment(remainingPayment);
            remainingPayment -= nextPayment;
            adjustDebtorScale(nextPayment);
            MoneyNoteEnum nextMoneyNote = moneyValToEnum[nextPayment];
            GameObject moneyInstance = getNextNote(nextMoneyNote);
            StartCoroutine(moveMoneyNote(moneyInstance.transform, nextPayment));
            for (int i = 0; i < 20; i++) yield return null;
        }
        for (int i = 0; i < MONEY_THROW_FRAMES + 50; i++) yield return null;
        ScreenOverlayFunctionEventHub.Instance.call_RemoveScreenOverlay();
    }
    private IEnumerator moveMoneyNote(Transform moneyTransform, int amount) {
        void adjustCreditorScale(float paymentReceived) {
            creditorWealth += paymentReceived;
            float newScale = getScale(creditorWealth / totalGameWealth) * screenAnimationSizeAdjuster.Scale;
            creditorRT.localScale = new Vector3(newScale, newScale, newScale);
        }
        float getY(RectTransform t) {
            float height = t.rect.height;
            float scale = t.localScale.x;
            float yPos = t.localPosition.y;
            return yPos + scale * height / 2f;
        }
        float getDerivative(float x, float a, float b) {
            return 2 * a * x + b;
        }

        float yStart = getY(debtorRT);
        float yEnd = getY(creditorRT);
        float yMid = (yStart + yEnd) / 2f + Random.Range(80f, 160f);
        float xStart = debtorRT.localPosition.x;
        float xEnd = creditorRT.localPosition.x;

        SoundPlayer.Instance.play_PaperSound();
        moneyTransform.localPosition = new Vector3(xStart, yStart, 0f);

        Matrix3x3 mat = new Matrix3x3(
            Mathf.Pow(xStart, 2), xStart, 1f,
            0f, 0f, 1f,
            Mathf.Pow(xEnd, 2), xEnd, 1f
        );
        Vector3 vec = new Vector3(yStart, yMid, yEnd);
        Vector3 coefs = mat.inverse() * vec;
        for (int i = 0; i < MONEY_THROW_FRAMES; i++) {
            float prop = (float)i / MONEY_THROW_FRAMES;
            float x = LinearValue.exe(prop, xStart, xEnd, 1f);
            float dydx = getDerivative(x, coefs.x, coefs.y);
            float angle = Mathf.Atan(dydx) * Mathf.Rad2Deg;
            moneyTransform.localPosition = new Vector3(
                x,
                coefs.x * Mathf.Pow(x, 2) + coefs.y * x + coefs.z,
                0f
            );
            moneyTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }

        SoundPlayer.Instance.play_PaperSound();
        adjustCreditorScale(amount);
        paid += amount;
        paidText.text = "$" + paid.ToString();
        Destroy(moneyTransform.gameObject);
    }
    private int getNextPayment(int remainingDebt) {
        return moneyNoteValues.Where(x => x <= remainingDebt).Max();
    }
    private GameObject getNextNote(MoneyNoteEnum moneyNoteEnum) {
        GameObject moneyInstance = screenAnimationSizeAdjuster.InstantiateAdjusted(moneyPrefab);
        MoneyNote moneyNote = moneyInstance.GetComponent<MoneyNote>();
        moneyNote.setup(moneyNoteEnum);
        return moneyInstance;
    }
    private float getScale(float proportion) {
        if (proportion < evenProportion) return LinearValue.exe(proportion, 0f, evenProportion, 2f, 5f);
        else return LinearValue.exe(proportion, evenProportion, 1f, 5f, 11f);
    }
    #endregion
}
