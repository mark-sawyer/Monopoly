using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceVisual : MonoBehaviour {
    [SerializeField] private ScriptableObject spaceInfoSO;
    [SerializeField] private TokenParameters tokenParameters;



    public SpaceInfo SpaceInfo => (SpaceInfo)spaceInfoSO;
    public TokenParameters TokenParameters => tokenParameters;
    public void alertRemainingTokensToMove() {
        IEnumerable<PlayerInfo> playerInfos = SpaceInfo.VisitingPlayers;
        IEnumerable<int> indices = playerInfos.Select(pi => pi.Index);
        IEnumerable<TokenVisual> tokenVisuals = indices.Select(x => TokenVisualManager.Instance.getTokenVisual(x));
        foreach (TokenVisual tv in tokenVisuals) {
            tv.tokenOnSpaceChanged();
        }
    }
    public void alertPresentTokensToMove() {
        IEnumerable<PlayerInfo> playerInfos = SpaceInfo.VisitingPlayers;
        IEnumerable<int> indices = playerInfos.Select(pi => pi.Index);
        List<TokenVisual> tokenVisuals = indices.Select(x => TokenVisualManager.Instance.getTokenVisual(x)).ToList();
        for (int i = 0; i < tokenVisuals.Count - 1; i++) {
            tokenVisuals[i].tokenOnSpaceChanged();
        }
    }
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
