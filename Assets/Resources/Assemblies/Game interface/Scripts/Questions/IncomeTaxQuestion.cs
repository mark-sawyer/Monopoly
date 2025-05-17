using UnityEngine;

public class IncomeTaxQuestion : MonoBehaviour {
    [SerializeField] private TokenIcon tokenIcon;

    public void setup(PlayerInfo player) {
        tokenIcon.setup(player);
    }
}
