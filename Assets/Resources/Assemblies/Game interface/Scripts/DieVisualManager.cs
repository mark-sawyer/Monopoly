using UnityEngine;

public class DieVisualManager : MonoBehaviour {
    [SerializeField] DieVisual die1;
    [SerializeField] DieVisual die2;



    public void startDiceRoll() {
        die1.startDieRoll();
        die2.startDieRoll();
    }
}
