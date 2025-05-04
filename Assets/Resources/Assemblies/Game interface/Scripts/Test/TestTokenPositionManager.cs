using UnityEngine;

public class TestTokenPositionManager : MonoBehaviour {
    [SerializeField] BaseTokenOffsets baseTokenOffsets;
    [SerializeField] TokenScales tokenScales;
    [SerializeField] GameObject tokenPrefab;
    [SerializeField] int tokens;

    private void Start() {
        instantiateChildren();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            destroyChildren();
            instantiateChildren();
        }
    }

    private void destroyChildren() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    private void instantiateChildren() {
        for (int i = 0; i < tokens; i++) {
            GameObject newToken = Instantiate(tokenPrefab, transform.position, Quaternion.identity, transform);
            newToken.GetComponent<SpriteRenderer>().sprite = UIUtilities.tokenTypeToSpriteForeground((Token)i);
            newToken.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = UIUtilities.tokenTypeToSpriteBackground((Token)i);
            newToken.GetComponent<TestTokenPosition>().setup(baseTokenOffsets, tokenScales, tokens, i);
            newToken.GetComponent<TestTokenPosition>().adjustScaleAndSize();
        }
    }
}
