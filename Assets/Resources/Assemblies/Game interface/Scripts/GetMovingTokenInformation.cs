using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMovingTokenInformation {
    private Transform tokensParent;
    private Game game;

    public GetMovingTokenInformation(Game game, Transform tokensParent) {
        this.game = game;
        this.tokensParent = tokensParent;
    }
    public Transform getTokenToMove() {
        Player turnPlayer = game.getTurnPlayer();
        int playerIndex = game.getPlayerIndex(turnPlayer) - 1;
        if (playerIndex < 0) playerIndex += game.getPlayers().Length;
        return tokensParent.GetChild(playerIndex);
    }
}
