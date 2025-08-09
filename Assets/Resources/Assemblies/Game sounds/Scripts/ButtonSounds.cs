using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    [SerializeField] private Button button;



    #region Implementing
    public void OnPointerDown(PointerEventData eventData) {
        if (!button.interactable) return;
        SoundOnlyEventHub.Instance.call_ButtonDown();
    }
    public void OnPointerUp(PointerEventData eventData) {
        if (!button.interactable) return;
        SoundOnlyEventHub.Instance.call_ButtonUp();
    }
    #endregion
}
