using TMPro;
using UnityEngine;

public class InJailOptions : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI countText;


    #region MonoBehaviour
    private void OnEnable() {
        setup();
        UIEventHub.Instance.sub_PrerollStateStarting(setup);
    }
    private void OnDisable() {
        UIEventHub.Instance.unsub_PrerollStateStarting(setup);
    }
    #endregion



    #region public
    private void setup() {
        int count = GameState.game.TurnPlayer.TurnInJail;
        countText.text = count.ToString();
        RectTransform rt = (RectTransform)countText.transform;
        if (count == 1) rt.anchoredPosition = new Vector3(-2f, 0f, 0f);
        else rt.anchoredPosition = Vector3.zero;
    }
    #endregion
}
