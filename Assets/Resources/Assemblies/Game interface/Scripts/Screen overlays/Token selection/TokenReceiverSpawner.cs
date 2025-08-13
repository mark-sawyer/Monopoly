using UnityEngine;

public class TokenReceiverSpawner : MonoBehaviour {
    [SerializeField] private GameObject tokenReceiverPrefab;



    public void setup(int numberOfPlayers) {
        RectTransform RT = GetComponent<RectTransform>();
        float gap = 30f;
        float receiverWidth = ((RectTransform)tokenReceiverPrefab.transform).rect.width;
        float containerWidth = numberOfPlayers * (receiverWidth + gap) - gap;
        for (int i = 0; i < numberOfPlayers; i++) {
            GameObject newReceiver = Instantiate(tokenReceiverPrefab, transform);
            RectTransform newReceiverRT = (RectTransform)newReceiver.transform;
            float xPos = i * (receiverWidth + gap);
            newReceiverRT.anchoredPosition = new Vector2(xPos, 0f);
        }
        RT.anchorMin = new Vector2(0.5f, 0.5f);
        RT.anchorMax = new Vector2(0.5f, 0.5f);
        RT.sizeDelta = new Vector2(containerWidth, RT.rect.height);
        RT.anchoredPosition = new Vector2(0f, RT.anchoredPosition.y);
    }
}
