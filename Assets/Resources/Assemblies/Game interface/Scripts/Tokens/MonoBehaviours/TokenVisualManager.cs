using UnityEngine;
using System.Collections.Generic;

public class TokenVisualManager : MonoBehaviour {
    [SerializeField] private GameObject tokenPrefab;
    [SerializeField] private TokenSprites[] tokenSprites;
    [SerializeField] private TokenColours[] tokenColours;



    #region Singleton boilerplate
    public static TokenVisualManager Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion



    #region MonoBehaviour
    private void Start() {
        IEnumerable<PlayerInfo> players = GameState.game.PlayerInfos;
        int i = 0;
        SpaceVisual startingSpaceVisual = SpaceVisualManager.Instance.getSpaceVisual(0);
        foreach (PlayerInfo player in players) {
            Vector3 startingPosition = getStartingPosition(player);
            GameObject newToken = Instantiate(tokenPrefab, startingPosition, Quaternion.identity, transform);
            newToken.GetComponent<TokenVisual>().setup(player, startingSpaceVisual.getScale(player));
            i += 1;
        }
        UIEventHub.Instance.sub_TurnPlayerMovedAlongBoard(moveTurnPlayer);
        UIEventHub.Instance.sub_TurnPlayerMovedToSpace(moveTurnPlayerDirectly);
    }
    #endregion



    #region public
    public TokenVisual getTokenVisual(int index) {
        return transform.GetChild(index).GetComponent<TokenVisual>();
    }
    #endregion



    #region private
    private Vector3 getStartingPosition(PlayerInfo player) {
        SpaceVisual startingSpaceVisual = SpaceVisualManager.Instance.getSpaceVisual(0);
        return startingSpaceVisual.getMinorPoint(player);
    }
    private void moveTurnPlayer(int startingIndex, int diceValues) {
        int playerIndex = GameState.game.IndexOfTurnPlayer;
        TokenVisual turnTokenVisual = getTokenVisual(playerIndex);
        turnTokenVisual.moveTokenAlongBoard(startingIndex, diceValues);
    }
    private void moveTurnPlayerDirectly(int startingIndex, int newIndex) {
        int playerIndex = GameState.game.IndexOfTurnPlayer;
        TokenVisual turnTokenVisual = getTokenVisual(playerIndex);
        turnTokenVisual.moveTokenDirectlyToSpace(startingIndex, newIndex);
    }
    #endregion
}
