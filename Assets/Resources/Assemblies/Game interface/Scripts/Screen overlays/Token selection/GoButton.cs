using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GoButton : MonoBehaviour {
    [SerializeField] private Button button;
    [SerializeField] private Transform tokenReceiverParent;



    #region MonoBehaviour
    private void Start() {
        ScreenOverlayEventHub.Instance.sub_SelectedTokensChanged(updateInteractable);
    }
    private void OnDestroy() {
        ScreenOverlayEventHub.Instance.unsub_SelectedTokensChanged(updateInteractable);
    }
    #endregion



    #region public
    public void buttonClicked() {
        List<Token> tokens = getTokens();
        List<PlayerColour> colours = getColours();
        Manager.Instance.startGame(tokens, colours);
        ScreenOverlayEventHub.Instance.call_RemoveScreenOverlay();
    }
    #endregion



    #region private
    private void updateInteractable() {
        button.interactable = isInteractable();
    }
    private bool isInteractable() {
        int[] encodings = new int[tokenReceiverParent.childCount];
        for (int i = 0; i < tokenReceiverParent.childCount; i++) {
            TokenIcon tokenIcon = getTokenIcon(i);
            if (isDisabled(tokenIcon)) return false;
            int tokenInt = (int)tokenIcon.Token;
            int colourInt = 8 * (int)tokenIcon.Colour;
            encodings[i] = tokenInt + colourInt;
        }
        return encodings.Length == encodings.Distinct().Count();
    }    
    private TokenIcon getTokenIcon(int i) {
        return tokenReceiverParent.GetChild(i).GetChild(1).GetComponent<TokenIcon>();
    }
    private bool isDisabled(TokenIcon tokenIcon) {
        return !tokenIcon.gameObject.activeSelf;
    }
    private List<Token> getTokens() {
        int players = tokenReceiverParent.childCount;
        List<Token> tokens = new(players);
        for (int i = 0; i < players; i++) {
            TokenIcon tokenIcon = getTokenIcon(i);
            Token token = tokenIcon.Token;
            tokens.Add(token);
        }
        return tokens;
    }
    private List<PlayerColour> getColours() {
        int players = tokenReceiverParent.childCount;
        List<PlayerColour> colours = new(players);
        for (int i = 0; i < players; i++) {
            TokenIcon tokenIcon = getTokenIcon(i);
            PlayerColour colour = tokenIcon.Colour;
            colours.Add(colour);
        }
        return colours;
    }
    #endregion
}
