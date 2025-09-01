using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    [SerializeField] private Button button;



    #region Implementing
    public void OnPointerDown(PointerEventData eventData) {
        bool notLeft = eventData.button != PointerEventData.InputButton.Left;
        bool buttonOff = !button.interactable;
        if (notLeft || buttonOff) return;
        SoundPlayer.Instance.play_ButtonDown();
    }
    public void OnPointerUp(PointerEventData eventData) {
        bool notLeft = eventData.button != PointerEventData.InputButton.Left;
        bool buttonOff = !button.interactable;
        if (notLeft || buttonOff) return;
        SoundPlayer.Instance.play_ButtonUp();
    }
    #endregion
}
