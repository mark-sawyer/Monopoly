using UnityEngine;

public class Manager : MonoBehaviour {
    [SerializeField] private ReferencePasser referencePasser;



    private void Awake() {
        int playerNum = 4;
        GameFactory gameFactory = new GameFactory();
        gameFactory.makeGame(playerNum);
        referencePasser.GamePlayer = gameFactory.GamePlayer;
        GameState.game = gameFactory.GameStateInfo;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
