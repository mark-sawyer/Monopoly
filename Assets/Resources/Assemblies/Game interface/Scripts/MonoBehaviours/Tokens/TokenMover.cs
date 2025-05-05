using System.Collections.Generic;
using UnityEngine;

public class TokenMover : MonoBehaviour {
    [SerializeField] private TokenScaler tokenScaler;
    public PlayerInfo player { get; private set; }
    private SpaceVisualManager spaceVisualManager;
    private List<Vector3> queue = new();
    private Vector3 attractivePoint;
    private Vector3 velocity = new();
    private bool settled;



    #region MonoBehaviour
    private void Start() {
        GameEvents.tokenOnSpaceChanged.AddListener(tokenOnSpaceChanged);
        attractivePoint = transform.position;
    }
    private void Update() {
        moveToAttractivePoint();
        if (queue.Count > 0 && directionVector().magnitude < InterfaceConstants.DISTANCE_TO_SPACE_THRESHOLD) {
            attractivePoint = queue[0];
            queue.RemoveAt(0);
            if (queue.Count == 0) {
                GameEvents.tokenOnSpaceChanged.Invoke(this, getSpaceIndex());
                SpaceVisual spaceVisual = spaceVisualManager.getSpaceVisual(getSpaceIndex());
                tokenScaler.beginScaleChange();
            }
        }
        else if (passesSettledTest()) {
            settled = true;
            GameEvents.tokenSettled.Invoke();
        }
    }
    #endregion



    #region public
    public void setup(PlayerInfo player, SpaceVisualManager spaceVisualManager) {
        this.player = player;
        this.spaceVisualManager = spaceVisualManager;
    }
    public void startMoving(int startingSpaceIndex, int roll) {
        List<int> spaceIndices = getSpaceIndices(startingSpaceIndex, roll);

        List<Vector3> majorPoints = getMajorPoints(spaceIndices);
        foreach (Vector3 point in majorPoints) queue.Add(point);

        Vector3 minorPoint = getMinorPoint(spaceIndices[spaceIndices.Count - 1], 0);
        queue.Add(minorPoint);

        attractivePoint = queue[0];
        queue.RemoveAt(0);

        GameEvents.tokenOnSpaceChanged.Invoke(this, startingSpaceIndex);
        settled = false;
    }
    #endregion



    #region private
    private List<int> getSpaceIndices(int currentIndex, int remaining) {
        List<int> indices = new List<int>();
        void updateValues(int added) {
            int nextIndex = (currentIndex + added) % GameConstants.TOTAL_SPACES;
            indices.Add(nextIndex);
            currentIndex = nextIndex;
            remaining -= added;
        }

        int toCorner = GameConstants.SPACES_ON_EDGE - (currentIndex % GameConstants.SPACES_ON_EDGE);
        if (toCorner <= remaining) updateValues(toCorner);
        while (remaining >= GameConstants.SPACES_ON_EDGE) updateValues(GameConstants.SPACES_ON_EDGE);
        if (remaining > 0) updateValues(remaining);
        return indices;
    }
    private List<Vector3> getMajorPoints(List<int> spaceIndices) {
        List<Vector3> majorPoints = new();
        for (int i = 0; i < spaceIndices.Count; i++) {
            int spaceIndex = spaceIndices[i];
            Vector3 majorPoint = spaceVisualManager.getSpaceVisual(spaceIndex).getCentralPosition();
            majorPoints.Add(majorPoint);
        }
        return majorPoints;
    }
    private Vector3 getMinorPoint(int spaceIndex, int order) {
        SpaceVisual spaceVisual = spaceVisualManager.getSpaceVisual(spaceIndex);
        SpaceInfo spaceInfo = spaceVisual.spaceInfo;
        int playersOnSpace = spaceInfo.getNumberOfPlayersOnSpace();
        return spaceVisual.getFinalPosition(playersOnSpace, order);
    }
    private void moveToAttractivePoint() {
        float velocityConstant = InterfaceConstants.TOKEN_VELOCITY_CONSTANT;
        float accelerationConstant = InterfaceConstants.TOKEN_ACCELERATION_CONSTANT;
        Vector3 acceleration = (directionVector() - velocityConstant * velocity) * accelerationConstant;
        velocity = velocity + acceleration;
        transform.position = (transform.position + velocity * Time.deltaTime);
    }
    private void tokenOnSpaceChanged(TokenMover caller, int spaceIndex) {
        if (caller == this) return;
        if (spaceIndex != getSpaceIndex()) return;

        tokenScaler.beginScaleChange();
        attractivePoint = getMinorPoint(spaceIndex, getSpaceOrderIndex());
    }
    private Vector3 directionVector() {
        return attractivePoint - transform.position;
    }
    private int getSpaceIndex() {
        return player.getSpaceIndex();
    }
    private int getSpaceOrderIndex() {
        SpaceVisual spaceVisual = spaceVisualManager.getSpaceVisual(getSpaceIndex());
        SpaceInfo spaceInfo = spaceVisual.spaceInfo;
        int playerOnSpaceIndex = spaceInfo.getPlayerOrderIndex(player);
        int totalPlayersOnSpace = spaceInfo.getNumberOfPlayersOnSpace();
        return totalPlayersOnSpace - playerOnSpaceIndex - 1;
    }
    private bool passesSettledTest() {
        return queue.Count == 0 &&
            !settled &&
            velocity.magnitude < InterfaceConstants.VELOCITY_FOR_SETTLING_THRESHOLD &&
            directionVector().magnitude < InterfaceConstants.DISTANCE_FOR_SETTLING_THRESHOLD;
    }
    #endregion
}
