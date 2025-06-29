using UnityEngine;

public class TestManager : MonoBehaviour {
    [SerializeField] private GameDataUpdater gameDataUpdater;



    private void Awake() {
        int playerNum = 3;
        GameFactory gameFactory = new GameFactory();
        gameFactory.makeTestGame(playerNum, 15000);
        GameState.game = gameFactory.GameStateInfo;
        gameDataUpdater.setup(gameFactory.GamePlayer);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
