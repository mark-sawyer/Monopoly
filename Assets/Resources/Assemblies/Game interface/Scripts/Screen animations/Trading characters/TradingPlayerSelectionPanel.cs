using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TradingPlayerSelectionPanel : MonoBehaviour {
    [SerializeField] private GameObject ghostSourcePrefab;
    [SerializeField] private RectTransform prefabRT;



    public void displayCharacters(IEnumerable<PlayerInfo> activePlayers) {
        int players = activePlayers.Count();
        int i = 0;
        float gap = getGap();
        float xShift = getXShift(players, gap);
        foreach (PlayerInfo playerInfo in activePlayers) {
            GameObject ghostSourceInstance = Instantiate(ghostSourcePrefab, transform);
            RectTransform ghostSourceRT = (RectTransform)ghostSourceInstance.transform;
            TokenIcon tokenIcon = ghostSourceInstance.GetComponent<TokenIcon>();
            tokenIcon.setup(playerInfo.Token, playerInfo.Colour);
            float width = ghostSourceRT.rect.width;
            ghostSourceRT.anchoredPosition = new Vector2((width + gap) * i - xShift, 0f);
            i++;
        }
    }



    private float getGap() {
        float width = prefabRT.rect.width;
        return width * 0.2f;
    }
    private float getXShift(int count, float gap) {
        float width = prefabRT.rect.width;
        float widthFromTokens = count * width;
        float widthFromGaps = (count - 1) * gap;
        float fullWidth = widthFromTokens + widthFromGaps;
        return fullWidth / 2f;
    }
}
