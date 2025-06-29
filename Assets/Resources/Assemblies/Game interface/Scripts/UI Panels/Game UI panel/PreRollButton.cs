using UnityEngine;
using UnityEngine.UI;

public class PreRollButton : MonoBehaviour {
    [SerializeField] private GameEvent rollButtonClicked;
    [SerializeField] private GameEvent managePropertiesButtonClicked;
    [SerializeField] private GameEvent regularTurnBegin;
    [SerializeField] private GameEvent jailTurnBeginUI;
    [SerializeField] private Button button;

    private void Start() {
        rollButtonClicked.Listeners += () => button.interactable = false;
        managePropertiesButtonClicked.Listeners += () => button.interactable = false;
        regularTurnBegin.Listeners += () => button.interactable = true;
        jailTurnBeginUI.Listeners += () => button.interactable = true;
    }
}
