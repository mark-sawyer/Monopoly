using UnityEngine;

public class DieVisualManager : MonoBehaviour {
    [SerializeField] private GameEvent rollClicked;
    [SerializeField] private DieVisual die1;
    [SerializeField] private DieVisual die2;



    private void Start() {
        rollClicked.Listeners += startDiceRoll;
    }
    private void startDiceRoll() {
        die1.startDieRoll(InterfaceConstants.DIE_FRAMES_PER_IMAGE, InterfaceConstants.DIE_IMAGES_BEFORE_SETTLING);
        die2.startDieRoll(InterfaceConstants.DIE_FRAMES_PER_IMAGE, InterfaceConstants.DIE_IMAGES_BEFORE_SETTLING);
    }
}
