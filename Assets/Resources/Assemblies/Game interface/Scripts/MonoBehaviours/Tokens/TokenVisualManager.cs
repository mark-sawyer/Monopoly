using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;
using System;

public class TokenVisualManager : MonoBehaviour {
    [SerializeField] private GameObject tokenPrefab;
    [SerializeField] private SpaceVisualManager spaceVisualManager;
    [SerializeField] private TokenSprites[] tokenSprites;
    [SerializeField] private TokenColours[] tokenColours;



    #region MonoBehaviour
    private void Start() {
        IEnumerable<PlayerInfo> players = GameState.game.PlayerInfos;
        int i = 0;
        foreach (PlayerInfo player in players) {
            Vector3 startingPosition = getStartingPosition(i);
            GameObject newToken = Instantiate(tokenPrefab, startingPosition, Quaternion.identity, transform);
            newToken.GetComponent<TokenVisual>().setup(
                player,
                spaceVisualManager,
                tokenTypeToTokenSprites(player.Token),
                playerColourToTokenColours(player.Colour)
            );
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
    public TokenColours playerColourToTokenColours(PlayerColour colour) {
        switch (colour) {
            case PlayerColour.BLUE: return tokenColours[0];
            case PlayerColour.GREEN: return tokenColours[1];
            case PlayerColour.MAGENTA: return tokenColours[2];
            case PlayerColour.ORANGE: return tokenColours[3];
            case PlayerColour.PURPLE: return tokenColours[4];
            case PlayerColour.RED: return tokenColours[5];
            case PlayerColour.WHITE: return tokenColours[6];
            default: return tokenColours[7];
        }
    }
    #endregion



    #region private
    public Vector3 getStartingPosition(int order) {
        int totalPlayers = GameState.game.NumberOfPlayers;
        SpaceVisual startingSpaceVisual = spaceVisualManager.getSpaceVisual(0);
        return startingSpaceVisual.getFinalPosition(totalPlayers, order);
    }
    #endregion
}
