using UnityEngine;

[CreateAssetMenu(fileName = "new TokenScales", menuName = "Token/TokenScales")]
public class TokenScales : ScriptableObject {
    [SerializeField] private float[] scaleValues;

    public float getScaleValue(int playersOnSpace) {
        return scaleValues[playersOnSpace - 1];
    }
}
