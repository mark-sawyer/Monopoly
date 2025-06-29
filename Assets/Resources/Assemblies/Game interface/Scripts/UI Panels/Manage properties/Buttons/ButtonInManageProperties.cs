using UnityEngine;
using UnityEngine.UI;

public class ButtonInManageProperties : MonoBehaviour {
    [SerializeField] private GameEvent backButtonPressed;
    [SerializeField] private Button button;

    private void Start() {
        backButtonPressed.Listeners += () => button.interactable = false;
    }
}
