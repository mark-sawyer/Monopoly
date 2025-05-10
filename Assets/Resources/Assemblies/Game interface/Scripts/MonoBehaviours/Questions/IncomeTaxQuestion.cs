using UnityEngine;

public class IncomeTaxQuestion : MonoBehaviour {
    [SerializeField] private TokenIcon tokenIcon;

    public void setup(PlayerInfo player, TokenVisualManager tokenVisualManager) {
        tokenIcon.setup(player, tokenVisualManager);
    }
}
