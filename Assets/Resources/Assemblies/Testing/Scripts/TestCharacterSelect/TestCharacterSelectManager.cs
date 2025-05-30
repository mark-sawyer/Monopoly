using UnityEngine;

public class TestCharacterSelectManager : MonoBehaviour {
    [SerializeField] private QuestionEventsAndPrefabs questionEventRaiser;
    private bool spacePressed = false;

    private void Update() {
        if (!spacePressed && Input.GetKeyDown(KeyCode.Space)) {
            spacePressed = true;
            questionEventRaiser.PlayerNumberQuestion.invoke();
        }
    }
}
