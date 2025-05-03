using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class TokenVisualManager : MonoBehaviour {
    [SerializeField] private GameObject tokenPrefab;
    [SerializeField] private SpaceVisual startingSpaceVisual;

    #region MonoBehaviour
    private void Start() {
        IEnumerable<PlayerInfo> players = GameState.game.getPlayers();
        int i = 0;
        foreach (PlayerInfo pi in players) {
            PlayerInfo player = pi;
            Vector3 startingPosition = getStartingPosition(i);
            GameObject newToken = Instantiate(tokenPrefab, startingPosition, Quaternion.identity, transform);
            newToken.GetComponent<TokenVisual>().setup(player, startingSpaceVisual);
            i += 1;
        }
    }
    #endregion

    #region public
    public TokenVisual getTurnTokenVisual() {
        int index = GameState.game.getIndexOfTurnPlayer();
        return transform.GetChild(index).GetComponent<TokenVisual>();
    }
    public TokenVisual getTokenVisual(int index) {
        return transform.GetChild(index).GetComponent<TokenVisual>();
    }
    public IEnumerable<TokenVisual> getTokenVisualsOnSpace(int spaceIndex) {
        IEnumerable<PlayerInfo> players = GameState.game.getPlayersOnSpace(spaceIndex);
        IEnumerable<int> indices = players.Select(x => GameState.game.getPlayerIndex(x));
        return indices.Select(x => getTokenVisual(x));
    }
    #endregion

    #region private
    private Vector3 getStartingPosition(int i) {
        float angle = (2f * Mathf.PI * i) / GameState.game.getNumberOfPlayers();
        float xOffSet = -Mathf.Cos(angle);
        float yOffSet = Mathf.Sin(angle);
        return startingSpaceVisual.getTargetPosition() + 2f * new Vector3(xOffSet, yOffSet, 0f);
    }
    #endregion

}
