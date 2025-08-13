using UnityEngine;
using UnityEngine.UI;

public class TokenDraggableGhost : DraggableGhost {
    [SerializeField] private Image tokenImage;
    [SerializeField] private Image silouhetteImage;
    private Token token;



    public Token Token => token;
    public override void ghostSetup() {
        token = transform.parent.GetComponent<ChoosableToken>().Token;
        TokenDictionary tokenDictionary = TokenDictionary.Instance;
        tokenImage.sprite = tokenDictionary.getSprites(token).ForegroundSprite;
        silouhetteImage.sprite = tokenDictionary.getSprites(token).SilouhetteSprite;
        float width = tokenImage.sprite.rect.width;
        float height = tokenImage.sprite.rect.height;
        RectTransform rt = (RectTransform)transform;
        rt.sizeDelta = new Vector2(width / 10f, height / 10f);
    }
}
