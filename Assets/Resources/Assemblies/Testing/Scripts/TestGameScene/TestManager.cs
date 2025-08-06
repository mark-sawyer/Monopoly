using UnityEngine;

public class TestManager : MonoBehaviour {
    [SerializeField] private GameDataUpdater gameDataUpdater;



    private void Awake() {
        int playerNum = 2;
        GameFactory gameFactory = new GameFactory();
        gameFactory.makeTestGame(playerNum, 1500);
        GameState.game = gameFactory.GameStateInfo;
        gameDataUpdater.setup(gameFactory.GamePlayer);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            DataUIPipelineEventHub.Instance.call_MoneyAdjustment(GameState.game.TurnPlayer, 50);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            DataUIPipelineEventHub.Instance.call_MoneyAdjustment(GameState.game.TurnPlayer, -50);
        }
    }
}
