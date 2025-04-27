using UnityEngine;

public class PlayerPanels : MonoBehaviour {
    public void setupPanels(Player[] players) {
        destroyExtraPanels(players);
        associateWithPlayers(players);
    }

    private void destroyExtraPanels(Player[] players) {
        for (int i = players.Length; i < GameConstants.MAX_PLAYERS; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    private void associateWithPlayers(Player[] players) {
        for (int i = 0; i < players.Length; i++) {
            transform.GetChild(i).GetComponent<PlayerPanel>().setup(players[i]);
        }
    }
}
