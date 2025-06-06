using TMPro;
using UnityEngine;

public class CardFlipManager : MonoBehaviour {
    [SerializeField] private CardFlipper cardFlipper;
    [SerializeField] private TextMeshProUGUI cardTypeText;
    [SerializeField] private TextMeshProUGUI idText;
    [SerializeField] private int idSelected = 1;
    [SerializeField] private CardType cardTypeSelected;


    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow) && idSelected < 16) {
            idSelected++;
            idText.text = idSelected.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && idSelected > 1) {
            idSelected--;
            idText.text = idSelected.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (cardTypeSelected == CardType.CHANCE) {
                cardTypeSelected = CardType.COMMUNITY_CHEST;
                cardTypeText.text = "Community chest";
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (cardTypeSelected == CardType.COMMUNITY_CHEST) {
                cardTypeSelected = CardType.CHANCE;
                cardTypeText.text = "Chance";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            cardFlipper.flipCard(cardTypeSelected, idSelected);
        }
    }
}
