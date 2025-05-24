using TMPro;
using UnityEngine;

public class PropertyGroupSelected : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textMesh;
    private int propertyGroup = 1;
    public int PropertyGroup => propertyGroup;

    void Start() {
        textMesh.text = propertyGroup.ToString();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow) && propertyGroup < 10) propertyGroup += 1;
        else if (Input.GetKeyDown(KeyCode.DownArrow) && propertyGroup > 1) propertyGroup -= 1;
        textMesh.text = propertyGroup.ToString();
    }
}
