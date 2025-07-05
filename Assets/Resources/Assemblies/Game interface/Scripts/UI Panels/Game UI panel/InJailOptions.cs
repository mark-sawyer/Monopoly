using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InJailOptions : MonoBehaviour {
    [SerializeField] private Button payFiftyButton;
    [SerializeField] private Button chanceCardButton;
    [SerializeField] private Button ccCardButton;
    [SerializeField] private TextMeshProUGUI countText;


    #region MonoBehaviour
    private void OnEnable() {
        UIEventHub.Instance.sub_RollButtonClicked(disableButtons);
        UIEventHub.Instance.sub_TurnBegin(setup);
        setup(true);
    }
    private void OnDisable() {
        UIEventHub.Instance.unsub_RollButtonClicked(disableButtons);
        UIEventHub.Instance.unsub_TurnBegin(setup);
    }
    #endregion



    #region public
    public void setup(bool turnPlayerIsInJail) {
        if (!turnPlayerIsInJail) return;

        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        payFiftyButton.interactable = canUsePayButton(turnPlayer);
        chanceCardButton.interactable = canUseCardButton(turnPlayer, CardType.CHANCE);
        ccCardButton.interactable = canUseCardButton(turnPlayer, CardType.COMMUNITY_CHEST);
        setText();
    }
    #endregion



    #region private
    private void setText() {
        int count = GameState.game.TurnPlayer.TurnInJail;
        countText.text = count.ToString();
        RectTransform rt = (RectTransform)countText.transform;
        if (count == 1) rt.anchoredPosition = new Vector3(-2f, 0f, 0f);
        else rt.anchoredPosition = Vector3.zero;
    }
    private void disableButtons() {
        payFiftyButton.interactable = false;
        chanceCardButton.interactable = false;
        ccCardButton.interactable = false;
    }
    private bool canUsePayButton(PlayerInfo turnPlayer) {
        return turnPlayer.Money >= 50 && turnPlayer.TurnInJail < 3;
    }
    private bool canUseCardButton(PlayerInfo turnPlayer, CardType cardType) {
        return turnPlayer.hasGOOJFCardOfType(cardType);
    }
    #endregion
}
