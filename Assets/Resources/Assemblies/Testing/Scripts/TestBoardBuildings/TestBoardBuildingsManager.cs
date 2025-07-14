using UnityEngine;

public class TestBoardBuildingsManager : MonoBehaviour {
    [SerializeField] private int spaceIndex;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpaceVisual space = SpaceVisualManager.Instance.getSpaceVisual(spaceIndex);
            if (space is not EstateVisual estateVisual) return;

            Transform housesTransform = estateVisual.transform.GetChild(2).GetChild(0);
            int activeChildren = countActiveChildren(housesTransform);
            int houseToActivate = activeChildren + 1;
            if (houseToActivate > 4) return;
            //estateVisual.toggleHouse(true);
        }
    }

    private int countActiveChildren(Transform t) {
        int count = 0;
        for (int i = 0; i < 4; i++) {
            if (t.GetChild(i).gameObject.activeSelf) {
                count += 1;
            }
        }
        return count;
    }
}
