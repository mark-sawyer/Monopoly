using UnityEngine;

public class TestManager : MonoBehaviour {
    private void Awake() {
        int playerNum = 2;
        GameFactory gameFactory = new GameFactory();
        gameFactory.makeTestGame(playerNum, 500);
        GameState.game = gameFactory.GameStateInfo;
        GameDataUpdater gameDataUpdater = new GameDataUpdater(gameFactory.GamePlayer);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            DataUIPipelineEventHub.Instance.call_MoneyAdjustment(GameState.game.TurnPlayer, 50);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9)) {
            DataUIPipelineEventHub.Instance.call_MoneyAdjustment(GameState.game.TurnPlayer, 1);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            PlayerInfo turnPlayer = GameState.game.TurnPlayer;
            int money = turnPlayer.Money;
            if (money > 0) {
                int moneyLost = money < 50 ? money : 50;
                DataUIPipelineEventHub.Instance.call_MoneyAdjustment(GameState.game.TurnPlayer, -moneyLost);
            }
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMultiply)) {
            PlayerInfo turnPlayer = GameState.game.TurnPlayer;
            int money = turnPlayer.Money;
            if (money > 0) {
                DataUIPipelineEventHub.Instance.call_MoneyAdjustment(GameState.game.TurnPlayer, -1);
            }
        }
    }
}
