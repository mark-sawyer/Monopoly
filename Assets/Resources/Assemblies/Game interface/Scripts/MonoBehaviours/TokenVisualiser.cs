using UnityEngine;

public class TokenVisualiser : MonoBehaviour {
    [SerializeField] private GameObject tokenPrefab;

    public void instantiateTokens(PlayerVisualDataGetter[] playerVisuals) {
        for (int i = 0; i < playerVisuals.Length; i++) {
            Vector3 position = new Vector3(0f, i, 0f);
            GameObject newToken = Instantiate(tokenPrefab, position, Quaternion.identity, transform);
            newToken.GetComponent<Token>().assignPlayer(playerVisuals[i]);
        }
    }
}
