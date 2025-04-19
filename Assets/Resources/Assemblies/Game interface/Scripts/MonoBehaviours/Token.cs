using UnityEngine;

public class Token : MonoBehaviour {
    PlayerVisualDataGetter playerVisual;

    private void Start() {
        GameEvents.visualUpdateTriggered.AddListener(updatePosition);
    }
    public void assignPlayer(PlayerVisualDataGetter player) {
        this.playerVisual = player;
    }

    private void updatePosition() {
        Vector3 currentPos = transform.position;
        float newXPos = playerVisual.getPlayerPosition();
        transform.position = new Vector3(
            newXPos,
            currentPos.y,
            currentPos.z
        );
    }
}
