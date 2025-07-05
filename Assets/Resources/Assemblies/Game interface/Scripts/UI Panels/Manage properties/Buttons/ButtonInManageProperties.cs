using UnityEngine;
using UnityEngine.UI;

public class ButtonInManageProperties : MonoBehaviour {
    [SerializeField] private Button button;

    private void Start() {
        ManagePropertiesEventHub.Instance.sub_BackButtonPressed(() => button.interactable = false);
    }
}
