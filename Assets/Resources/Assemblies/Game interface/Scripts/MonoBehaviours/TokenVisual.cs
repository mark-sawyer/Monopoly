using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class TokenVisual : MonoBehaviour {
    [SerializeField] private SpriteRenderer tokenSpriteRenderer;
    [SerializeField] private SpriteRenderer silouhetteSpriteRenderer;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private int boardLayer;
    [SerializeField] private int movingLayer;
    private UnityEvent madeItToTargetSpace = new UnityEvent();
    private SpaceVisual targetSpace;
    private PlayerInfo player;
    private bool isInSpace;



    #region MonoBehaviour
    private void Start() {
        setSprites();
        setSpriteLayerOrders();
        setScale();
    }
    private void Update() {
        if (!isInSpace) {
            Vector3 dir = (targetSpace.getTargetPosition() - transform.position).normalized;
            Vector3 velocityIncrease = InterfaceConstants.TOKEN_SPEED_INCREASE * dir * Time.fixedDeltaTime;
            rb2D.velocity = Vector3.ClampMagnitude((Vector3)rb2D.velocity + velocityIncrease, InterfaceConstants.TOKEN_MAX_SPEED);
        }
    }
    #endregion



    #region public
    public void setup(PlayerInfo player, SpaceVisual startingTargetSpace) {
        this.player = player;
        targetSpace = startingTargetSpace;
    }
    public void changeLayer(bool isMovingLayer) {
        if (isMovingLayer) gameObject.layer = movingLayer;
        else gameObject.layer = boardLayer;
    }
    public void startChangingScale(float targetScale) {
        StartCoroutine(changeScale(targetScale));
    }
    public void updateTargetSpace(SpaceVisual newTargetSpace) {
        targetSpace = newTargetSpace;
        isInSpace = false;
    }
    public void listenForMadeItToTargetSpace(UnityAction a) {
        madeItToTargetSpace.AddListener(a);
    }
    public void removeListenersForMadeItToTargetSpace() {
        madeItToTargetSpace.RemoveAllListeners();
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
    private void setScale() {
        int totalPlayers = GameState.game.getNumberOfPlayers();
        float scale = UIUtilities.scaleForTokens(totalPlayers);
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
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<SpaceVisual>() == targetSpace) {
            isInSpace = true;
            madeItToTargetSpace.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<SpaceVisual>() == targetSpace) {
            isInSpace = false;
        }
    }
    #endregion
}
