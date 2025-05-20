using UnityEngine;

public class Manager : MonoBehaviour {
    [SerializeField] private ReferencePasser referencePasser;
    private Game game;



    private void Awake() {
        int playerNum = 4;
        game = new Game(playerNum);
        referencePasser.GamePlayer = game;
        GameState.game = game;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
