using UnityEngine;

public class TradingTokenReceiver : GhostReceiver {
    [SerializeField] private GameObject tokenIconGameObject;
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private TradingTokenReceiver otherTradingTokenReceiver;
    private bool hasToken;
    private Token token;
    private PlayerColour colour;



    #region public
    public bool HasToken => hasToken;
    public Token Token => token;
    public PlayerColour Colour => colour;
    public override void receiveGhost(DraggableGhost ghost) {
        TokenIconGhost tokenIconGhost = (TokenIconGhost)ghost;
        token = tokenIconGhost.Token;
        colour = tokenIconGhost.Colour;
        hasToken = true;
        tokenIconGameObject.SetActive(true);
        tokenIcon.setup(token, colour);
        if (otherMatchesNewTokenIcon()) {
            otherTradingTokenReceiver.removeIcon();
        }
        UIEventHub.Instance.call_TradingPlayerPlaced();
    }
    #endregion



    #region private
    private void removeIcon() {
        tokenIconGameObject.SetActive(false);
        hasToken = false;
    }
    private bool otherMatchesNewTokenIcon() {
        return otherTradingTokenReceiver.hasToken
            && token == otherTradingTokenReceiver.token
            && colour == otherTradingTokenReceiver.colour;
    }
    #endregion
}
