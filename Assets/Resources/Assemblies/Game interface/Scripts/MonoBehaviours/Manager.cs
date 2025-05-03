using UnityEngine;

public class Manager : MonoBehaviour {
    [SerializeField] private ReferencePasser referencePasser;
    private StateManager stateManager;
    private Game game;



    private void Awake() {
        int playerNum = 8;
        game = new Game(playerNum);
        GameState.game = game;
        stateManager = new StateManager(game, referencePasser);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        stateManager.update();
    }
}
