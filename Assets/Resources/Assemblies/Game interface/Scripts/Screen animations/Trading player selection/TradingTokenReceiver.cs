using System.Collections.Generic;
using UnityEngine;

public class TradingTokenReceiver : GhostReceiver {
    [SerializeField] private GameObject tokenIconGameObject;
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private TradingTokenReceiver otherTradingTokenReceiver;
    private PlayerInfo playerInfo;



    #region public
    public PlayerInfo PlayerInfo => playerInfo;
    public override void receiveGhost(DraggableGhost ghost) {
        TokenIconGhost tokenIconGhost = (TokenIconGhost)ghost;
        Token token = tokenIconGhost.Token;
        PlayerColour colour = tokenIconGhost.Colour;
        tokenIconGameObject.SetActive(true);
        tokenIcon.setup(token, colour);
        playerInfo = getPlayerInfo(token, colour);
        if (playerInfo == otherTradingTokenReceiver.PlayerInfo) {
            otherTradingTokenReceiver.removeIcon();
        }
        UIEventHub.Instance.call_TradingPlayerPlaced();
    }
    #endregion



    #region private
    private void removeIcon() {
        tokenIconGameObject.SetActive(false);
        playerInfo = null;
    }
    private PlayerInfo getPlayerInfo(Token token, PlayerColour colour) {
        IEnumerable<PlayerInfo> activePlayers = GameState.game.ActivePlayers;
        foreach (PlayerInfo playerInfo in activePlayers) {
            Token thisToken = playerInfo.Token;
            PlayerColour thisColour = playerInfo.Colour;
            bool match = token == thisToken && colour == thisColour;
            if (match) return playerInfo;
        }
        throw new System.Exception("No matching player found.");
    }
    #endregion
}
