using UnityEngine;

public class AuctionRowSetup : MonoBehaviour {
    [SerializeField] private AuctionPlayerSection[] auctionPlayerSections;

    public void setup(PlayerInfo[] players) {
        for (int i = 0; i < players.Length; i++) {
            auctionPlayerSections[i].setup(players[i]);
        }
    }
}
