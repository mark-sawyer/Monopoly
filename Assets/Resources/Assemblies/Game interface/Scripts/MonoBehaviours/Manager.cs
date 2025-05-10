using UnityEngine;

public class Manager : MonoBehaviour {
    [SerializeField] private ReferencePasser referencePasser;
    private StateManager stateManager;
    private Game game;



    private void Awake() {
        int playerNum = 4;
        game = new Game(playerNum);
        GameState.game = game;
        stateManager = new StateManager(game, referencePasser);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        stateManager.update();

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            GameEvents.purchaseQuestionAsked.Invoke(
                game.getPlayerInfo(Random.Range(0, game.NumberOfPlayers)),
                game.DELETE_THIS_LATER()
            );
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            GameEvents.incomeTaxQuestionAsked.Invoke(
                game.getPlayerInfo(Random.Range(0, game.NumberOfPlayers))
            );
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            GameEvents.unmortgageQuestionAsked.Invoke(
                game.getPlayerInfo(Random.Range(0, game.NumberOfPlayers)),
                game.DELETE_THIS_LATER()
            );
        }
    }
}
