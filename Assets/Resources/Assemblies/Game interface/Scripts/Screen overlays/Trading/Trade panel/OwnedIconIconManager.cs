using UnityEngine;
using UnityEngine.UI;

public class OwnedIconIconManager : MonoBehaviour {
    [SerializeField] private GameObject[] iconGameObjects;
    private const int HOUSE_INDEX = 0;
    private const int TRAIN_INDEX = 1;
    private const int ELECTRICITY_INDEX = 2;
    private const int WATER_INDEX = 3;
    private const int LOCK_INDEX = 4;



    public void setupHouse(Color colour) {
        activateIcon(HOUSE_INDEX);
        Image image = iconGameObjects[HOUSE_INDEX].GetComponent<Image>();
        image.color = colour;
    }
    public void setupTrain() {
        activateIcon(TRAIN_INDEX);
    }
    public void setupElectricity() {
        activateIcon(ELECTRICITY_INDEX);
    }
    public void setupWater() {
        activateIcon(WATER_INDEX);
    }
    public void setupLock(Color colour) {
        activateIcon(LOCK_INDEX);
        Image image = iconGameObjects[LOCK_INDEX].GetComponent<Image>();
        image.color = colour;
    }



    #region private
    private void activateIcon(int index) {
        for (int i = 0; i < 5; i++) {
            GameObject iconGameObject = iconGameObjects[i];
            bool active = i == index;
            iconGameObject.SetActive(active);
        }
    }
    #endregion
}
