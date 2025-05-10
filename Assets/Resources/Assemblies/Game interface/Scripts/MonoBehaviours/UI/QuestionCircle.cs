using UnityEngine;
using UnityEngine.EventSystems;

public class QuestionCircle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private GameObject hoverImagePrefab;
    private GameObject hoverImage;
    private bool hoverOn;

    private void Update() {
        if (!hoverOn) return;

        hoverImage.transform.position = Input.mousePosition;
    }
    public void OnPointerEnter(PointerEventData eventData) {
        hoverOn = true;
        hoverImage = Instantiate(
            hoverImagePrefab,
            Input.mousePosition,
            Quaternion.identity,
            transform
        );
    }
    public void OnPointerExit(PointerEventData eventData) {
        hoverOn = false;
        Destroy(hoverImage);
    }
}
