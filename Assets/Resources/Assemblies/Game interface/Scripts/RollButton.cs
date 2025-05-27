using UnityEngine;

public class RollButton : MonoBehaviour {
    [SerializeField] private GameEvent rollClicked;
    [SerializeField] private GameEvent animationOver;

    public void buttonClicked() {
        rollClicked.invoke();
        StartCoroutine(WaitFrames.exe(
            InterfaceConstants.DIE_FRAMES_PER_IMAGE * InterfaceConstants.DIE_IMAGES_BEFORE_SETTLING,
            () => animationOver.invoke()
        ));
    }
}
