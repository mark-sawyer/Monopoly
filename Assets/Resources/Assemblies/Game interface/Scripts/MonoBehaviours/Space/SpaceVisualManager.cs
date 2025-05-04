using UnityEngine;

public class SpaceVisualManager : MonoBehaviour {
    public SpaceVisual getSpaceVisual(int index) {
        return transform.GetChild(index).GetComponent<SpaceVisual>();
    }
    public Vector3 getStartingPosition(int order) {
        int totalPlayers = GameState.game.getNumberOfPlayers();
        return getSpaceVisual(0).getPosition(totalPlayers, order);
    }
    public float getStartingScale() {
        return getSpaceVisual(0).getScale(GameState.game.getNumberOfPlayers());
    }
}
