using System;
using System.Collections;
using UnityEngine;

public class DeedSpawner : MonoBehaviour {
    #region Internal references
    [SerializeField] private RectTransform middleSectionRT;
    #endregion
    #region External references
    [SerializeField] private GameObject estateDeed;
    [SerializeField] private GameObject railroadDeed;
    [SerializeField] private GameObject utilityDeed;
    #endregion
    #region Private attributes
    private RectTransform deedRT;
    private float canvasWidth;
    private float canvasHeight;
    private float middleWidth;
    private float goalX;
    #endregion



    #region public
    public void spawnDeed(PropertyInfo propertyInfo) {
        GameObject deedPrefab = getPrefabToSpawn(propertyInfo);
        GameObject spawnedDeed = Instantiate(deedPrefab, transform);
        PropertyDeed propertyDeed = spawnedDeed.GetComponent<PropertyDeed>();
        propertyDeed.setupCard(propertyInfo);
        setAttributes(spawnedDeed);
        scaleDeed();
        goalX = getGoalX();
    }
    public IEnumerator moveDeed() {
        int frames = FrameConstants.SCREEN_COVER_TRANSITION;
        RectTransform rt = (RectTransform)transform;
        Func<float, float> getXPos = LinearValue.getFunc(rt.anchoredPosition.x, goalX, frames);
        for (int i = 1; i <= frames; i++) {
            float xPos = getXPos(i);
            rt.anchoredPosition = new Vector3(xPos, 0f, 0f);
            yield return null;
        }
        rt.anchoredPosition = new Vector3(goalX, 0f, 0f);
    }
    #endregion



    #region private
    private GameObject getPrefabToSpawn(PropertyInfo propertyInfo) {
        if (propertyInfo is EstateInfo) return estateDeed;
        else if (propertyInfo is RailroadInfo) return railroadDeed;
        else if (propertyInfo is UtilityInfo) return utilityDeed;

        throw new System.Exception();
    }
    private void setAttributes(GameObject spawnedDeed) {
        RectTransform canvasRT = (RectTransform)transform.parent;
        canvasWidth = canvasRT.rect.width;
        canvasHeight = canvasRT.rect.height;
        middleWidth = middleSectionRT.rect.width;
        deedRT = (RectTransform)spawnedDeed.transform;
    }
    private void scaleDeed() {
        float deedHeight = deedRT.rect.height;
        float deedScale = 0.5f * canvasHeight / deedHeight;
        deedRT.localScale = new Vector3(deedScale, deedScale, deedScale);
    }
    private float getGoalX() {
        float deedScale = deedRT.localScale.x;
        float deedWidth = deedRT.rect.width * deedScale;
        float sideSpace = (canvasWidth - middleWidth) / 2;
        float spaceBetweenQuestionAndDeed = (sideSpace - deedWidth) / 2;
        return -deedWidth - spaceBetweenQuestionAndDeed;
    }
    #endregion
}
