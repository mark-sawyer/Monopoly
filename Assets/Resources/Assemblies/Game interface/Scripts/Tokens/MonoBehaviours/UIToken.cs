using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class UIToken : Ghostable {
    [SerializeField] private Token token;
    [SerializeField] private Image tokenImage;
    [SerializeField] private Image silouhetteImage;



    private void Start() {
        setup();
    }


    public Token Token => token;
    public void setup() {
        TokenDictionary tokenDictionary = TokenDictionary.Instance;
        tokenImage.sprite = tokenDictionary.getSprites(token).ForegroundSprite;
        silouhetteImage.sprite = tokenDictionary.getSprites(token).SilouhetteSprite;
        float width = tokenImage.sprite.rect.width;
        float height = tokenImage.sprite.rect.height;
        RectTransform rt = (RectTransform)transform;
        rt.sizeDelta = new Vector2(width / 10f, height / 10f);
    }
    public override void ghostSetup() {
        token = transform.parent.GetComponent<UIToken>().Token;
        setup();
    }
}
