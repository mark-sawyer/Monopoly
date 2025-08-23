using TMPro;
using UnityEngine;

public class StoreDiceValues : MonoBehaviour {
    [SerializeField] private TMP_InputField die1Value;
    [SerializeField] private TMP_InputField die2Value;
    private DiceValueStorer diceValueStorer;



    #region MonoBehaviour
    private void Start() {
        diceValueStorer = (DiceValueStorer)GameState.game.DiceInfo;
        UIPipelineEventHub.Instance.sub_RollButtonClicked(playDiceSound);
        UIEventHub.Instance.sub_NonTurnDiceRoll((int x, int y) => playDiceSound());
    }
    private void Update() {
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
    #endregion



    #region private
    private void playDiceSound() {
        SoundPlayer.Instance.play_DiceSound();
    }
    #endregion
}
