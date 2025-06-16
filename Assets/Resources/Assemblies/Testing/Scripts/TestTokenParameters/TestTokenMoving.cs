using UnityEngine;

public class TestTokenMoving : MonoBehaviour {
    [SerializeField] private Transform targetSpace;
    [SerializeField] private TokenParameters tokenParameters;
    private Vector3 attractivePoint;
    private Vector3 velocity = new();
    int players;
    int order;
    #region consts
    private const float ACCELERATION_CONSTANT = 0.1f;
    private const float VELOCITY_CONSTANT = 0.5f;
    private const float MAX_VELOCITY = 100f;
    #endregion


    #region MonoBehaviour
    private void Start() {
        setSprites();
        players = transform.parent.childCount;
        order = getOrder();
        attractivePoint = (Vector2)targetSpace.position + tokenParameters.getTotalPositionOffset(players, order);
        GetComponent<TokenScaler>().beginScaleChange(tokenParameters.getScaleValue(players));
    }
    private void Update() {
        //moveToAttractivePoint();
        float scale = tokenParameters.getScaleValue(players);
        transform.localScale = new Vector3(scale, scale, scale);
        transform.position = (Vector2)targetSpace.position + tokenParameters.getTotalPositionOffset(players, order);
    }
    #endregion



    #region private
    private void moveToAttractivePoint() {
        Vector3 dirVec = attractivePoint - transform.position;
        Vector3 acceleration = (dirVec - VELOCITY_CONSTANT * velocity) * ACCELERATION_CONSTANT;
        velocity = velocity + acceleration;
        if (velocity.magnitude > MAX_VELOCITY) velocity = velocity.normalized * MAX_VELOCITY;
        transform.position = (transform.position + velocity * Time.deltaTime);
    }
    private int getOrder() {
        Transform parent = transform.parent;
        for (int i = 0; i < parent.childCount; i++) {
            if (parent.GetChild(i) == transform) return i;
        }
        throw new System.Exception();
    }
    private void setSprites() {
        int index = Random.Range(0, 8);
        TokenDictionary tokenDictionary = Resources.Load<TokenDictionary>(
            "ScriptableObjects/Tokens/token_dictionary"
        );
        Token token = (Token)index;
        TokenSprites tokenSprites = tokenDictionary.getSprites(token);
        SpriteRenderer front = GetComponent<SpriteRenderer>();
        SpriteRenderer back = transform.GetChild(0).GetComponent<SpriteRenderer>();
        front.sprite = tokenSprites.ForegroundSprite;
        back.sprite = tokenSprites.SilouhetteSprite;
    }
    #endregion
}
