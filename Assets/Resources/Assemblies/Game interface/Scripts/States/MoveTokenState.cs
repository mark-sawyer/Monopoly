using UnityEngine;

public class MoveTokenState : State {
    private Transform tokensParent;
    private Transform spacesParent;
    private Transform token;
    private Vector3 targetPosition;



    /* GameState */
    public override void enterState() {
        token = getTokenToMove();
        targetPosition = getTargetPosition();
    }
    public override void update() {
        Vector3 dir = getDirectionVector().normalized;
        token.position = token.position + (100 * Time.deltaTime * dir);
    }
    public override bool exitConditionMet() {
        return getDirectionVector().magnitude < 1f;
    }
    public override State getNextState() {
        return possibleNextStates[0];
    }



    /* public */
    public MoveTokenState(Transform tokensParent, Transform spacesParent) {
        this.tokensParent = tokensParent;
        this.spacesParent = spacesParent;
    }




    /* private */
    private Transform getTokenToMove() {
        int playerIndex = getPlayerToMoveIndex();
        return tokensParent.GetChild(playerIndex);
    }
    private Vector3 getTargetPosition() {
        int playerIndex = getPlayerToMoveIndex();
        PlayerInfo[] players = GameState.game.getPlayers();
        PlayerInfo movingPlayer = players[playerIndex];
        int spaceIndex = movingPlayer.getSpaceIndex();
        Transform spaceTransform = spacesParent.GetChild(spaceIndex);
        return new Vector3(
            spaceTransform.position.x,
            spaceTransform.position.y,
            token.position.z
        );
    }
    private int getPlayerToMoveIndex() {
        PlayerInfo turnPlayer = GameState.game.getTurnPlayer();
        int playerIndex = GameState.game.getPlayerIndex(turnPlayer) - 1;
        if (playerIndex < 0) playerIndex += GameState.game.getPlayers().Length;
        return playerIndex;
    }
    private Vector3 getDirectionVector() {
        return targetPosition - token.position;
    }
}
