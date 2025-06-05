using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TokenMover : MonoBehaviour {
    [SerializeField] private TokenScaler tokenScaler;
    [SerializeField] private GameEvent tokenSettled;
    [SerializeField] private GameEvent<TokenMover, int> tokenOnSpaceChangedEvent;
    private Queue<Vector3> queue = new();
    private Vector3 attractivePoint;
    private Vector3 velocity = new();
    private bool settled;
    #region consts
    private const float ACCELERATION_CONSTANT = 0.1f;
    private const float VELOCITY_CONSTANT = 0.5f;
    private const float DISTANCE_TO_SPACE_THRESHOLD = 5f;
    private const float DISTANCE_FOR_SETTLING_THRESHOLD = 0.2f;
    private const float VELOCITY_FOR_SETTLING_THRESHOLD = 0.2f;
    private const float MAX_VELOCITY = 100f;
    #endregion



    #region MonoBehaviour
    private void Start() {
        tokenOnSpaceChangedEvent.Listeners += tokenOnSpaceChanged;
        attractivePoint = transform.position;
    }
    private void Update() {
        moveToAttractivePoint();
        if (queue.Count > 0 && directionVector().magnitude < DISTANCE_TO_SPACE_THRESHOLD) {
            attractivePoint = queue.Dequeue();
            if (queue.Count == 0) {
                tokenOnSpaceChangedEvent.invoke(this, PlayerInfo.SpaceIndex);
                SpaceVisual spaceVisual = SpaceVisualManager.Instance.getSpaceVisual(PlayerInfo.SpaceIndex);
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
    public PlayerInfo PlayerInfo { get; private set; }
    public void setup(PlayerInfo player) {
        PlayerInfo = player;
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
                SpaceVisual spaceVisual = SpaceVisualManager.Instance.getSpaceVisual(spaceIndex);
                Vector3 majorPoint = spaceVisual.getMajorPoint(PlayerInfo);
                majorPoints.Add(majorPoint);
            }
            return majorPoints;
        }



        List<int> spaceIndices = getSpaceIndices(startingSpaceIndex, roll);

        List<Vector3> majorPoints = getMajorPoints(spaceIndices);
        Vector3 minorPoint = getMinorPoint();

        foreach (Vector3 point in majorPoints) queue.Enqueue(point);
        queue.Enqueue(minorPoint);
        attractivePoint = queue.Dequeue();
        tokenOnSpaceChangedEvent.invoke(this, startingSpaceIndex);
        settled = false;
    }
    public void startMovingToJail(int startingSpaceIndex) {
        JailVisual jailVisual = SpaceVisualManager.Instance.JailVisual;
        Vector3 majorPoint = jailVisual.getMajorPoint(PlayerInfo);
        Vector3 minorPoint = jailVisual.getMinorPoint(PlayerInfo);
        queue.Enqueue(majorPoint);
        queue.Enqueue(minorPoint);
        attractivePoint = queue.Dequeue();
        tokenOnSpaceChangedEvent.invoke(this, startingSpaceIndex);
        settled = false;
    }
    #endregion



    #region private
    private Vector3 getMinorPoint() {
        SpaceVisual spaceVisual = SpaceVisualManager.Instance.getSpaceVisual(PlayerInfo.SpaceIndex);
        return spaceVisual.getMinorPoint(PlayerInfo);
    }
    private void moveToAttractivePoint() {
        float finalPositionMagnitude() {
            return (queue.Last() - transform.position).magnitude;
        }

        Vector3 dirVec = queue.Count > 0 ? directionVector().normalized * finalPositionMagnitude() : directionVector();
        Vector3 acceleration = (dirVec - VELOCITY_CONSTANT * velocity) * ACCELERATION_CONSTANT;
        velocity = velocity + acceleration;
        if (velocity.magnitude > MAX_VELOCITY) velocity = velocity.normalized * MAX_VELOCITY;
        transform.position = (transform.position + velocity * Time.deltaTime);
    }
    private void tokenOnSpaceChanged(TokenMover caller, int spaceIndex) {
        if (caller == this) return;
        if (spaceIndex != PlayerInfo.SpaceIndex) return;


        SpaceVisual spaceVisual = SpaceVisualManager.Instance.getSpaceVisual(spaceIndex);
        tokenScaler.beginScaleChange();
        attractivePoint = spaceVisual.getMinorPoint(PlayerInfo);
    }
    private Vector3 directionVector() {
        return attractivePoint - transform.position;
    }
    private bool passesSettledTest() {
        return !settled &&
            queue.Count == 0 &&
            velocity.magnitude < VELOCITY_FOR_SETTLING_THRESHOLD &&
            directionVector().magnitude < DISTANCE_FOR_SETTLING_THRESHOLD;
    }
    #endregion
}
