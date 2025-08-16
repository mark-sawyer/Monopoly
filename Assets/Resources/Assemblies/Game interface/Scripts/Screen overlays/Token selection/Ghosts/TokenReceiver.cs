using UnityEngine;
using UnityEngine.UI;

public class TokenReceiver : GhostReceiver {
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    private PlayerColour colour;
    private Token token;



    #region MonoBehaviour
    private void Start() {
        colour = PlayerColour.WHITE;
    }
    #endregion



    #region GhostReceiver
    public override void receiveGhost(DraggableGhost ghost) {
        SoundOnlyEventHub.Instance.call_Put();
        TokenDraggableGhost tokenGhost = (TokenDraggableGhost)ghost;
        token = tokenGhost.Token;
        upButton.interactable = true;
        downButton.interactable = true;
        tokenIcon.gameObject.SetActive(true);
        tokenIcon.setup(token, colour);
        ScreenOverlayEventHub.Instance.call_SelectedTokensChanged();
    }
    #endregion



    #region public
    public void colourUp() {
        int colourInt = (int)colour;
        colourInt = (colourInt - 1).mod(8);
        colour = (PlayerColour)colourInt;
        tokenIcon.setup(token, colour);
        ScreenOverlayEventHub.Instance.call_SelectedTokensChanged();
    }
    public void colourDown() {
        int colourInt = (int)colour;
        colourInt = (colourInt + 1).mod(8);
        colour = (PlayerColour)colourInt;
        tokenIcon.setup(token, colour);
        ScreenOverlayEventHub.Instance.call_SelectedTokensChanged();
    }
    #endregion
}
