using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceVisual : MonoBehaviour {
    public SpaceInfo spaceInfo { get; private set; }
    [SerializeField] private TokenParameters tokenParameters;
    private TokenVisualManager tokenVisualManager;



    public void setup(SpaceInfo spaceInfo, TokenVisualManager tokenVisualManager) {
        this.spaceInfo = spaceInfo;
        this.tokenVisualManager = tokenVisualManager;
    }
    public float getScale() {
        int playersOnSpace = spaceInfo.NumberOfPlayersOnSpace;
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
    public IEnumerable<TokenVisual> getTokenVisualsOnSpace() {
        IEnumerable<PlayerInfo> playerInfos = spaceInfo.VisitingPlayers;
        IEnumerable<int> indices = playerInfos.Select(x => GameState.game.getPlayerIndex(x));
        foreach (int index in indices) {
            Debug.Log(index);
        }
        return indices.Select(x => tokenVisualManager.getTokenVisual(x));
    }
}
