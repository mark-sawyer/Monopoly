using UnityEngine;

[CreateAssetMenu(fileName = "New TokenParameters", menuName = "TokenParameters")]
public class TokenParameters : ScriptableObject {
    [SerializeField] private BaseTokenOffsets tokenOffsets;
    [SerializeField] private TokenScales tokenScales;



    public float getScaleValue(int playersOnSpace) {
        return tokenScales.getScaleValue(playersOnSpace);
    }
    public Vector2 getPositionOffset(int playersOnSpace, int order) {
        return tokenOffsets.getTotalOffset(playersOnSpace, order);
    }
}
