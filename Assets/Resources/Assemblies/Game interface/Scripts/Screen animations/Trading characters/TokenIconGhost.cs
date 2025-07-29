using UnityEngine;

public class TokenIconGhost : DraggableGhost {
    [SerializeField] private TokenIcon thisTokenIcon;

    public override void ghostSetup() {
        Transform parent = transform.parent;
        TokenIcon parentTokenIcon = parent.GetComponent<TokenIcon>();
        Token token = parentTokenIcon.Token;
        PlayerColour colour = parentTokenIcon.Colour;
        thisTokenIcon.setup(token, colour);
    }
}
