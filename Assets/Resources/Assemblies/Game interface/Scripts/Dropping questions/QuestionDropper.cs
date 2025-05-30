using System.Collections.Generic;
using UnityEngine;

public class QuestionDropper : MonoBehaviour {
    private Queue<DroppedQuestion> questionQueue = new();
    [SerializeField] private ScreenCover screenCover;
    [SerializeField] private QuestionEventsAndPrefabs qRefs;
    #region GameEvents
    [SerializeField] private GameEvent questionAsked;
    [SerializeField] private GameEvent questionAnswered;
    [SerializeField] private GameEvent allQuestionsAnswered;
    #endregion



    #region MonoBehaviour
    private void Start() {
        qRefs.PlayerNumberQuestion.Listeners += instantiatePlayerNumberQuestion;
        qRefs.TokenSelectionQuestion.Listeners += instantiateTokenSelectionQuestion;
        qRefs.IncomeTaxQuestion.Listeners += instantiateIncomeTaxQuestion;
        qRefs.PurchaseQuestion.Listeners += instantiatePurchaseQuestion;
        qRefs.UnmortgageQuestion.Listeners += instantiateUnmortgageQuestion;
        questionAnswered.Listeners += resolveQuestionAnswered;
    }
    #endregion



    #region instantiate
    private void instantiatePlayerNumberQuestion() {
        GameObject question = Instantiate(qRefs.PlayerNumberQuestionPrefab, transform);
        PlayerNumberSelection playerNumberSelection = question.GetComponent<PlayerNumberSelection>();
        addQuestionToQueue(playerNumberSelection);
    }
    private void instantiateTokenSelectionQuestion(int players) {
        GameObject question = Instantiate(qRefs.TokenSelectionQuestionPrefab, transform);
        TokenSelection tokenSelection = question.GetComponent<TokenSelection>();
        tokenSelection.setup(players);
        addQuestionToQueue(tokenSelection);
    }
    private void instantiatePurchaseQuestion(PlayerInfo player, PropertyInfo property) {
        GameObject question = Instantiate(qRefs.PurchaseQuestionPrefab, transform);
        PurchaseQuestion purchaseQuestion = question.GetComponent<PurchaseQuestion>();
        purchaseQuestion.setup(player, property);
        addQuestionToQueue(purchaseQuestion);
    }
    private void instantiateIncomeTaxQuestion(PlayerInfo player) {
        GameObject question = Instantiate(qRefs.IncomeTaxQuestionPrefab, transform);
        IncomeTaxQuestion incomeTaxQuestion = question.GetComponent<IncomeTaxQuestion>();
        incomeTaxQuestion.setup(player);
        addQuestionToQueue(incomeTaxQuestion);
    }
    private void instantiateUnmortgageQuestion(PlayerInfo player, PropertyInfo property) {
        GameObject question = Instantiate(qRefs.UnmortgageQuestionPrefab, transform);
        UnmortgageQuestion unmortgageQuestion = question.GetComponent<UnmortgageQuestion>();
        unmortgageQuestion.setup(player, property);
        addQuestionToQueue(unmortgageQuestion);
    }
    #endregion



    #region private
    private void startDroppingQuestions() {
        screenCover.startFadeIn();
        dropQuestion();
    }
    private void dropQuestion() {
        questionQueue.Peek().drop();
        questionAsked.invoke();
    }
    private void addQuestionToQueue(DroppedQuestion droppedQuestion) {
        questionQueue.Enqueue(droppedQuestion);
        if (questionQueue.Peek() == droppedQuestion) startDroppingQuestions();
    }
    private void resolveQuestionAnswered() {
        questionQueue.Dequeue().disappear();
        if (questionQueue.Count > 0) dropQuestion();
        else {
            allQuestionsAnswered.invoke();
            screenCover.startFadeOut();
        }
    }
    #endregion
}
