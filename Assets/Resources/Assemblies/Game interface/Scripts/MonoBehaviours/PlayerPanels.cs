using UnityEngine;

public class PlayerPanels : MonoBehaviour {
    private void Start() {
        PlayerInfo[] players = GameState.game.getPlayers();
        destroyExtraPanels(players);
        associateWithPlayers(players);
    }
    private void destroyExtraPanels(PlayerInfo[] players) {
        for (int i = players.Length; i < GameConstants.MAX_PLAYERS; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    private void associateWithPlayers(PlayerInfo[] players) {
        for (int i = 0; i < players.Length; i++) {
            transform.GetChild(i).GetComponent<PlayerPanel>().setup(players[i]);
        }
    }
}
