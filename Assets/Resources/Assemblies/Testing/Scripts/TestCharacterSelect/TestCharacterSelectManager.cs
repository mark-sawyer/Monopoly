using UnityEngine;

public class TestCharacterSelectManager : MonoBehaviour {
    //[SerializeField] private GameEvent playerNumberQuestion;
    private bool spacePressed = false;

    private void Update() {
        if (!spacePressed && Input.GetKeyDown(KeyCode.Space)) {
            spacePressed = true;
            //playerNumberQuestion.invoke();
        }
    }
}
