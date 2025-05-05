using UnityEngine;
using System.Collections;

public class TokenVisual : MonoBehaviour {
    [SerializeField] private SpriteRenderer tokenSpriteRenderer;
    [SerializeField] private SpriteRenderer silouhetteSpriteRenderer;
    [SerializeField] private int boardLayer;
    [SerializeField] private int movingLayer;
    private SpaceVisualManager spaceVisualManager;
    public PlayerInfo player { get; private set; }
    private TokenMover tokenMover;



    #region MonoBehaviour
    private void Start() {
        tokenMover = new TokenMover(this, spaceVisualManager);
        setSprites();
        setSpriteLayerOrders();
        setStartingScale();
    }
    private void Update() {
        tokenMover.updatePosition();
    }
    #endregion



    #region public
    public void setup(PlayerInfo player, SpaceVisualManager spaceVisualManager) {
        this.player = player;
        this.spaceVisualManager = spaceVisualManager;
    }
    public void changeLayer(bool isMovingLayer) {
        if (isMovingLayer) gameObject.layer = movingLayer;
        else gameObject.layer = boardLayer;
    }
    public void beginScaleChange(float targetScale) {
        StartCoroutine(changeScale(targetScale));
    }
    public void beginScaleChange() {
        SpaceVisual spaceVisual = spaceVisualManager.getSpaceVisual(player.getSpaceIndex());
        StartCoroutine(changeScale(spaceVisual.getScale()));
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
        silouhetteSpriteRenderer.sprite = UIUtilities.tokenTypeToSpriteBackground(player.getToken());
        tokenSpriteRenderer.sprite = UIUtilities.tokenTypeToSpriteForeground(player.getToken());
    }
    private void setSpriteLayerOrders() {
        int turnOrder = GameState.game.getPlayerIndex(player);
        int players = GameState.game.getNumberOfPlayers();
        int foregroundOrder = 2 * (players - turnOrder);
        tokenSpriteRenderer.sortingOrder = foregroundOrder;
        silouhetteSpriteRenderer.sortingOrder = foregroundOrder - 1;
    }
    private void setStartingScale() {
        float scale = spaceVisualManager.getStartingScale();
        transform.localScale = new Vector3(scale, scale, scale);
    }
    private IEnumerator changeScale(float targetScale) {
        float startScale = transform.localScale.x;
        int frames = InterfaceConstants.FRAMES_FOR_TOKEN_GROWING;
        float slope = (targetScale - startScale) / frames;
        for (int i = 0; i < frames; i++) {
            float scale = startScale + slope * i;
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
    }
    #endregion
}
