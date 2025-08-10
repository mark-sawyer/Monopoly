using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GoButton : MonoBehaviour {
    /*
    [SerializeField] private Button button;
    [SerializeField] private Transform tokenReceiverParent;
    [SerializeField] private GameEvent selectedTokensChanged;



    #region MonoBehaviour
    private void Start() {
        selectedTokensChanged.Listeners += updateInteractable;
    }
    #endregion



    #region public
    public void updateInteractable() {
        button.interactable = isInteractable();
    }
    public void unsubscribe() {
        selectedTokensChanged.Listeners -= updateInteractable;
    }
    #endregion



    #region private
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
        return !tokenIcon.transform.GetChild(0).GetComponent<Image>().enabled;
    }
    #endregion
    */
}
