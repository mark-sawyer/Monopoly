using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TestQuestionAsked : MonoBehaviour {
    [SerializeField] RectTransform canvasRect;
    [SerializeField] RectTransform dropdownRect;
    [SerializeField] RectTransform titleDeedRect;
    [SerializeField] TitleDeed titleDeed;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<ScriptableObject> estateSOs;
    private IReadOnlyList<EstateInfo> estateInfos;



    #region MonoBehaviour
    private void Start() {
        GameEvents.propertyPurchaseQuestion.AddListener(startBringingDownOptions);
        estateInfos = estateSOs.Cast<EstateInfo>().ToList();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            GameEvents.propertyPurchaseQuestion.Invoke(estateInfos[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            GameEvents.propertyPurchaseQuestion.Invoke(estateInfos[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            GameEvents.propertyPurchaseQuestion.Invoke(estateInfos[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            GameEvents.propertyPurchaseQuestion.Invoke(estateInfos[3]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            GameEvents.propertyPurchaseQuestion.Invoke(estateInfos[4]);
        }
    }
    #endregion



    #region private
    private void startBringingDownOptions(EstateInfo estateInfo) {
        titleDeed.updateProperty(estateInfo);
        questionText.text = "PURCHASE FOR $" + estateInfo.Cost.ToString();
        scaleAndPositionTitleDeed();
        StartCoroutine(bringOverVisual(titleDeedRect, new Vector2((canvasRect.rect.width / 2f) - 50f, titleDeedRect.anchoredPosition.y)));
        StartCoroutine(bringOverVisual(dropdownRect, new Vector2(0f, -0.5f * canvasRect.rect.height)));
    }

    private void scaleAndPositionTitleDeed() {
        float cardHeight = ((RectTransform)titleDeedRect.GetChild(0)).rect.height;
        float goalFrac = 0.6f;
        float currentFrac = cardHeight / canvasRect.rect.height;
        float scalar = goalFrac / currentFrac;
        titleDeed.transform.localScale = new Vector3(scalar, scalar, 1f);
        titleDeedRect.pivot = new Vector2(0f, 0.5f);
        titleDeedRect.anchoredPosition = new Vector3(
            (canvasRect.rect.width / 2f) + 50f,
            -canvasRect.rect.height / 2f,
            0f
        );
        changePivotWithoutMoving(titleDeedRect, new Vector2(1f, 0.5f));
    }
    private IEnumerator bringOverVisual(RectTransform rt, Vector2 end) {
        Vector2 start = rt.anchoredPosition;
        Vector2 change = end - start;
        for (int i = 1; i <= InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION; i++) {
            Vector2 newPos = start + (i * change / InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION);
            rt.anchoredPosition = newPos;
            yield return null;
        }
    }
    private void changePivotWithoutMoving(RectTransform rt, Vector2 newPivot) {
        Vector2 size = rt.rect.size;
        Vector2 deltaPivot = rt.pivot - newPivot;
        Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);
        rt.pivot = newPivot;
        rt.localPosition -= deltaPosition;
    }
    #endregion
}
