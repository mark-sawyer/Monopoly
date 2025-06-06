using System.Collections.Generic;
using UnityEngine;

public class CardFlipper : MonoBehaviour {
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private RectTransform rt;
    [SerializeField] private GameObject[] chancePrefabs;
    [SerializeField] private GameObject[] communityChestPrefabs;
    private Dictionary<int, GameObject> chanceIDToPrefabDictionary = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> communityChestIDToPrefabDictionary = new Dictionary<int, GameObject>();
    private GameObject instance;



    #region MonoBehaviour
    private void Start() {
        initialiseChanceDictionary();
        initialiseCommunityChestyDictionary();
    }
    #endregion



    public void flipCard(CardType cardType, int id) {
        if (instance != null) Destroy(instance);
        GameObject prefab = getPrefab(cardType, id);
        float canvasHeight = rt.rect.height;
        float canvasWidth = rt.rect.width;
        instance = Instantiate(prefab, transform);
        instance.transform.localPosition = new Vector3(
            -canvasWidth / 4f,
            -1.1f * canvasHeight / 2f,
            0f
        );
        instance.transform.localRotation = Quaternion.Euler(-100f, 20, 0);
        instance.GetComponent<CardMonoBehaviour>().startCoroutines(cameraTransform);
    }



    #region private
    private GameObject getPrefab(CardType cardType, int id) {
        if (cardType == CardType.CHANCE) return chanceIDToPrefabDictionary[id];
        else return communityChestIDToPrefabDictionary[id];
    }
    private void initialiseChanceDictionary() {
        chanceIDToPrefabDictionary.Add(1, chancePrefabs[0]);
        chanceIDToPrefabDictionary.Add(2, chancePrefabs[1]);
        chanceIDToPrefabDictionary.Add(3, chancePrefabs[2]);
        chanceIDToPrefabDictionary.Add(4, chancePrefabs[3]);
        chanceIDToPrefabDictionary.Add(5, chancePrefabs[4]);
        chanceIDToPrefabDictionary.Add(6, chancePrefabs[5]);
        chanceIDToPrefabDictionary.Add(7, chancePrefabs[6]);
        chanceIDToPrefabDictionary.Add(8, chancePrefabs[7]);
        chanceIDToPrefabDictionary.Add(9, chancePrefabs[8]);
        chanceIDToPrefabDictionary.Add(10, chancePrefabs[9]);
        chanceIDToPrefabDictionary.Add(11, chancePrefabs[9]);
        chanceIDToPrefabDictionary.Add(12, chancePrefabs[10]);
        chanceIDToPrefabDictionary.Add(13, chancePrefabs[11]);
        chanceIDToPrefabDictionary.Add(14, chancePrefabs[12]);
        chanceIDToPrefabDictionary.Add(15, chancePrefabs[13]);
        chanceIDToPrefabDictionary.Add(16, chancePrefabs[14]);
    }
    private void initialiseCommunityChestyDictionary() {
        communityChestIDToPrefabDictionary.Add(1, communityChestPrefabs[0]);
        communityChestIDToPrefabDictionary.Add(2, communityChestPrefabs[1]);
        communityChestIDToPrefabDictionary.Add(3, communityChestPrefabs[2]);
        communityChestIDToPrefabDictionary.Add(4, communityChestPrefabs[3]);
        communityChestIDToPrefabDictionary.Add(5, communityChestPrefabs[4]);
        communityChestIDToPrefabDictionary.Add(6, communityChestPrefabs[5]);
        communityChestIDToPrefabDictionary.Add(7, communityChestPrefabs[6]);
        communityChestIDToPrefabDictionary.Add(8, communityChestPrefabs[7]);
        communityChestIDToPrefabDictionary.Add(9, communityChestPrefabs[8]);
        communityChestIDToPrefabDictionary.Add(10, communityChestPrefabs[9]);
        communityChestIDToPrefabDictionary.Add(11, communityChestPrefabs[10]);
        communityChestIDToPrefabDictionary.Add(12, communityChestPrefabs[11]);
        communityChestIDToPrefabDictionary.Add(13, communityChestPrefabs[12]);
        communityChestIDToPrefabDictionary.Add(14, communityChestPrefabs[13]);
        communityChestIDToPrefabDictionary.Add(15, communityChestPrefabs[14]);
        communityChestIDToPrefabDictionary.Add(16, communityChestPrefabs[15]);
    }
    #endregion
}
