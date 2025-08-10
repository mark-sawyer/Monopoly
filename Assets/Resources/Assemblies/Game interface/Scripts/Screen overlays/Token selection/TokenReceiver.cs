using UnityEngine;
using UnityEngine.UI;
/*
public class TokenReceiver : GhostReceiver {
    /*
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private GameEvent selectedTokensChanged;
    private Token token;
    private PlayerColour colour = PlayerColour.WHITE;



    #region public
    public override void receiveGhost(DraggableGhost ghost) {
        //UIToken uiToken = (UIToken)ghost;
        //token = uiToken.Token;
        //turnOnButtons();
        //turnOnIconImages();
        //tokenIcon.setup(token, colour);
        //selectedTokensChanged.invoke();
    }
    public void colourUp() {
        int colourInt = (int)colour;
        colourInt = (colourInt - 1).mod(8);
        colour = (PlayerColour)colourInt;
        tokenIcon.setup(token, colour);
        selectedTokensChanged.invoke();
    }
    public void colourDown() {
        int colourInt = (int)colour;
        colourInt = (colourInt + 1).mod(8);
        colour = (PlayerColour)colourInt;
        tokenIcon.setup(token, colour);
        selectedTokensChanged.invoke();
    }
    #endregion



    #region private
    private void turnOnButtons() {
        upButton.interactable = true;
        downButton.interactable = true;
    }
    private void turnOnIconImages() {
        tokenIcon.transform.GetChild(0).GetComponent<Image>().enabled = true;
        tokenIcon.transform.GetChild(1).GetComponent<Image>().enabled = true;
        tokenIcon.transform.GetChild(2).GetChild(0).GetComponent<Image>().enabled = true;
        tokenIcon.transform.GetChild(2).GetChild(1).GetComponent<Image>().enabled = true;
    }
    #endregion
}
*/