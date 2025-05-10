using System.Collections;
using UnityEngine;

public class QuestionDropper : MonoBehaviour {
    [SerializeField] private TokenVisualManager tokenVisualManager;
    [SerializeField] private GameObject purchaseQuestionPrefab;
    [SerializeField] private GameObject incomeTaxQuestionPrefab;
    [SerializeField] private GameObject unmortgageQuestionPrefab;



    private void Start() {
        GameEvents.purchaseQuestionAsked.AddListener(askPurchaseQuestion);
        GameEvents.incomeTaxQuestionAsked.AddListener(askIncomeTaxQuestion);
        GameEvents.unmortgageQuestionAsked.AddListener(askUnmortgageQuestion);
    }




    #region private
    private void askPurchaseQuestion(PlayerInfo player, PropertyInfo property) {
        GameObject question = Instantiate(purchaseQuestionPrefab, transform);
        PurchaseQuestion purchaseQuestion = question.GetComponent<PurchaseQuestion>();
        purchaseQuestion.setup(player, property, tokenVisualManager);
        StartCoroutine(bringDownQuestion((RectTransform)question.transform));
    }
    private void askIncomeTaxQuestion(PlayerInfo player) {
        GameObject question = Instantiate(incomeTaxQuestionPrefab, transform);
        IncomeTaxQuestion incomeTaxQuestion = question.GetComponent<IncomeTaxQuestion>();
        incomeTaxQuestion.setup(player, tokenVisualManager);
        StartCoroutine(bringDownQuestion((RectTransform)question.transform));
    }
    private void askUnmortgageQuestion(PlayerInfo player, PropertyInfo property) {
        GameObject question = Instantiate(unmortgageQuestionPrefab, transform);
        UnmortgageQuestion unmortgageQuestion = question.GetComponent<UnmortgageQuestion>();
        unmortgageQuestion.setup(player, property, tokenVisualManager);
        StartCoroutine(bringDownQuestion((RectTransform)question.transform));
    }
    private IEnumerator bringDownQuestion(RectTransform rt) {
        Vector2 start = rt.anchoredPosition;
        for (int i = 1; i <= InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION; i++) {
            Vector2 newPos = start - (i * start / InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION);
            rt.anchoredPosition = newPos;
            yield return null;
        }
    }
    #endregion
}
