using UnityEngine;

public class SpaceVisual : MonoBehaviour {
    [SerializeField] TokenParameters tokenParameters;

    public Vector3 transformPoint(Vector3 point) {
        return transform.TransformPoint(point);
    }
    public float getScale(int playersOnSpace) {
        return tokenParameters.getScaleValue(playersOnSpace);
    }
    public Vector3 getPosition(int playersOnSpace, int order) {
        Vector3 position = tokenParameters.getPositionOffset(playersOnSpace, order);
        return transform.TransformPoint(position);
    }
}
