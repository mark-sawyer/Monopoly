using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour {
    [SerializeField] private GameDataUpdater gameDataUpdater;



    private void Awake() {
        int playerNum = 4;
        GameFactory gameFactory = new GameFactory();
        gameFactory.makeRiggedDiceGame(playerNum);
        GameState.game = gameFactory.GameStateInfo;
        gameDataUpdater.setup(gameFactory.GamePlayer);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
