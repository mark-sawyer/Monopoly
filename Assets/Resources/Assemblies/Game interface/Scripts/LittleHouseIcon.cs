using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class LittleHouseIcon : MonoBehaviour {
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private int number;
    [SerializeField] private bool isHouse;
    [SerializeField] private GameColour houseColour;
    [SerializeField] private GameColour hotelColour;

    private void Start() {
        if (isHouse) {
            image.color = houseColour.Colour;
            textMesh.text = number.ToString();
            ((RectTransform)textMesh.transform).anchoredPosition = getHousePosition(number);
        }
        else {
            image.color = hotelColour.Colour;
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
