using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject manageProperties;
    [SerializeField] private GameObject tokens;
    [SerializeField] private GameObject stateManager;



    #region Singleton boilerplate
    public static Manager Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion



    #region public
    public void startGame(List<Token> tokens, List<PlayerColour> colours) {
        GameFactory gameFactory = new GameFactory();
        gameFactory.makeGame(tokens, colours);
        GameState.game = gameFactory.GameStateInfo;
        GameDataUpdater gameDataUpdater = new GameDataUpdater(gameFactory.GamePlayer);
        turnOnGameObjects();
    }
    #endregion



    #region private
    private void turnOnGameObjects() {
        playerUI.SetActive(true);
        gameUI.SetActive(true);
        manageProperties.SetActive(true);
        tokens.SetActive(true);
        stateManager.SetActive(true);
    }
    #endregion
}
