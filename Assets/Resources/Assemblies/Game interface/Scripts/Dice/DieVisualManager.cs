using UnityEngine;

public class DieVisualManager : MonoBehaviour {
    [SerializeField] private GameEvent rollClicked;
    [SerializeField] private DieVisual die1;
    [SerializeField] private DieVisual die2;


    private void Start() {
        rollClicked.Listeners += startDiceRoll;
    }
    private void startDiceRoll() {
        die1.startDieRoll();
        die2.startDieRoll();
    }
}
