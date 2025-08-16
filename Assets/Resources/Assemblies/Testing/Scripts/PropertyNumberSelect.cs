using TMPro;
using UnityEngine;

public class PropertyNumberSelect : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textMesh;
    public int PropertyNumber { get; private set; }



    private void Start() {
        PropertyNumber = 1;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (PropertyNumber < 4) {
                PropertyNumber++;
                textMesh.text = PropertyNumber.ToString();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (PropertyNumber > 1) {
                PropertyNumber--;
                textMesh.text = PropertyNumber.ToString();
            }
        }
    }
}
