using UnityEngine;
using UnityEngine.UI;

public class OwnedIconIconManager : MonoBehaviour {
    [SerializeField] private GameObject[] iconGameObjects;
    [SerializeField] private RectTransform mortgageRT;
    private const int HOUSE_INDEX = 0;
    private const int TRAIN_INDEX = 1;
    private const int ELECTRICITY_INDEX = 2;
    private const int WATER_INDEX = 3;
    private const int LOCK_INDEX = 4;
    private const int MORTGAGE_INDEX = 5;



    public void setupHouse(Color colour, bool isMortgaged) {
        activateIcon(HOUSE_INDEX, isMortgaged);
        Image image = iconGameObjects[HOUSE_INDEX].GetComponent<Image>();
        image.color = colour;
    }
    public void setupTrain(bool isMortgaged) {
        activateIcon(TRAIN_INDEX, isMortgaged);
    }
    public void setupElectricity(bool isMortgaged) {
        activateIcon(ELECTRICITY_INDEX, isMortgaged);
    }
    public void setupWater(bool isMortgaged) {
        activateIcon(WATER_INDEX, isMortgaged);
    }
    public void setupLock(Color colour) {
        activateIcon(LOCK_INDEX, false);
        Image image = iconGameObjects[LOCK_INDEX].GetComponent<Image>();
        image.color = colour;
    }



    #region private
    private void activateIcon(int index, bool isMortgaged) {
        for (int i = 0; i < 6; i++) {
            GameObject iconGameObject = iconGameObjects[i];
            bool active = (i == index) || (isMortgaged && i == MORTGAGE_INDEX);
            iconGameObject.SetActive(active);
        }
        if (isMortgaged) {
            float pos = ((RectTransform)iconGameObjects[index].transform).offsetMin.x;
            mortgageRT.anchoredPosition = new Vector2(pos, 0);
        }
    }
    #endregion
}
