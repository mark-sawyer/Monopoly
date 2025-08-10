using UnityEngine;

public class TokenIconGhost : DraggableGhost {
    [SerializeField] private TokenIcon thisTokenIcon;
    private Token token;
    private PlayerColour colour;



    public Token Token => token;
    public PlayerColour Colour => colour;
    public override void ghostSetup() {
        Transform parent = transform.parent;
        TokenIcon parentTokenIcon = parent.GetComponent<TokenIcon>();
        token = parentTokenIcon.Token;
        colour = parentTokenIcon.Colour;
        thisTokenIcon.setup(token, colour);        
    }
}
