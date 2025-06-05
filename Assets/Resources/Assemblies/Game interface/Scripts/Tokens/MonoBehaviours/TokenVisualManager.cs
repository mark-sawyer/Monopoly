using UnityEngine;
using System.Collections.Generic;

public class TokenVisualManager : MonoBehaviour {
    [SerializeField] private GameObject tokenPrefab;
    [SerializeField] private TokenSprites[] tokenSprites;
    [SerializeField] private TokenColours[] tokenColours;



    #region Singleton boilerplate
    public static TokenVisualManager Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion



    #region MonoBehaviour
    private void Start() {
        IEnumerable<PlayerInfo> players = GameState.game.PlayerInfos;
        int i = 0;
        foreach (PlayerInfo player in players) {
            Vector3 startingPosition = getStartingPosition(player);
            GameObject newToken = Instantiate(tokenPrefab, startingPosition, Quaternion.identity, transform);
            newToken.GetComponent<TokenVisual>().setup(player);
            newToken.GetComponent<TokenMover>().setup(player);
            newToken.GetComponent<TokenScaler>().setup(player);
            i += 1;
        }
    }
    #endregion



    #region public
    public TokenVisual getTokenVisual(int index) {
        return transform.GetChild(index).GetComponent<TokenVisual>();
    }
    public TokenMover getTokenMover(int index) {
        return transform.GetChild(index).GetComponent<TokenMover>();
    }
    public TokenScaler getTokenScaler(int index) {
        return transform.GetChild(index).GetComponent<TokenScaler>();
    }
    #endregion



    #region private
    public Vector3 getStartingPosition(PlayerInfo player) {
        SpaceVisual startingSpaceVisual = SpaceVisualManager.Instance.getSpaceVisual(0);
        return startingSpaceVisual.getMinorPoint(player);
    }
    #endregion
}
