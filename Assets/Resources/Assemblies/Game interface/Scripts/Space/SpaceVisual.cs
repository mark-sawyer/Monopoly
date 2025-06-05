using UnityEngine;

public class SpaceVisual : MonoBehaviour {
    [SerializeField] private ScriptableObject spaceInfoSO;
    [SerializeField] private TokenParameters tokenParameters;



    public SpaceInfo SpaceInfo => (SpaceInfo)spaceInfoSO;
    public TokenParameters TokenParameters => tokenParameters;
    public virtual float getScale(PlayerInfo playerInfo) {
        int playersOnSpace = SpaceInfo.NumberOfPlayersOnSpace;
        return tokenParameters.getScaleValue(playersOnSpace);
    }
    public virtual Vector3 getMajorPoint(PlayerInfo playerInfo) {
        Vector3 position = tokenParameters.getMajorPositionOffset();
        return transform.TransformPoint(position);
    }
    public virtual Vector3 getMinorPoint(PlayerInfo playerInfo) {
        SpaceInfo spaceInfo = playerInfo.SpaceInfo;
        int playersOnSpace = spaceInfo.NumberOfPlayersOnSpace;
        int order = spaceInfo.getPlayerOrderIndex(playerInfo);
        Vector3 position = tokenParameters.getTotalPositionOffset(playersOnSpace, order);
        return transform.TransformPoint(position);
    }
}
