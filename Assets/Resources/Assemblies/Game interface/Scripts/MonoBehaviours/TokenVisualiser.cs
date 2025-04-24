using UnityEngine;

public class TokenVisualiser : MonoBehaviour {
    [SerializeField] private GameObject tokenPrefab;

    public void instantiateTokens(Player[] players) {
        for (int i = 0; i < players.Length; i++) {
            Player player = players[i];
            Vector3 position = UIUtilities.spaceIndexToPosition(0);
            GameObject newToken = Instantiate(tokenPrefab, position, Quaternion.identity, transform);
            newToken.GetComponent<SpriteRenderer>().sprite = UIUtilities.tokenTypeToSprite(player.getToken());
            newToken.GetComponent<TokenVisual>().assignPlayer(player);
        }
    }
}
