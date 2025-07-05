using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenVisual : MonoBehaviour {
    private class TokenMover {
        #region Private attributes
        private TokenVisual tokenVisual;
        private Transform transform;
        private Queue<Vector3> queue = new();
        private Vector3 attractivePoint;
        private Vector3 velocity = new();
        private bool settled;
        private Vector3 goMajorPoint;
        private PlayerInfo playerInfo;
        #endregion
        #region Constants
        private const float ACCELERATION_CONSTANT = 0.1f;
        private const float VELOCITY_CONSTANT = 0.5f;
        private const float MAX_VELOCITY = 60f;
        private const float FRAME_TIME = 1f / 60f;
        private const float DISTANCE_TO_SPACE_THRESHOLD = 5f;
        private const float DISTANCE_FOR_SETTLING_THRESHOLD = 0.2f;
        private const float VELOCITY_FOR_SETTLING_THRESHOLD = 0.2f;
        #endregion



        #region public
        public TokenMover(TokenVisual tokenVisual) {
            this.tokenVisual = tokenVisual;
            transform = tokenVisual.transform;
            playerInfo = tokenVisual.playerInfo;
            goMajorPoint = getGoMajorPoint();
            UIEventHub.Instance.sub_LeaveJail(tokenOnJailSpaceChanged);
            attractivePoint = transform.position;
        }
        public void update() {
            moveToAttractivePoint();
            if (queue.Count > 0 && directionVector().magnitude < DISTANCE_TO_SPACE_THRESHOLD) {
                if (passingGo()) {
                    DataEventHub.Instance.call_MoneyAdjustment(tokenVisual.PlayerInfo, GameConstants.MONEY_FOR_PASSING_GO);
                }
                attractivePoint = queue.Dequeue();
                if (queue.Count == 0) {
                    SpaceVisualManager.Instance.getSpaceVisual(playerInfo.SpaceIndex).alertPresentTokensToMove();
                    int spaceIndex = playerInfo.SpaceIndex;
                    SpaceVisual spaceVisual = SpaceVisualManager.Instance.getSpaceVisual(spaceIndex);
                    float scale = spaceVisual.getScale(playerInfo);
                    tokenVisual.beginScaleChange(scale);
                }
            }
            else if (passesSettledTest()) {
                settled = true;
                tokenVisual.changeLayer(InterfaceConstants.BOARD_TOKEN_LAYER_NAME);
                UIEventHub.Instance.call_TokenSettled();
            }
        }
        public void startMoving(int startingSpaceIndex, int spacesMoved) {
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
                    Vector3 majorPoint = spaceVisual.getMajorPoint(playerInfo);
                    majorPoints.Add(majorPoint);
                }
                return majorPoints;
            }



            List<int> spaceIndices = getSpaceIndices(startingSpaceIndex, spacesMoved);

            List<Vector3> majorPoints = getMajorPoints(spaceIndices);
            Vector3 minorPoint = getMinorPoint();

            foreach (Vector3 point in majorPoints) queue.Enqueue(point);
            queue.Enqueue(minorPoint);
            attractivePoint = queue.Dequeue();
            SpaceVisualManager.Instance.getSpaceVisual(startingSpaceIndex).alertRemainingTokensToMove();
            settled = false;
        }
        public void startMovingDirectly(int startingSpaceIndex, int newSpaceIndex) {
            SpaceVisual spaceVisual = SpaceVisualManager.Instance.getSpaceVisual(newSpaceIndex);
            Vector3 majorPoint = spaceVisual.getMajorPoint(playerInfo);
            Vector3 minorPoint = spaceVisual.getMinorPoint(playerInfo);
            queue.Enqueue(majorPoint);
            queue.Enqueue(minorPoint);
            attractivePoint = queue.Dequeue();
            SpaceVisualManager.Instance.getSpaceVisual(startingSpaceIndex).alertRemainingTokensToMove();
            settled = false;
        }
        public void changeAttractivePoint(Vector3 newPoint) {
            attractivePoint = newPoint;
        }
        #endregion



        #region private
        private Vector3 getMinorPoint() {
            SpaceVisualManager spaceVisualManager = SpaceVisualManager.Instance;
            int spaceIndex = playerInfo.SpaceIndex;
            SpaceVisual spaceVisual = spaceVisualManager.getSpaceVisual(spaceIndex);
            return spaceVisual.getMinorPoint(playerInfo);
        }
        private void moveToAttractivePoint() {
            float finalPositionMagnitude() {
                Vector3 temp = transform.position;
                float distance = 0;
                foreach (Vector3 v in queue) {
                    distance += (temp - v).magnitude;
                    temp = v;
                }
                return distance;
            }

            Vector3 dirVec = queue.Count > 0
                ? directionVector().normalized * finalPositionMagnitude()
                : directionVector();
            Vector3 acceleration = (dirVec - VELOCITY_CONSTANT * velocity) * ACCELERATION_CONSTANT;
            velocity = velocity + acceleration;
            if (velocity.magnitude > MAX_VELOCITY) velocity = velocity.normalized * MAX_VELOCITY;
            transform.position = transform.position + velocity * FRAME_TIME;
        }
        private void tokenOnJailSpaceChanged() {
            if (playerInfo.SpaceIndex != GameConstants.JAIL_SPACE_INDEX) return;


            SpaceVisual spaceVisual = SpaceVisualManager.Instance.getSpaceVisual(GameConstants.JAIL_SPACE_INDEX);
            tokenVisual.beginScaleChange(spaceVisual.getScale(playerInfo));
            attractivePoint = spaceVisual.getMinorPoint(playerInfo);
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
        private Vector3 getGoMajorPoint() {
            return SpaceVisualManager.Instance.getSpaceVisual(0).getMajorPoint(GameState.game.TurnPlayer);
        }
        private bool passingGo() {
            return attractivePoint.x == goMajorPoint.x
                && attractivePoint.y == goMajorPoint.y;
        }
        #endregion
    }
    #region Editor references
    [SerializeField] private TokenDictionary tokenDictionary;
    [SerializeField] private SpriteRenderer tokenSpriteRenderer;
    [SerializeField] private SpriteRenderer silouhetteSpriteRenderer;
    #endregion
    #region Private attributes
    private TokenMover tokenMover;
    private PlayerInfo playerInfo;
    private TokenSprites tokenSprites;
    private TokenColours tokenColours;
    #endregion



    #region MonoBehaviour
    private void Start() {
        setSprites();
        setSpriteLayerOrders();
        tokenMover = new(this);
    }
    private void Update() {
        tokenMover.update();
    }
    #endregion



    #region public
    public PlayerInfo PlayerInfo => playerInfo;
    public SpaceVisual CurrentSpace => SpaceVisualManager.Instance.getSpaceVisual(playerInfo.SpaceIndex);
    public void setup(PlayerInfo playerInfo, float scale) {
        this.playerInfo = playerInfo;
        tokenSprites = tokenDictionary.getSprites(playerInfo.Token);
        tokenColours = tokenDictionary.getColours(playerInfo.Colour);
        transform.localScale = new Vector3(scale, scale, scale);
    }
    public void prepForMoving() {
        beginScaleChange(InterfaceConstants.SCALE_FOR_MOVING_TOKEN);
        changeLayer(InterfaceConstants.MOVING_TOKEN_LAYER_NAME);
    }
    public void moveTokenAlongBoard(int startingIndex, int spacesMoved) {
        changeLayer(InterfaceConstants.MOVING_TOKEN_LAYER_NAME);
        float currentScale = transform.localScale.x;
        if (currentScale < InterfaceConstants.SCALE_FOR_MOVING_TOKEN) {
            beginScaleChange(InterfaceConstants.SCALE_FOR_MOVING_TOKEN);
            WaitFrames.Instance.exe(
                InterfaceConstants.FRAMES_FOR_TOKEN_GROWING,
                () => tokenMover.startMoving(startingIndex, spacesMoved)
            );
        }
        else tokenMover.startMoving(startingIndex, spacesMoved);
    }
    public void moveTokenDirectlyToSpace(int startingIndex, int newIndex) {
        changeLayer(InterfaceConstants.MOVING_TOKEN_LAYER_NAME);
        float currentScale = transform.localScale.x;
        if (currentScale < InterfaceConstants.SCALE_FOR_MOVING_TOKEN) {
            beginScaleChange(InterfaceConstants.SCALE_FOR_MOVING_TOKEN);
            WaitFrames.Instance.exe(
                InterfaceConstants.FRAMES_FOR_TOKEN_GROWING,
                () => tokenMover.startMovingDirectly(startingIndex, newIndex)
            );
        }
        else tokenMover.startMovingDirectly(startingIndex, newIndex); ;
    }
    public void tokenOnSpaceChanged() {
        SpaceVisual currentSpace = CurrentSpace;
        beginScaleChange(currentSpace.getScale(PlayerInfo));
        tokenMover.changeAttractivePoint(currentSpace.getMinorPoint(playerInfo));
    }
    #endregion



    #region private
    private void beginScaleChange(float targetScale) {
        IEnumerator changeScale(float targetScale) {
            float startScale = transform.localScale.x;
            int frames = InterfaceConstants.FRAMES_FOR_TOKEN_GROWING;
            for (int i = 1; i <= frames; i++) {
                float scale = LinearValue.exe(i, startScale, targetScale, frames);
                transform.localScale = new Vector3(scale, scale, scale);
                yield return null;
            }
            transform.localScale = new Vector3(targetScale, targetScale, targetScale);
        }

        StartCoroutine(changeScale(targetScale));
    }
    private void setSprites() {
        silouhetteSpriteRenderer.sprite = tokenSprites.SilouhetteSprite;
        silouhetteSpriteRenderer.color = tokenColours.OutlineColour;
        tokenSpriteRenderer.sprite = tokenSprites.ForegroundSprite;
        tokenSpriteRenderer.color = tokenColours.TokenColour;
    }
    private void setSpriteLayerOrders() {
        int turnOrder = GameState.game.getPlayerIndex(PlayerInfo);
        int players = GameState.game.NumberOfPlayers;
        int foregroundOrder = 2 * (players - turnOrder);
        tokenSpriteRenderer.sortingOrder = foregroundOrder;
        silouhetteSpriteRenderer.sortingOrder = foregroundOrder - 1;
    }
    private void changeLayer(string layerName) {
        tokenSpriteRenderer.sortingLayerName = layerName;
        silouhetteSpriteRenderer.sortingLayerName = layerName;
    }
    #endregion
}
