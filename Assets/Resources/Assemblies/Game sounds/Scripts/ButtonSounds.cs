using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    [SerializeField] private Button button;
    private UIEventHub uiEvents;



    #region MonoBehaviour
    private void Start() {
        uiEvents = UIEventHub.Instance;
    }
    #endregion



    #region Implementing
    public void OnPointerDown(PointerEventData eventData) {
        if (!button.interactable) return;
        uiEvents.call_buttonDown();
    }
    public void OnPointerUp(PointerEventData eventData) {
        if (!button.interactable) return;
        uiEvents.call_buttonUp();
    }
    #endregion
}
