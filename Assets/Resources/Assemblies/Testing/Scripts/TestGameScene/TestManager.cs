using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour {
    [SerializeField] private ReferencePasser referencePasser;



    private void Awake() {
        int playerNum = 4;
        GameFactory gameFactory = new GameFactory();
        gameFactory.makeRiggedDiceGame(playerNum);
        referencePasser.GamePlayer = gameFactory.GamePlayer;
        GameState.game = gameFactory.GameStateInfo;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
