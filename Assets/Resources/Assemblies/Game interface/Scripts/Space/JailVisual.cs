using UnityEngine;
using UnityEngine.UIElements;

public class JailVisual : SpaceVisual {
    [SerializeField] private TokenParameters jailParameters;

    public override float getScale(PlayerInfo playerInfo) {
        JailSpaceInfo jailSpaceInfo = (JailSpaceInfo)SpaceInfo;

        if (playerInfo.InJail) {
            int playersInJail = jailSpaceInfo.NumberOfPlayersInJail;
            return jailParameters.getScaleValue(playersInJail);
        }
        else {
            int playersVisiting = jailSpaceInfo.NumberOfVisitingPlayers;
            return TokenParameters.getScaleValue(playersVisiting);
        }
    }
    public override Vector3 getMajorPoint(PlayerInfo playerInfo) {
        if (playerInfo.InJail) {
            Vector3 position = jailParameters.getMajorPositionOffset();
            return transform.TransformPoint(position);
        }
        else {
            Vector3 position = TokenParameters.getMajorPositionOffset();
            return transform.TransformPoint(position);
        }
    }
    public override Vector3 getMinorPoint(PlayerInfo playerInfo) {
        JailSpaceInfo jailSpaceInfo = (JailSpaceInfo)SpaceInfo;

        if (playerInfo.InJail) {
            int playersInJail = jailSpaceInfo.NumberOfPlayersInJail;
            int jailOrder = jailSpaceInfo.getJailOrderIndex(playerInfo);
            Vector3 position = jailParameters.getTotalPositionOffset(playersInJail, jailOrder);
            return transform.TransformPoint(position);
        }
        else {
            int playersVisiting = jailSpaceInfo.NumberOfVisitingPlayers;
            int visitingOrder = jailSpaceInfo.getJailOrderIndex(playerInfo);
            Vector3 position = TokenParameters.getTotalPositionOffset(playersVisiting, visitingOrder);
            return transform.TransformPoint(position);
        }
    }
}
