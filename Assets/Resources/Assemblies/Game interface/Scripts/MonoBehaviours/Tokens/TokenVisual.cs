using UnityEngine;
using System.Collections;

public class TokenVisual : MonoBehaviour {
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
    public void setup(PlayerInfo player, SpaceVisualManager spaceVisualManager, TokenSprites tokenSprites, TokenColours tokenColours) {
        this.player = player;
        this.spaceVisualManager = spaceVisualManager;
        this.tokenSprites = tokenSprites;
        this.tokenColours = tokenColours;
    }
    public void changeLayer(string layerName) {
        tokenSpriteRenderer.sortingLayerName = layerName;
        silouhetteSpriteRenderer.sortingLayerName = layerName;
    }
    public void beginMovingToNewSpace() {
        DiceInfo diceInfo = GameState.game.DiceInfo;
        int roll = diceInfo.getTotalValue();
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
