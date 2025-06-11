using System.Collections.Generic;
using UnityEngine;

public class QuestionDropper : MonoBehaviour {
    private Queue<DroppedQuestion> questionQueue = new();
    [SerializeField] private ScreenCover screenCover;
    #region GameEvents
    [SerializeField] private GameEvent questionAsked;
    [SerializeField] private GameEvent questionAnswered;
    [SerializeField] private GameEvent allQuestionsAnswered;
    #endregion
    #region Question Prefabs
    [SerializeField] private GameObject playerNumberQuestionPrefab;
    [SerializeField] private GameObject tokenSelectionQuestionPrefab;
    [SerializeField] private GameObject purchaseQuestionPrefab;
    [SerializeField] private GameObject incomeTaxQuestionPrefab;
    [SerializeField] private GameObject unmortgageQuestionPrefab;
    #endregion
    #region Question GameEvents
    [SerializeField] private GameEvent playerNumberQuestion;
    [SerializeField] private GameEvent<int> tokenSelectionQuestion;
    [SerializeField] private GameEvent<PlayerInfo> incomeTaxQuestion;
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> purchaseQuestion;
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> unmortgageQuestion;
    #endregion



    #region MonoBehaviour
    private void Start() {
        playerNumberQuestion.Listeners += instantiatePlayerNumberQuestion;
        tokenSelectionQuestion.Listeners += instantiateTokenSelectionQuestion;
        incomeTaxQuestion.Listeners += instantiateIncomeTaxQuestion;
        purchaseQuestion.Listeners += instantiatePurchaseQuestion;
        unmortgageQuestion.Listeners += instantiateUnmortgageQuestion;
        questionAnswered.Listeners += resolveQuestionAnswered;
    }
    #endregion



    #region instantiate
    private void instantiatePlayerNumberQuestion() {
        GameObject question = Instantiate(playerNumberQuestionPrefab, transform);
        PlayerNumberSelection playerNumberSelection = question.GetComponent<PlayerNumberSelection>();
        addQuestionToQueue(playerNumberSelection);
    }
    private void instantiateTokenSelectionQuestion(int players) {
        GameObject question = Instantiate(tokenSelectionQuestionPrefab, transform);
        TokenSelection tokenSelection = question.GetComponent<TokenSelection>();
        tokenSelection.setup(players);
        addQuestionToQueue(tokenSelection);
    }
    private void instantiatePurchaseQuestion(PlayerInfo player, PropertyInfo property) {
        GameObject question = Instantiate(purchaseQuestionPrefab, transform);
        PurchaseQuestion purchaseQuestion = question.GetComponent<PurchaseQuestion>();
        purchaseQuestion.setup(player, property);
        addQuestionToQueue(purchaseQuestion);
    }
    private void instantiateIncomeTaxQuestion(PlayerInfo player) {
        GameObject question = Instantiate(incomeTaxQuestionPrefab, transform);
        IncomeTaxQuestion incomeTaxQuestion = question.GetComponent<IncomeTaxQuestion>();
        incomeTaxQuestion.setup(player);
        addQuestionToQueue(incomeTaxQuestion);
    }
    private void instantiateUnmortgageQuestion(PlayerInfo player, PropertyInfo property) {
        GameObject question = Instantiate(unmortgageQuestionPrefab, transform);
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
