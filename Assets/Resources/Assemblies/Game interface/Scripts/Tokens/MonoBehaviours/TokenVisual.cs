using UnityEngine;
using System.Collections;

public class TokenVisual : MonoBehaviour {
    [SerializeField] private TokenDictionary tokenDictionary;
    [SerializeField] private SpriteRenderer tokenSpriteRenderer;
    [SerializeField] private SpriteRenderer silouhetteSpriteRenderer;
    [SerializeField] private TokenMover tokenMover;
    public PlayerInfo player { get; private set; }
    private SpaceVisualManager spaceVisualManager;
    private TokenSprites tokenSprites;
    private TokenColours tokenColours;



    #region MonoBehaviour
    private void Start() {
        setSprites();
        setSpriteLayerOrders();
    }
    #endregion



    #region public
    public void setup(PlayerInfo player, SpaceVisualManager spaceVisualManager) {
        this.player = player;
        this.spaceVisualManager = spaceVisualManager;
        tokenSprites = tokenDictionary.getSprites(player.Token);
        tokenColours = tokenDictionary.getColours(player.Colour);
    }
    public void changeLayer(string layerName) {
        tokenSpriteRenderer.sortingLayerName = layerName;
        silouhetteSpriteRenderer.sortingLayerName = layerName;
    }
    public void beginMovingToNewSpace() {
        DiceInfo diceInfo = GameState.game.DiceInfo;
        int roll = diceInfo.TotalValue;
        int newSpaceIndex = player.SpaceIndex;
        int priorSpaceIndex = Modulus.exe(newSpaceIndex - roll, GameConstants.TOTAL_SPACES);
        tokenMover.startMoving(priorSpaceIndex, roll);
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
        int turnOrder = GameState.game.getPlayerIndex(player);
        int players = GameState.game.NumberOfPlayers;
        int foregroundOrder = 2 * (players - turnOrder);
        tokenSpriteRenderer.sortingOrder = foregroundOrder;
        silouhetteSpriteRenderer.sortingOrder = foregroundOrder - 1;
    }
    #endregion
}
