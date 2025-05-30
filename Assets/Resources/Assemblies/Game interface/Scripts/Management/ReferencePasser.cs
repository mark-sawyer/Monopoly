using UnityEngine;
using UnityEngine.UI;

public class ReferencePasser : MonoBehaviour {
    [SerializeField] private Button rollButton;
    [SerializeField] private TokenVisualManager tokenVisualManager;
    [SerializeField] private SpaceVisualManager spaceVisualManager;
    #region properties
    public Button RollButton => rollButton;
    public TokenVisualManager TokenVisualManager => tokenVisualManager;
    public SpaceVisualManager SpaceVisualManager => spaceVisualManager;
    public GamePlayer GamePlayer { get; set; }
    #endregion
}
