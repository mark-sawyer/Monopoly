using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class InJailOptions : MonoBehaviour {
    [SerializeField] private Button payFiftyButton;
    [SerializeField] private Button chanceCardButton;
    [SerializeField] private Button ccCardButton;
    [SerializeField] private TextMeshProUGUI countText;
    private UIEventHub uiEvents;
    private UIPipelineEventHub uiPipelines;


    #region MonoBehaviour
    private void OnEnable() {
        uiEvents = UIEventHub.Instance;
        uiPipelines = UIPipelineEventHub.Instance;
        uiPipelines.sub_RollButtonClicked(disableButtons);
        uiEvents.sub_PayFiftyButtonClicked(disableButtons);
        uiPipelines.sub_UseGOOJFCardButtonClicked(disableButtons);
        uiEvents.sub_JailPreRollStateStarting(setup);
        setup();
    }
    private void OnDisable() {
        uiPipelines.unsub_RollButtonClicked(disableButtons);
    }
    #endregion



    #region public
    public void setup() {
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
    private void disableButtons(CardType ct) {
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
