using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TokenMover : MonoBehaviour {
    [SerializeField] private TokenScaler tokenScaler;
    [SerializeField] private GameEvent tokenSettled;
    [SerializeField] private GameEvent<TokenMover, int> tokenOnSpaceChangedEvent;
    public PlayerInfo player { get; private set; }
    private SpaceVisualManager spaceVisualManager;
    private List<Vector3> queue = new();
    private Vector3 attractivePoint;
    private Vector3 velocity = new();
    private bool settled;
    #region consts
    private const float ACCELERATION_CONSTANT = 0.1f;
    private const float VELOCITY_CONSTANT = 0.5f;
    private const float DISTANCE_TO_SPACE_THRESHOLD = 5f;
    private const float DISTANCE_FOR_SETTLING_THRESHOLD = 0.2f;
    private const float VELOCITY_FOR_SETTLING_THRESHOLD = 0.2f;
    #endregion



    #region MonoBehaviour
    private void Start() {
        tokenOnSpaceChangedEvent.Listeners += tokenOnSpaceChanged;
        attractivePoint = transform.position;
    }
    private void Update() {
        moveToAttractivePoint();
        if (queue.Count > 0 && directionVector().magnitude < DISTANCE_TO_SPACE_THRESHOLD) {
            attractivePoint = queue[0];
            queue.RemoveAt(0);
            if (queue.Count == 0) {
                tokenOnSpaceChangedEvent.invoke(this, getSpaceIndex());
                SpaceVisual spaceVisual = spaceVisualManager.getSpaceVisual(getSpaceIndex());
                tokenScaler.beginScaleChange();
            }
        }
        else if (passesSettledTest()) {
            settled = true;
            tokenSettled.invoke();
        }
    }
    #endregion



    #region public
    public void setup(PlayerInfo player, SpaceVisualManager spaceVisualManager) {
        this.player = player;
        this.spaceVisualManager = spaceVisualManager;
    }
    public void startMoving(int startingSpaceIndex, int roll) {
        List<int> getSpaceIndices(int currentIndex, int remaining) {
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
        List<Vector3> getMajorPoints(List<int> spaceIndices) {
            List<Vector3> majorPoints = new();
            for (int i = 0; i < spaceIndices.Count; i++) {
                int spaceIndex = spaceIndices[i];
                Vector3 majorPoint = spaceVisualManager.getSpaceVisual(spaceIndex).getCentralPosition();
                majorPoints.Add(majorPoint);
            }
            return majorPoints;
        }



        List<int> spaceIndices = getSpaceIndices(startingSpaceIndex, roll);

        List<Vector3> majorPoints = getMajorPoints(spaceIndices);
        foreach (Vector3 point in majorPoints) queue.Add(point);

        Vector3 minorPoint = getMinorPoint(spaceIndices[spaceIndices.Count - 1], 0);
        queue.Add(minorPoint);

        attractivePoint = queue[0];
        queue.RemoveAt(0);

        tokenOnSpaceChangedEvent.invoke(this, startingSpaceIndex);
        settled = false;
    }
    #endregion



    #region private
    private Vector3 getMinorPoint(int spaceIndex, int order) {
        SpaceVisual spaceVisual = spaceVisualManager.getSpaceVisual(spaceIndex);
        SpaceInfo spaceInfo = spaceVisual.SpaceInfo;
        int playersOnSpace = spaceInfo.NumberOfPlayersOnSpace;
        return spaceVisual.getFinalPosition(playersOnSpace, order);
    }
    private void moveToAttractivePoint() {
        float finalPositionMagnitude() {
            return (queue.Last() - transform.position).magnitude;
        }


        Vector3 dirVec = queue.Count > 0 ? directionVector().normalized * finalPositionMagnitude() : directionVector();
        Vector3 acceleration = (dirVec - VELOCITY_CONSTANT * velocity) * ACCELERATION_CONSTANT;
        velocity = velocity + acceleration;
        transform.position = (transform.position + velocity * Time.deltaTime);
    }
    private void tokenOnSpaceChanged(TokenMover caller, int spaceIndex) {
        if (caller == this) return;
        if (spaceIndex != getSpaceIndex()) return;


        int getSpaceOrderIndex() {
            SpaceVisual spaceVisual = spaceVisualManager.getSpaceVisual(getSpaceIndex());
            SpaceInfo spaceInfo = spaceVisual.SpaceInfo;
            int playerOnSpaceIndex = spaceInfo.getPlayerOrderIndex(player);
            int totalPlayersOnSpace = spaceInfo.NumberOfPlayersOnSpace;
            return totalPlayersOnSpace - playerOnSpaceIndex - 1;
        }

        tokenScaler.beginScaleChange();
        attractivePoint = getMinorPoint(spaceIndex, getSpaceOrderIndex());
    }
    private Vector3 directionVector() {
        return attractivePoint - transform.position;
    }
    private int getSpaceIndex() {
        return player.SpaceIndex;
    }
    private bool passesSettledTest() {
        return !settled &&
            queue.Count == 0 &&
            velocity.magnitude < VELOCITY_FOR_SETTLING_THRESHOLD &&
            directionVector().magnitude < DISTANCE_FOR_SETTLING_THRESHOLD;
    }
    #endregion
}
