using UnityEngine;

public class TestPayRentManager : MonoBehaviour {
    [SerializeField] private GameEvent<PlayerInfo, int> ownerOwedRent;

    private void Start() {
        
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            int playerNum = Random.Range(2, 9);
            GameFactory gameFactory = new GameFactory();
            gameFactory.makeGame(playerNum);
            GameState.game = gameFactory.GameStateInfo;
            ownerOwedRent.invoke(
                GameState.game.getPlayerInfo(Random.Range(1, playerNum)),
                Random.Range(10, 2001)
            );
        }
    }
}
