using UnityEngine;

[ExecuteAlways]
public class SetupTokenIcons : MonoBehaviour {
    [SerializeField] private Token[] tokens;
    [SerializeField] private PlayerColour[] playerColours;

    private void Update() {
        for (int i = 0; i < 8; i++) {
            transform.GetChild(i).GetComponent<TokenIcon>().setup(tokens[i], playerColours[i]);
        }
    }
}
