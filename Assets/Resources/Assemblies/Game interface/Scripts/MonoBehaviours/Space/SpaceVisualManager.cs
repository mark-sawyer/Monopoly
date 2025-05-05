using UnityEngine;

public class SpaceVisualManager : MonoBehaviour {
    [SerializeField] TokenVisualManager tokenVisualManager;

    #region MonoBehaviour
    private void Start() {
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            SpaceVisual spaceVisual = child.GetComponent<SpaceVisual>();
            spaceVisual.setup(GameState.game.getSpaceInfo(i), tokenVisualManager);
        }
    }
    #endregion



    #region public
    public SpaceVisual getSpaceVisual(int index) {
        return transform.GetChild(index).GetComponent<SpaceVisual>();
    }
    public Vector3 getStartingPosition(int order) {
        int totalPlayers = GameState.game.getNumberOfPlayers();
        return getSpaceVisual(0).getFinalPosition(totalPlayers, order);
    }
    public float getStartingScale() {
        return getSpaceVisual(0).getScale();
    }
    #endregion
}
