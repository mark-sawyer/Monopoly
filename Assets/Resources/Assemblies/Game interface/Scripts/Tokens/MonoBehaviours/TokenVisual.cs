using UnityEngine;

public class TokenVisual : MonoBehaviour {
    [SerializeField] private TokenDictionary tokenDictionary;
    [SerializeField] private SpriteRenderer tokenSpriteRenderer;
    [SerializeField] private SpriteRenderer silouhetteSpriteRenderer;
    [SerializeField] private TokenMover tokenMover;
    public PlayerInfo PlayerInfo { get; private set; }
    private TokenSprites tokenSprites;
    private TokenColours tokenColours;



    #region MonoBehaviour
    private void Start() {
        setSprites();
        setSpriteLayerOrders();
    }
    #endregion



    #region public
    public void setup(PlayerInfo player) {
        this.PlayerInfo = player;
        tokenSprites = tokenDictionary.getSprites(player.Token);
        tokenColours = tokenDictionary.getColours(player.Colour);
    }
    public void changeLayer(string layerName) {
        tokenSpriteRenderer.sortingLayerName = layerName;
        silouhetteSpriteRenderer.sortingLayerName = layerName;
    }
    #endregion



    #region private
    private void setSprites() {
        silouhetteSpriteRenderer.sprite = tokenSprites.SilouhetteSprite;
        silouhetteSpriteRenderer.color = tokenColours.OutlineColour;
        tokenSpriteRenderer.sprite = tokenSprites.ForegroundSprite;
        tokenSpriteRenderer.color = tokenColours.TokenColour;
    }
    private void setSpriteLayerOrders() {
        int turnOrder = GameState.game.getPlayerIndex(PlayerInfo);
        int players = GameState.game.NumberOfPlayers;
        int foregroundOrder = 2 * (players - turnOrder);
        tokenSpriteRenderer.sortingOrder = foregroundOrder;
        silouhetteSpriteRenderer.sortingOrder = foregroundOrder - 1;
    }
    #endregion
}
