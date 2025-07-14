using System;
using System.Collections;
using UnityEngine;

public class DeedSpawner : MonoBehaviour {
    #region Internal references
    [SerializeField] private RectTransform questionSectionRT;
    #endregion
    #region External references
    [SerializeField] private GameObject estateDeed;
    [SerializeField] private GameObject railroadDeed;
    [SerializeField] private GameObject utilityDeed;
    #endregion
    #region Private attributes
    private RectTransform deedRT;
    private float canvasWidth;
    private float questionHeight;
    private float questionWidth;
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
        int frames = InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION;
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
        float getQuestionWidth(float scale) {
            RectTransform buttonRT = (RectTransform)questionSectionRT.GetChild(2);
            float width = buttonRT.rect.width * scale;
            return width;
        }

        float questionScale = questionSectionRT.localScale.x;
        canvasWidth = ((RectTransform)transform.parent).rect.width;
        questionHeight = questionSectionRT.rect.height * questionScale;
        questionWidth = getQuestionWidth(questionScale);
        deedRT = (RectTransform)spawnedDeed.transform;
    }
    private void scaleDeed() {
        float deedHeight = deedRT.rect.height;
        float deedScale = questionHeight / deedHeight;
        deedRT.localScale = new Vector3(deedScale, deedScale, deedScale);
    }
    private float getGoalX() {
        float deedScale = deedRT.localScale.x;
        float deedWidth = deedRT.rect.width * deedScale;
        float sideSpace = (canvasWidth - questionWidth) / 2;
        float spaceBetweenQuestionAndDeed = (sideSpace - deedWidth) / 2;
        return -deedWidth - spaceBetweenQuestionAndDeed;
    }
    #endregion
}
