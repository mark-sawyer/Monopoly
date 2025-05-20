using UnityEngine;

public class TokenScales : ScriptableObject {
    [SerializeField] private float[] scaleValues;

    public float getScaleValue(int playersOnSpace) {
        return scaleValues[playersOnSpace - 1];
    }
}
