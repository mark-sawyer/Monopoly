using UnityEngine;

public class Manager : MonoBehaviour {
    [SerializeField] private ReferencePasser referencePasser;
    [SerializeField] private StateManager stateManager;
    private Game game;



    private void Awake() {
        int playerNum = 4;
        game = new Game(playerNum);
        GameState.game = game;
        stateManager.setup(game);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
