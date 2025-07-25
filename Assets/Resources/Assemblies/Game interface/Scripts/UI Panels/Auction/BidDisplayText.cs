using TMPro;
using UnityEngine;

public class BidDisplayText : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI frontText;
    [SerializeField] private TextMeshProUGUI backText;



    public void adjustBidDisplay(int bid) {
        string bidText = "$" + bid.ToString();
        frontText.text = bidText;
        backText.text = bidText;
    }
}
