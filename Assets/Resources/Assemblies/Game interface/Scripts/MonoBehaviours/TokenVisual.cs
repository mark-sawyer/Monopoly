using UnityEngine;

public class TokenVisual : MonoBehaviour {
    private PlayerInfo player;

    private void Start() {
        GameEvents.visualUpdateTriggered.AddListener(updatePosition);
    }

    public void assignPlayer(PlayerInfo player) {
        this.player = player;
    }

    private void updatePosition() {
        int spaceIndex = player.getSpaceIndex();
        transform.position = UIUtilities.spaceIndexToPosition(spaceIndex);
    }
}
