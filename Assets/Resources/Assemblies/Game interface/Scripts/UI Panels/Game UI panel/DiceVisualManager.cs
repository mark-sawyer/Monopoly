using UnityEngine;

public class DiceVisualManager : MonoBehaviour {
    [SerializeField] private DieVisual die1;
    [SerializeField] private DieVisual die2;



    private void Start() {
        UIPipelineEventHub.Instance.sub_RollButtonClicked(startDiceRoll);
        UIEventHub.Instance.sub_NonTurnDiceRoll(startDiceRoll);
    }
    private void startDiceRoll() {
        SoundPlayer.Instance.play_DiceSound();
        DiceInfo diceInfo = GameState.game.DiceInfo;
        die1.startDieRoll(diceInfo.getDieValue(0));
        die2.startDieRoll(diceInfo.getDieValue(1));
    }
    private void startDiceRoll(int value1, int value2) {
        SoundPlayer.Instance.play_DiceSound();
        DiceInfo diceInfo = GameState.game.DiceInfo;
        die1.startDieRoll(value1);
        die2.startDieRoll(value2);
    }
}
