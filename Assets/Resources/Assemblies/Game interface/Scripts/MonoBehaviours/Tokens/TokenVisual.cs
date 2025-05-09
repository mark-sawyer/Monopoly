using UnityEngine;
using System.Collections;

public class TokenVisual : MonoBehaviour {
    [SerializeField] private SpriteRenderer tokenSpriteRenderer;
    [SerializeField] private SpriteRenderer silouhetteSpriteRenderer;
    [SerializeField] private TokenMover tokenMover;
    public PlayerInfo player { get; private set; }
    private SpaceVisualManager spaceVisualManager;
    private TokenSprites tokenSprites;



    #region MonoBehaviour
    private void Start() {
        setSprites();
        setSpriteLayerOrders();
    }
    #endregion



    #region public
    public void setup(PlayerInfo player, SpaceVisualManager spaceVisualManager, TokenSprites tokenSprites) {
        this.player = player;
        this.spaceVisualManager = spaceVisualManager;
        this.tokenSprites = tokenSprites;
    }
    public void changeLayer(string layerName) {
        tokenSpriteRenderer.sortingLayerName = layerName;
        silouhetteSpriteRenderer.sortingLayerName = layerName;
    }
    public void beginMovingToNewSpace() {
        DiceInfo diceInfo = GameState.game.getDiceInfo();
        int roll = diceInfo.getTotalValue();
        int newSpaceIndex = player.getSpaceIndex();
        int priorSpaceIndex = Modulus.exe(newSpaceIndex - roll, GameConstants.TOTAL_SPACES);
        tokenMover.startMoving(priorSpaceIndex, roll);
    }
    #endregion



    #region private
    private void setSprites() {
        silouhetteSpriteRenderer.sprite = tokenSprites.SilouhetteSprite;
        tokenSpriteRenderer.sprite = tokenSprites.ForegroundSprite;
    }
    private void setSpriteLayerOrders() {
        int turnOrder = GameState.game.getPlayerIndex(player);
        int players = GameState.game.getNumberOfPlayers();
        int foregroundOrder = 2 * (players - turnOrder);
        tokenSpriteRenderer.sortingOrder = foregroundOrder;
        silouhetteSpriteRenderer.sortingOrder = foregroundOrder - 1;
    }
    #endregion
}
