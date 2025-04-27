using UnityEngine;
using UnityEngine.UI;

public class ReferencePasser : MonoBehaviour {
    [SerializeField] private Button rollButton;
    [SerializeField] private Transform tokensTransform;
    [SerializeField] private Transform boardTransform;

    public Button getRollButton() {
        return rollButton;
    }
    public Transform getTokensTransform() {
        return tokensTransform;
    }
    public Transform getBoardTransform() {
        return boardTransform;
    }
}
