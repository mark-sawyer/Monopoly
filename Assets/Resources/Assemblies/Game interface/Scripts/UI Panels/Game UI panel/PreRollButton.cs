using UnityEngine;
using UnityEngine.UI;

public class PreRollButton : MonoBehaviour {
    [SerializeField] private Button button;

    private void Start() {
        UIEventHub.Instance.sub_RollButtonClicked(() => button.interactable = false);
        ManagePropertiesEventHub.Instance.sub_ManagePropertiesOpened(() => button.interactable = false);
        UIEventHub.Instance.sub_TurnBegin((bool b) => button.interactable = true);
    }
}
