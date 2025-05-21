using UnityEngine;

public class SpaceVisual : MonoBehaviour {
    public SpaceInfo SpaceInfo { get; private set; }
    private TokenVisualManager tokenVisualManager;
    [SerializeField] private TokenParameters tokenParameters;



    public void setup(SpaceInfo spaceInfo, TokenVisualManager tokenVisualManager) {
        SpaceInfo = spaceInfo;
        this.tokenVisualManager = tokenVisualManager;
    }
    public float getScale() {
        int playersOnSpace = SpaceInfo.NumberOfPlayersOnSpace;
        return tokenParameters.getScaleValue(playersOnSpace);
    }
    public Vector3 getFinalPosition(int playersOnSpace, int order) {
        Vector3 position = tokenParameters.getTotalPositionOffset(playersOnSpace, order);
        return transform.TransformPoint(position);
    }
    public Vector3 getCentralPosition() {
        Vector3 position = tokenParameters.getMajorPositionOffset();
        return transform.TransformPoint(position);
    }
}
