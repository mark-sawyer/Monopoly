using UnityEngine;

public class TokenVisualiser : MonoBehaviour {
    [SerializeField] private GameObject tokenPrefab;

    private void Start() {
        PlayerInfo[] players = GameState.game.getPlayers();
        for (int i = 0; i < players.Length; i++) {
            PlayerInfo player = players[i];
            Vector3 position = UIUtilities.spaceIndexToPosition(0);
            GameObject newToken = Instantiate(tokenPrefab, position, Quaternion.identity, transform);
            newToken.GetComponent<SpriteRenderer>().sprite = UIUtilities.tokenTypeToSprite(player.getToken());
            newToken.GetComponent<TokenVisual>().assignPlayer(player);
        }
    }
}
