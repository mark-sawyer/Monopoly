using UnityEngine;

public class TestManager : MonoBehaviour {
    [SerializeField] private GameDataUpdater gameDataUpdater;



    private void Awake() {
        int playerNum = 2;
        GameFactory gameFactory = new GameFactory();
        gameFactory.makeTestGame(playerNum, 50);
        GameState.game = gameFactory.GameStateInfo;
        gameDataUpdater.setup(gameFactory.GamePlayer);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            DataEventHub.Instance.call_MoneyAdjustment(GameState.game.TurnPlayer, 50);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            DataEventHub.Instance.call_MoneyAdjustment(GameState.game.TurnPlayer, -50);
        }
    }
}
