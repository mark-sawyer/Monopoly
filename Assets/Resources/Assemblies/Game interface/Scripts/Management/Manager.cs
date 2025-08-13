using UnityEngine;

public class Manager : MonoBehaviour {
    private void Awake() {
        int playerNum = 4;
        GameFactory gameFactory = new GameFactory();
        gameFactory.makeGame(playerNum);
        GameState.game = gameFactory.GameStateInfo;
        GameDataUpdater gameDataUpdater = new GameDataUpdater(gameFactory.GamePlayer);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
