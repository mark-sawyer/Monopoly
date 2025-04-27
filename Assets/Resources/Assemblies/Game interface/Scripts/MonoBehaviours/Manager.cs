using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    [SerializeField] private TokenVisualiser tokenVisualiser;
    [SerializeField] private PlayerPanels playerPanels;
    [SerializeField] private DieVisual die1Visual;
    [SerializeField] private DieVisual die2Visual;
    [SerializeField] private Button rollButton;
    private Game game;
    private GameStateManager gameStateManager;

    private void Awake() {
        int playerNum = 8;
        game = new Game(playerNum);
        tokenVisualiser.instantiateTokens(game.getPlayers());
        playerPanels.setupPanels(game.getPlayers());
        die1Visual.setDie(game.getDie(0));
        die2Visual.setDie(game.getDie(1));
        GetMovingTokenInformation getMovingTokenInformation = new GetMovingTokenInformation(game, tokenVisualiser.transform);
        gameStateManager = new GameStateManager(game, rollButton, getMovingTokenInformation);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        gameStateManager.update();
    }
}
