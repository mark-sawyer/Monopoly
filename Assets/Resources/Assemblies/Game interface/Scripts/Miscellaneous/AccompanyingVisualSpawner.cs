using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class AccompanyingVisualSpawner : MonoBehaviour {
    [SerializeField] private RectTransform rt;
    [SerializeField] private GameObject estateDeedPrefab;
    [SerializeField] private GameObject railroadDeedPrefab;
    [SerializeField] private GameObject utilityDeedPrefab;
    [SerializeField] private GameObject auctionHousePrefab;
    [SerializeField] private GameObject auctionHotelPrefab;
    private GameObject spawnedObjectInstance;
    private const int DEFAULT_X_POSITION = 100;



    #region Singleton boilerplate
    public static AccompanyingVisualSpawner Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion



    #region public
    public bool VisualExists => spawnedObjectInstance != null;
    public void spawnAndMove(RectTransform middleRT, PropertyInfo propertyInfo) {
        spawnedObjectInstance = spawnCorrectPrefab(propertyInfo);
        spawnAndMove(middleRT);
    }
    public void spawnAndMove(RectTransform middleRT, BuildingType buildingType) {
        spawnedObjectInstance = spawnCorrectPrefab(buildingType);
        spawnAndMove(middleRT);
    }
    public void removeObjectAndResetPosition() {
        Destroy(spawnedObjectInstance);
        spawnedObjectInstance = null;
        rt.anchoredPosition = new Vector2(DEFAULT_X_POSITION, 0f);
    }
    public float xPosOfVisual(RectTransform withRespectToRT) {
        RectTransform spawnedRT = (RectTransform)spawnedObjectInstance.transform;
        Vector3 worldPos = spawnedRT.parent.TransformPoint(spawnedRT.anchoredPosition);
        Vector3 newParentPos = withRespectToRT.InverseTransformPoint(worldPos);
        return newParentPos.x;
    }
    #endregion



    #region private
    private void spawnAndMove(RectTransform middleRT) {
        RectTransform spawnedRT = (RectTransform)spawnedObjectInstance.transform;
        adjustSpawnedPivotAndPosition(spawnedRT);
        scaleSpawnedObject(middleRT, spawnedRT);
        float goalX = getGoalX(middleRT, spawnedRT);
        StartCoroutine(moveObject(spawnedRT, goalX));
    }
    private GameObject spawnCorrectPrefab(PropertyInfo propertyInfo) {
        GameObject spawnedGameObject;
        if (propertyInfo is EstateInfo) spawnedGameObject = Instantiate(estateDeedPrefab, rt);
        else if (propertyInfo is RailroadInfo) spawnedGameObject = Instantiate(railroadDeedPrefab, rt);
        else spawnedGameObject = Instantiate(utilityDeedPrefab, rt);
        PropertyDeed propertyDeed = spawnedGameObject.GetComponent<PropertyDeed>();
        propertyDeed.setupCard(propertyInfo);        
        return spawnedGameObject;
    }
    private GameObject spawnCorrectPrefab(BuildingType buildingType) {
        GameObject prefab = buildingType == BuildingType.HOUSE
            ? auctionHousePrefab
            : auctionHotelPrefab;
        GameObject instance = Instantiate(prefab, rt);
        return instance;
    }
    private void adjustSpawnedPivotAndPosition(RectTransform spawnedRT) {
        spawnedRT.pivot = new Vector2(0f, 0.5f);
        spawnedRT.anchoredPosition = Vector2.zero;
    }
    private void scaleSpawnedObject(RectTransform middleRT, RectTransform spawnedRT) {
        RectTransform canvasRT = (RectTransform)rt.parent;
        float canvasHeight = canvasRT.rect.height;
        float canvasWidth = canvasRT.rect.width;
        float spawnedHeight = spawnedRT.rect.height;
        float spawnedWidth = spawnedRT.rect.width;
        float middleWidth = middleRT.rect.width;
        float spaceOnOneSide = (canvasWidth - middleWidth) / 2f;
        float scaleForHeightFit = 0.5f * canvasHeight / spawnedHeight;
        float scaleForWidthFit = 0.6f * spaceOnOneSide / spawnedWidth;
        float scale = scaleForHeightFit < scaleForWidthFit ? scaleForHeightFit : scaleForWidthFit;
        spawnedRT.localScale = new Vector3(scale, scale, scale);
    }
    private float getGoalX(RectTransform middleRT, RectTransform spawnedRT) {
        RectTransform canvasRT = (RectTransform)rt.parent;
        float canvasWidth = canvasRT.rect.width;
        float middleWidth = middleRT.rect.width * middleRT.localScale.x;
        float spaceOnOneSide = (canvasWidth - middleWidth) / 2f;
        float spawnedScale = spawnedRT.localScale.x;
        float spawnedWidth = spawnedRT.rect.width * spawnedScale;
        float gapBetweenMiddleAndObject = (spaceOnOneSide - spawnedWidth) / 2f;
        return -DEFAULT_X_POSITION - spawnedWidth - gapBetweenMiddleAndObject;
    }
    private IEnumerator moveObject(RectTransform spawnedRT, float goalX) {
        Func<float, float> getX = LinearValue.getFunc(DEFAULT_X_POSITION, goalX, FrameConstants.SCREEN_COVER_TRANSITION);
        for (int i = 0; i < FrameConstants.SCREEN_COVER_TRANSITION; i++) {
            float x = getX(i);
            spawnedRT.anchoredPosition = new Vector2(x, 0f);
            yield return null;
        }
        spawnedRT.anchoredPosition = new Vector2(goalX, 0f);
    }
    #endregion
}
