using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OKButton : MonoBehaviour {
    [SerializeField] private Button button;


    private void Start() {
        button.interactable = false;
    }
    public void raise(RectTransform cardTransform) {
        RectTransform canvasRT = (RectTransform)transform.parent;
        float canvasHeight = canvasRT.rect.height;
        float cardHeight = cardTransform.rect.height;
        float spaceOnBottomHalf = (canvasHeight - cardHeight) / 2f;
        float yEnd = (-cardHeight - spaceOnBottomHalf) / 2f;
        float yStart = transform.localPosition.y;
        StartCoroutine(increaseYPos(yStart, yEnd));
    }

    private IEnumerator increaseYPos(float yStart, float yEnd) {
        for (int i = 0; i < InterfaceConstants.FRAMES_FOR_CARD_FLIP; i++) yield return null;

        int length = InterfaceConstants.FRAMES_FOR_CARD_FLIP;
        for (int i = 1; i <= length; i++) {
            float yPos = i * (yEnd - yStart) / length + yStart;
            transform.localPosition = new Vector3(0f, yPos, 0f);
            yield return null;
        }
        transform.localPosition = new Vector3(0f, yEnd, 0f);
        button.interactable = true;
    }
}
