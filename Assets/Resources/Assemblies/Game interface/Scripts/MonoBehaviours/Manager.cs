using UnityEngine;

public class Manager : MonoBehaviour {
    [SerializeField] private TokenVisualiser tokenVisualiser;
    [SerializeField] private PlayerPanels playerPanels;
    private Game game;

    private void Awake() {
        int playerNum = 4;
        game = new Game(playerNum);
        tokenVisualiser.instantiateTokens(game.getPlayers());
        playerPanels.setupPanels(game.getPlayers());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            game.turn();
            GameEvents.visualUpdateTriggered.Invoke();
        }
    }
}
