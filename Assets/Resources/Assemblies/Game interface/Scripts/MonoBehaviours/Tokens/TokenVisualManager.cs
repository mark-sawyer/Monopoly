using UnityEngine;
using System.Collections.Generic;

public class TokenVisualManager : MonoBehaviour {
    [SerializeField] private GameObject tokenPrefab;
    [SerializeField] private SpaceVisualManager spaceVisualManager;
    [SerializeField] private TokenSprites[] tokenSprites;



    #region MonoBehaviour
    private void Start() {
        IEnumerable<PlayerInfo> players = GameState.game.getPlayers();
        int i = 0;
        foreach (PlayerInfo player in players) {
            Vector3 startingPosition = getStartingPosition(i);
            GameObject newToken = Instantiate(tokenPrefab, startingPosition, Quaternion.identity, transform);
            newToken.GetComponent<TokenVisual>().setup(player, spaceVisualManager, tokenTypeToTokenSprites(player.getToken()));
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
    public TokenSprites tokenTypeToTokenSprites(Token token) {
        switch (token) {
            case Token.BOOT: return tokenSprites[0];
            case Token.CAR: return tokenSprites[1];
            case Token.DOG: return tokenSprites[2];
            case Token.HAT: return tokenSprites[3];
            case Token.IRON: return tokenSprites[4];
            case Token.SHIP: return tokenSprites[5];
            case Token.THIMBLE: return tokenSprites[6];
            default: return tokenSprites[7];
        }
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
