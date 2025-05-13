using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class LittleHouseIcon : MonoBehaviour {
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private int number;
    [SerializeField] private BuildingType type;
    private Color houseColour = new Color(33f / 255f, 156f / 255f, 0f / 255f);
    private Color hotelColour = new Color(214f / 255f, 38f / 255f, 31f / 255f);

    private void Start() {
        if (type == BuildingType.HOUSE) {
            image.color = houseColour;
            textMesh.text = number.ToString();
            ((RectTransform)textMesh.transform).anchoredPosition = getHousePosition(number);
        }
        else {
            image.color = hotelColour;
            textMesh.text = "";
        }
    }
    private Vector2 getHousePosition(int number) {
        if (number == 1) return new Vector2(-0.88f, -1.76f);
        else if (number <= 3) return new Vector2(0f, -1.76f);
        else if (number == 4) return new Vector2(-0.35f, -1.76f);
        throw new System.Exception("Invalid number of houses");
    }
}
