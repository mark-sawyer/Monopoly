using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TokenVisualManager : MonoBehaviour {
    [SerializeField] private GameObject tokenPrefab;
    [SerializeField] private SpaceVisualManager spaceVisualManager;



    #region MonoBehaviour
    private void Start() {
        IEnumerable<PlayerInfo> players = GameState.game.getPlayers();
        int i = 0;
        foreach (PlayerInfo player in players) {
            Vector3 startingPosition = getStartingPosition(i);
            GameObject newToken = Instantiate(tokenPrefab, startingPosition, Quaternion.identity, transform);
            newToken.GetComponent<TokenVisual>().setup(player, spaceVisualManager);
            newToken.GetComponent<TokenMover>().setup(player, spaceVisualManager);
            newToken.GetComponent<TokenScaler>().setup(player, spaceVisualManager);
            i += 1;
        }
    }
    #endregion



    #region public
    public TokenVisual getTokenVisual(int index) {
        return transform.GetChild(index).GetComponent<TokenVisual>();
    }
    public TokenMover getTokenMover(int index) {
        return transform.GetChild(index).GetComponent<TokenMover>();
    }
    public TokenScaler getTokenScaler(int index) {
        return transform.GetChild(index).GetComponent<TokenScaler>();
    }
    #endregion



    #region private
    public Vector3 getStartingPosition(int order) {
        int totalPlayers = GameState.game.getNumberOfPlayers();
        SpaceVisual startingSpaceVisual = spaceVisualManager.getSpaceVisual(0);
        return startingSpaceVisual.getFinalPosition(totalPlayers, order);
    }
    #endregion
}
