using TMPro;
using UnityEngine;

public class StoreDiceValues : MonoBehaviour {
    [SerializeField] private TMP_InputField die1Value;
    [SerializeField] private TMP_InputField die2Value;
    private DiceValueStorer diceValueStorer;
    


    private void Start() {
        diceValueStorer = (DiceValueStorer)GameState.game.DiceInfo;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            try {
                diceValueStorer.storeValues(
                    new int[2] {
                    int.Parse(die1Value.text),
                    int.Parse(die2Value.text)
                    }
                );
                print("Values stored.");
            }
            catch {
                print("Invalid values.");
            }
        }
    }
}
