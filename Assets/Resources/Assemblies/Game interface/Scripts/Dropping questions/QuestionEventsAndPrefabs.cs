using UnityEngine;

public class QuestionEventsAndPrefabs : ScriptableObject {
    #region Prefabs
    [SerializeField] private GameObject playerNumberQuestionPrefab;
    [SerializeField] private GameObject tokenSelectionQuestionPrefab;
    [SerializeField] private GameObject purchaseQuestionPrefab;
    [SerializeField] private GameObject incomeTaxQuestionPrefab;
    [SerializeField] private GameObject unmortgageQuestionPrefab;
    #endregion
    #region GameEvents
    [SerializeField] private GameEvent playerNumberQuestion;
    [SerializeField] private GameEvent<int> tokenSelectionQuestion;
    [SerializeField] private GameEvent<PlayerInfo> incomeTaxQuestion;
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> purchaseQuestion;
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> unmortgageQuestion;
    #endregion



    public GameObject PlayerNumberQuestionPrefab => playerNumberQuestionPrefab;
    public GameObject TokenSelectionQuestionPrefab => tokenSelectionQuestionPrefab;
    public GameObject PurchaseQuestionPrefab => purchaseQuestionPrefab;
    public GameObject IncomeTaxQuestionPrefab => incomeTaxQuestionPrefab;
    public GameObject UnmortgageQuestionPrefab => unmortgageQuestionPrefab;
    public GameEvent PlayerNumberQuestion => playerNumberQuestion;
    public GameEvent<int> TokenSelectionQuestion => tokenSelectionQuestion;
    public GameEvent<PlayerInfo> IncomeTaxQuestion => incomeTaxQuestion;
    public GameEvent<PlayerInfo, PropertyInfo> PurchaseQuestion => purchaseQuestion;
    public GameEvent<PlayerInfo, PropertyInfo> UnmortgageQuestion => unmortgageQuestion;
}
