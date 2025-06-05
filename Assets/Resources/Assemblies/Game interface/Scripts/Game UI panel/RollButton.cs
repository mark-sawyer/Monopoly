using UnityEngine;
using UnityEngine.UI;

public class RollButton : MonoBehaviour {
    [SerializeField] private GameEvent nextPlayerTurn;
    [SerializeField] private GameEvent samePlayerTurn;
    [SerializeField] private Button button;
    [SerializeField] private GameEvent rollClicked;
    [SerializeField] private GameEvent animationOver;

    private void Start() {
        nextPlayerTurn.Listeners += () => button.interactable = true;
        samePlayerTurn.Listeners += () => button.interactable = true;
    }

    public void buttonClicked() {
        rollClicked.invoke();
        StartCoroutine(WaitFrames.exe(
            InterfaceConstants.DIE_FRAMES_PER_IMAGE * InterfaceConstants.DIE_IMAGES_BEFORE_SETTLING,
            () => animationOver.invoke()
        ));
        button.interactable = false;
    }
}
