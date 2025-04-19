using UnityEngine;

public class Manager : MonoBehaviour {
    [SerializeField] private TokenVisualiser tokenVisualiser;
    private Game game;

    private void Awake() {
        int playerNum = 4;
        game = new Game(playerNum);
        tokenVisualiser.instantiateTokens(game.getPlayerVisuals());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            game.turn();
            GameEvents.visualUpdateTriggered.Invoke();
        }
    }
}
