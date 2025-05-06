using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPanels : MonoBehaviour {
    private void Start() {
        IEnumerable<PlayerInfo> players = GameState.game.getPlayers();
        destroyExtraPanels(players);
        associateWithPlayers(players);
    }
    private void destroyExtraPanels(IEnumerable<PlayerInfo> players) {
        for (int i = players.Count(); i < GameConstants.MAX_PLAYERS; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    private void associateWithPlayers(IEnumerable<PlayerInfo> players) {
        int i = 0;
        foreach (PlayerInfo player in players) {
            transform.GetChild(i).GetComponent<PlayerPanel>().setup(player);
            i += 1;
        }
    }
}
