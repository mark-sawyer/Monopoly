using UnityEngine;
using UnityEngine.UI;

public class TokenSelection : MonoBehaviour {
    /*
    [SerializeField] private GameEvent playerNumberQuestion;
    [SerializeField] private GoButton goButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject tokenSelectedPanelPrefab;
    [SerializeField] private Transform choosableTokensParent;
    [SerializeField] private Transform tokenSelectedPanelSpawnTransform;



    #region DroppedQuestion
    protected override void dropComplete() {
        backButton.interactable = true;
        for (int i = 0; i < 8; i++) {
            choosableTokensParent.GetChild(i).GetComponent<Image>().raycastTarget = true;
        }
    }
    #endregion



    #region public
    public void setup(int players) {
        float tokenSelectedPanelWidth = ((RectTransform)tokenSelectedPanelPrefab.transform).rect.width;
        float gapWidth = 20f;
        float fullWidth = tokenSelectedPanelWidth * players + gapWidth * (players - 1);
        float minPos = (tokenSelectedPanelWidth - fullWidth) / 2;
        for (int i = 0; i < players; i++) {
            float pushRight = i * (tokenSelectedPanelWidth + gapWidth);
            float xPos = minPos + pushRight;
            GameObject tokenSelectedPanel = Instantiate(tokenSelectedPanelPrefab, tokenSelectedPanelSpawnTransform);
            tokenSelectedPanel.transform.localPosition = new Vector3(xPos, 0f, 0f);
        }
    }
    public void backClicked() {
        playerNumberQuestion.invoke();
        questionAnswered.invoke();
        goButton.unsubscribe();
        disappear();
    }
    public void goClicked() {
        questionAnswered.invoke();
        disappear();
    }
    #endregion
    */
}
